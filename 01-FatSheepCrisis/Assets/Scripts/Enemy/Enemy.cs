using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EnemyType
{
    None,
    Ð¡òùòð=2001,
    ´óòùòð,
    ÇàÍÜ,
    ÓÄÁé,
    Ð¡÷¼÷Ã
}
public class Enemy : EnemyBaseAttribute
{
    public EnemyType Type;
    [HideInInspector]
    public Rigidbody2D rigid;
    [HideInInspector]
    public bool isThunderBuff, isWaterBuff;

    private Transform target;
    private Damageable damageable;
    private SpriteRenderer sprite;

    private bool onHurt;
    private bool canAttack = true;
    private float _timer;

    private void Awake()
    {
        SetAttribute();
        target = Player.Instance.transform;
        sprite = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();

        damageable = GetComponent<Damageable>();

        damageable.onHurtStart.AddListener(OnHurtStart);
        damageable.onDeath.AddListener(OnDeath);
        damageable.onHurtEnd.AddListener(OnHurtEnd);
    }
    private void Update()
    {
        Flip();
        if (!canAttack)
        {
            _timer += Time.deltaTime;
            if (_timer >= 0.5)
            {
                canAttack = true;
                _timer = 0f;
            }
        }
    }

    private void FixedUpdate()
    {
        if (target == null || onHurt) return;
        rigid.velocity = (target.position - transform.position).normalized * MoveSpeed * 0.75f * (isThunderBuff ? 0 : 1) * (isWaterBuff ? 0.5f : 1);
    }

    public void Flip()
    {
        if (target == null) return;
        if (target.position.x < transform.position.x)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
        //transform.position = Vector2.MoveTowards(transform.position, target.position + Vector3.up * Random.Range(0.01f, 0.1f), MoveSpeed * Time.deltaTime / 2); 
    }

    public override void SetAttribute()
    {
        PackageItem enemyes = Resources.Load<PackageItem>("Config");
        Dictionary<string, EnemyData> Data = enemyes.GetEnemyesData();
        foreach (var item in Data.Values)
        {
            if (int.Parse(item.Id) == (int)Type)
            {
                Aggressivity = float.Parse(item.Aggressivity);
                Armor = float.Parse(item.Armor);
                Cur_Hp = Max_Hp = float.Parse(item.Max_Hp);
                MoveSpeed = float.Parse(item.MoveSpeed);
                DefenseRepelNum = float.Parse(item.DefenseRepelNum);
            }
        }
    }

    public void OnHurtStart(Damageable damageable,DamageMessage data)
    {
        sprite.color = Color.red;
        if (data.direction != Vector3.zero)
        {
            onHurt = true;
            rigid.velocity = (data.direction * 7) / DefenseRepelNum;
        }
        if(data.damage!=0)
            ObjectPool.Instance.RequestCacheGameObject(PrefabType.NumberText, transform.position + Vector3.up * 0.5f, data.damage, Player.Instance.isCrit ? 1 : 0);
    }

    public void OnHurtEnd()
    {
        onHurt = false;
        sprite.color = Color.white;
    }

    public void OnDeath(Damageable damageable, DamageMessage data)
    {
        Destroy(this.gameObject);
        EnemyManager.Instance.enemyList.Remove(this.transform);
        CalculateFallProbability();
        ObjectPool.Instance.RequestCacheGameObject(PrefabType.DestoryFX, transform.position);
        ObjectPool.Instance.RequestCacheGameObject(PrefabType.NumberText, transform.position + Vector3.up * 0.5f, data.damage, Player.Instance.isCrit ? 1 : 0);
    }

    private void CalculateFallProbability()
    {
        if (Random.value <= 0.1)
        {
            ObjectPool.Instance.RequestCacheGameObject(PrefabType.Gold, transform.position);
            return;
        }
        if (Random.value <= 0.25)
        {
            if (Random.value >= 0.2)
            {
                ObjectPool.Instance.RequestCacheGameObject(PrefabType.BlueDiamond, transform.position);
            }
            else
            {
                ObjectPool.Instance.RequestCacheGameObject(PrefabType.OrangeDiamond, transform.position);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Damageable damageable;
        if (collision.CompareTag("Player") && collision.TryGetComponent<Damageable>(out damageable))
        {
            DamageMessage data = new DamageMessage()
            {
                damage = Aggressivity,
            };
            if (canAttack)
            {
                damageable.OnDamage(data);
                canAttack = false;
            }
        }
    }
}

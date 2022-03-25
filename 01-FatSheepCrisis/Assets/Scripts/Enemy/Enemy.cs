using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EnemyType
{
    None,
    עשענ=2001,
}
public class Enemy : EnemyBaseAttribute
{
    public EnemyType Type;

    private Transform target;
    private Damageable damageable;
    private SpriteRenderer sprite;

    private void Awake()
    {
        SetAttribute();
        sprite = GetComponent<SpriteRenderer>();

        damageable = GetComponent<Damageable>();
        damageable.invinciableTime = 0.2f;

        damageable.onHurtStart.AddListener(OnHurtStart);
        damageable.onDeath.AddListener(OnDeath);
        damageable.onHurtEnd.AddListener(OnHurtEnd);

        EventCenter.AddListener(EventType.StartGame, StartGame);
    }

    private void StartGame()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        FollowPlayerAndFlip();
    }

    public void FollowPlayerAndFlip()
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
        transform.position = Vector2.MoveTowards(transform.position, target.position + Vector3.up * Random.Range(0.01f, 0.1f), MoveSpeed * Time.deltaTime / 2); ;
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
        transform.position = new Vector2(transform.position.x + data.direction.x, transform.position.y + data.direction.y);
        ObjectPool.Instance.RequestCacheGameObject(PrefabType.NumberText, transform.position + Vector3.up * 0.5f, data.damage, Player.Instance.isCrit ? 1 : 0);
    }

    public void OnHurtEnd()
    {
        sprite.color = Color.white;
    }

    public void OnDeath(Damageable damageable, DamageMessage data)
    {
        Destroy(this.gameObject);
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
            damageable.OnDamage(data);
        }
    }
}

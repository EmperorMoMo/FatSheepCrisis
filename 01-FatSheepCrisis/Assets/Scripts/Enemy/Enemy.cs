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
        target = GameObject.FindGameObjectWithTag("Player").transform;
        sprite = GetComponent<SpriteRenderer>();

        damageable = GetComponent<Damageable>();
        damageable.invinciableTime = 0.2f;

        damageable.onHurtStart.AddListener(OnHurtStart);
        damageable.onDeath.AddListener(OnDeath);
        damageable.onHurtEnd.AddListener(OnHurtEnd);
    }

    private void Update()
    {
        FollowPlayer();
    }

    public void FollowPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, MoveSpeed * Time.deltaTime);
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
    }

    public void OnHurtEnd()
    {
        sprite.color = Color.white;
    }

    public void OnDeath(Damageable damageable, DamageMessage data)
    {
        Destroy(this.gameObject);
    }
}

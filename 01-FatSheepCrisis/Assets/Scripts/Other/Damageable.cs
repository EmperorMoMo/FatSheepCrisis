using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageMessage
{
    public float damage;
    public Vector3 direction;
}

public enum Type
{
    None,
    Player,
    Boss,
    Enemy
}

[Serializable]
public class DamageableEvent : UnityEvent<Damageable, DamageMessage> { }

public class Damageable : MonoBehaviour
{
    public Type type;

    [HideInInspector]
    public float invinciableTime = 0;
    private float invinciableTimer = 0;
    private bool isInvinciable = false;

    private Player player;
    private Boss boss;
    private Enemy enemy;

    public DamageableEvent onHurtStart;
    public UnityEvent onHurtEnd;
    public DamageableEvent onDeath;

    private void Awake()
    {
        TryGetComponent(out player);
        TryGetComponent(out boss);
        TryGetComponent(out enemy);

        if (player != null) type = Type.Player;
        else if (boss != null) type = Type.Boss;
        else if (enemy != null) type = Type.Enemy;
    }

    private void Update()
    {
        if (isInvinciable)
        {
            invinciableTimer += Time.unscaledDeltaTime;
            if (invinciableTimer >= invinciableTime)
            {
                isInvinciable = false;
                invinciableTimer = 0f;
                onHurtEnd?.Invoke();
            }
        }
    }

    public void OnDamage(DamageMessage data)
    {

        if (isInvinciable) return;

        switch (type)
        {
            case Type.Player:
                Debug.Log("护甲：" + TotalAttribute.Armor + "---额外伤害：" + TotalAttribute.ExtraDamage + "---未减伤：" + data.damage + "---已减伤：" + data.damage * (1 - (TotalAttribute.Armor / 10)) * (1 + TotalAttribute.ExtraDamage) + "---四舍五入后：" + Mathf.RoundToInt(data.damage * (1 - (TotalAttribute.Armor / 10)) * (1 + TotalAttribute.ExtraDamage)));
                data.damage = Mathf.RoundToInt(data.damage * (1 - (TotalAttribute.Armor / 10)) * (1 + TotalAttribute.ExtraDamage));
                if (player.Cur_Hp <= 0) return;
                if (player.Cur_Hp > data.damage)
                {
                    player.Cur_Hp -= data.damage;
                    isInvinciable = true;
                    onHurtStart?.Invoke(this, data);
                }
                else
                {
                    player.Cur_Hp = 0;
                    onDeath?.Invoke(this, data);
                }
                break;
            case Type.Boss:
                if (boss.Cur_Hp <= 0) return;
                if (boss.Cur_Hp > data.damage)
                {
                    boss.Cur_Hp -= data.damage;
                    isInvinciable = true;
                    onHurtStart?.Invoke(this, data);
                }
                else
                {
                    boss.Cur_Hp = 0;
                    onDeath?.Invoke(this, data);
                }
                break;
            case Type.Enemy:
                if (enemy.Cur_Hp <= 0) return;
                if (enemy.Cur_Hp > data.damage)
                {
                    enemy.Cur_Hp -= data.damage;
                    isInvinciable = true;
                    onHurtStart?.Invoke(this, data);
                }
                else
                {
                    enemy.Cur_Hp = 0;
                    onDeath?.Invoke(this, data);
                }
                break;
        }
    }
}

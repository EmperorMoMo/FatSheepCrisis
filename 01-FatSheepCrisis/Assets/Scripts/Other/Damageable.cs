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
    [HideInInspector]
    public Type type;

    private float hurtColorShowTimer = 0;
    private bool isInvinciable = false;

    private Player player;
    private Boss boss;
    private Enemy enemy;

    [HideInInspector]
    public DamageableEvent onHurtStart;
    [HideInInspector]
    public UnityEvent onHurtEnd;
    [HideInInspector]
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
            hurtColorShowTimer += Time.deltaTime;
            if (hurtColorShowTimer >= 0.2)
            {
                isInvinciable = false;
                hurtColorShowTimer = 0f;
                onHurtEnd?.Invoke();
            }
        }
    }

    public void OnDamage(DamageMessage data)
    {

        //if (isInvinciable) return;

        switch (type)
        {
            case Type.Player:
                Debug.Log("???ף?" + TotalAttribute.Armor + "---?????˺???" + TotalAttribute.ExtraDamage + "---δ???ˣ?" + data.damage + "---?Ѽ??ˣ?" + data.damage * (1 - (TotalAttribute.Armor / 10)) * (1 + TotalAttribute.ExtraDamage) + "---????????????" + Mathf.RoundToInt(data.damage * (1 - (TotalAttribute.Armor / 10)) * (1 + TotalAttribute.ExtraDamage)));
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
                data.damage = Mathf.RoundToInt(data.damage * (1 - (boss.Armor / 10)));
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
                data.damage = Mathf.RoundToInt(data.damage * (1 - (enemy.Armor / 10)));
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

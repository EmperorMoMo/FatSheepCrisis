using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffType
{
    ����,
    �е�,
    ����,
    �ָ�
}
public class BuffRun : MonoBehaviour
{
    private Damageable damageable;
    private Enemy enemy;

    private float fireBuffDurationTime, thunderBuffDurationTime, waterBuffDurationTime;
    private bool fireBuffEnd = true, thunderBuffEnd = true, waterBuffEnd = true;

    // Start is called before the first frame update
    void Start()
    {
        damageable = GetComponent<Damageable>();
        TryGetComponent<Enemy>(out enemy);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            fireBuffDurationTime = 5;
    }

    public void Run(BuffType buffType, float durationTime, float aggressivity = 0f)
    {
        EventCenter.Broadcast(EventType.StatusBarHideOrShow, gameObject.GetInstanceID(), buffType, true);
        switch (buffType)
        {
            case BuffType.����:
                fireBuffDurationTime = durationTime;
                if (fireBuffEnd) StartCoroutine(FireBuff(aggressivity));
                break;
            case BuffType.�е�:
                thunderBuffDurationTime = durationTime;
                if (thunderBuffEnd) StartCoroutine(ThunderBuff());
                break;
            case BuffType.����:
                waterBuffDurationTime = durationTime;
                if (waterBuffEnd) StartCoroutine(WaterBuff());
                break;
        }
    }

    IEnumerator FireBuff(float aggressivity)
    {
        fireBuffEnd = false;
        while (fireBuffDurationTime >= 0)
        {
            yield return new WaitForSeconds(1);
            fireBuffDurationTime -= 1;
            DamageMessage data = new DamageMessage()
            {
                damage = aggressivity
            };
            damageable.OnDamage(data);
        }
        fireBuffEnd = true;
        EventCenter.Broadcast(EventType.StatusBarHideOrShow, gameObject.GetInstanceID(), BuffType.����, false);
    }
    IEnumerator ThunderBuff()
    {
        thunderBuffEnd = false;
        while (thunderBuffDurationTime != 0)
        {
            yield return new WaitForSeconds(0.5f);
            enemy.isThunderBuff = true;
            yield return new WaitForSeconds(0.5f);
            enemy.isThunderBuff = false;
            thunderBuffDurationTime -= 1;
        }
        thunderBuffEnd = true;
        EventCenter.Broadcast(EventType.StatusBarHideOrShow, gameObject.GetInstanceID(), BuffType.�е�, false);
    }
    IEnumerator WaterBuff()
    {
        waterBuffEnd = false;
        enemy.isWaterBuff = true;
        while (waterBuffDurationTime >= 0)
        {
            yield return new WaitForSeconds(1f);
            waterBuffDurationTime -= 1;
        }
        enemy.isWaterBuff = false;
        waterBuffEnd = true;
        EventCenter.Broadcast(EventType.StatusBarHideOrShow, gameObject.GetInstanceID(), BuffType.����, false);
    }
}

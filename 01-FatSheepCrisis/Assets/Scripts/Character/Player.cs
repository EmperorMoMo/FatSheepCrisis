using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Profession
{
    None=3000,
    Thieves,
    Berserker,
    Destoryer,
    Believer,
    Scholar,
}
public static class TalentSkillData
{
    public static float Max_Hp()
    {
        switch (Player.Instance.profession)
        {
            case Profession.Believer:
                return Player.Instance.Max_Hp * 0.5f;
        }
        return 0f;
    }
    public static float Re_Hp()
    {
        return 0f;
    }
    public static float Armor()
    {
        switch (Player.Instance.profession)
        {
            case Profession.Destoryer:
                return Mathf.Clamp(((int)(Player.Instance.Level / 5)) * 3 / 10f, 0, 3f);
        }
        return 0f;
    }
    public static float MoveSpeed()
    {
        switch (Player.Instance.profession)
        {
            case Profession.Thieves:
                return 0.25f;
            case Profession.Destoryer:
                return -0.2f;
        }
        return 0f;
    }
    public static float AttackSpeed()
    {
        return 0f;
    }
    public static float CritChance()
    {
        return 0f;
    }
    public static float CritDamage()
    {
        return 0f;
    }
    public static float PickUpRange()
    {
        return 0f;
    }
    public static float Exp_GainRate()
    {
        switch (Player.Instance.profession)
        {
            case Profession.Scholar:
                return 0.2f + Mathf.Clamp(((int)(Player.Instance.Level / 5)) * 10 / 100f, 0, 0.3f);
        }
        return 0f;
    }
    public static float Gold_GainRate()
    {
        switch (Player.Instance.profession)
        {
            case Profession.Thieves:
                return 0.5f;
        }
        return 0f;
    }
    public static int ProjectilesNum()
    {
        return 0;
    }
    public static float FinalDamage()
    {
        switch (Player.Instance.profession)
        {
            case Profession.Berserker:
                return (TotalAttribute.Max_Hp - Player.Instance.Cur_Hp) / 100f;
            case Profession.Destoryer:
                return Mathf.Clamp(((int)(Player.Instance.Level / 5)) * 3 / 100f, 0, 0.3f);
        }
        return 0f;
    }
    public static float ExtraDamage()
    {
        switch (Player.Instance.profession)
        {
            case Profession.Berserker:
                return 0.15f;
        }
        return 0f;
    }
    public static float AdditionalDamage()
    {
        switch (Player.Instance.profession)
        {
            case Profession.Believer:
                return (TotalAttribute.Max_Hp) * (0.5f + Mathf.Clamp((((int)Player.Instance.Level / 5) * 5) / 100, 0, 0.5f));
        }
        return 0f;
    }
}

public class Player : CharacterBaseAttribute
{
    public Profession profession;

    private Transform Unit000;
    private Animator anim;
    private Rigidbody2D rigid;
    private Weapon weapon;
    private Damageable damageable;

    private Vector2 input;
    private float timer;
    private bool death;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        Unit000 = transform.Find("Unit000").GetComponent<Transform>();
        anim = Unit000.GetComponentInChildren<Animator>();
        weapon = GetComponentInChildren<Weapon>();

        damageable = GetComponent<Damageable>(); 
        damageable.invinciableTime = 0.5f;
        damageable.onHurtStart.AddListener(OnHurtStart);
        damageable.onHurtEnd.AddListener(OnHurtEnd);
        damageable.onDeath.AddListener(OnDeath);
    }

    private void Update()
    {
        if (death) return;
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        AnimatorControl();
        AutoAttack();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (death)
        {
            rigid.velocity = Vector3.zero;
            return;
        }
        rigid.velocity = input.normalized * MoveSpeed;
    }

    private void AnimatorControl()
    {
        FlipControl();
        if (input != Vector2.zero)
        {
            anim.SetFloat("RunState", 0.5f);
        }
        else
        {
            anim.SetFloat("RunState", 0f);
        }
    }

    private void FlipControl()
    {
        if (input.x > 0)
        {
            Unit000.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (input.x < 0)
        {
            Unit000.eulerAngles = Vector3.zero;
        }
    }

    private void AutoAttack()
    {
        if (Input.GetMouseButtonDown(1))
            weapon.Attack(AttackSpeed);
        //timer += Time.deltaTime;
        //if (timer >= weapon.AttackInterval)
        //{
        //    timer = 0f;
        //    weapon.Attack(AttackSpeed);
        //}
    }

    private void OnHurtStart(Damageable damageable,DamageMessage data)
    {
        anim.SetTrigger("Hurt");
        ObjectPool.Instance.RequestCacheGameObject(PrefabType.DamageText, transform.position + Vector3.up * 1.5f, data.damage);
    }

    private void OnDeath(Damageable damageable,DamageMessage data)
    {
        death = true;
        anim.SetBool("Death", death);
        ObjectPool.Instance.RequestCacheGameObject(PrefabType.DamageText, transform.position + Vector3.up * 1.5f, data.damage);
    }

    private void OnHurtEnd()
    {

    }
}

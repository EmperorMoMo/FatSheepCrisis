using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Profession
{
    None,
    Thieves,
    Berserker,
    Destoryer,
    Believer,
    Scholar,
}

[System.Serializable]
public class TalentSkillData
{
    public float Max_Hp;
    public float Re_Hp;
    public float Armor;
    public float MoveSpeed;
    public float AttackSpeed;
    public float CritChance;
    public float CritDamage;
    public float PickUpRange;
    public float Exp_GainRate;
    public float Gold_GainRate;
    public int ProjectilesNum;
    public float FinalDamage;
    public float ExtraDamage;
    public float AdditionalDamage;

    public void Clear()
    {
        Max_Hp = Re_Hp = Armor = MoveSpeed = AttackSpeed = CritChance = CritDamage = PickUpRange = Exp_GainRate = Gold_GainRate = FinalDamage = ExtraDamage = ProjectilesNum = 0;
    }
}

public class Player : CharacterBaseAttribute
{
    public Profession profession;
    public TalentSkillData talentSkillData;

    private Transform Unit000;
    private Animator anim;
    private Rigidbody2D rigid;
    private Weapon weapon;

    private Vector2 input;
    private float timer;

    private void Awake()
    {
        talentSkillData = new TalentSkillData();

        rigid = GetComponent<Rigidbody2D>();
        Unit000 = transform.Find("Unit000").GetComponent<Transform>();
        anim = Unit000.GetComponentInChildren<Animator>();
        weapon = GetComponentInChildren<Weapon>();
    }

    public override void TalentSkill()
    {
        talentSkillData.Clear();
        switch (profession)
        {
            case Profession.Thieves:
                talentSkillData.MoveSpeed = 0.25f;
                talentSkillData.Gold_GainRate = 0.5f;
                break;
            case Profession.Berserker:
                talentSkillData.ExtraDamage = 0.15f;
                talentSkillData.FinalDamage = (TotalAttribute.Max_Hp - Cur_Hp) / 100f;
                break;
            case Profession.Destoryer:
                talentSkillData.MoveSpeed = -0.2f;
                talentSkillData.FinalDamage = Mathf.Clamp(((int)(Level / 5)) * 3 / 100f, 0, 0.3f);
                talentSkillData.Armor = Mathf.Clamp(((int)(Level / 5)) * 3 / 10f, 0, 3f);
                break;
            case Profession.Believer:
                talentSkillData.Max_Hp = Max_Hp * 0.5f;
                talentSkillData.AdditionalDamage = (TotalAttribute.Max_Hp) * (0.5f + Mathf.Clamp((((int)Level / 5) * 5) / 100, 0, 0.5f));
                break;
            case Profession.Scholar:
                talentSkillData.Exp_GainRate = 0.2f + Mathf.Clamp(((int)(Level / 5)) * 10 / 100f, 0, 0.3f);
                break;
        }
    }

    private void Update()
    {
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
        timer += Time.unscaledDeltaTime;
        if (timer > Instance.AttackSpeed * 2.5f)
        {
            timer = 0f;
            weapon.Attack();
        }
    }
}

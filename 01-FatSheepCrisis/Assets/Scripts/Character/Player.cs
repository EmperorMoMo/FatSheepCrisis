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
    public static Player instance;
    public Profession profession;
    public TalentSkillData talentSkillData;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        talentSkillData = new TalentSkillData();
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
}

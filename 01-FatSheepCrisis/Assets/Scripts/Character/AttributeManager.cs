using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TotalAttribute
{
    public static int ProjectlesNum => Player.Instance.ProjectilesNum + GreenAttribute.ProjectilesNum;
    public static float Max_Hp => Player.Instance.Max_Hp + GreenAttribute.Max_Hp;
    public static float Re_Hp => Player.Instance.Re_Hp + GreenAttribute.Re_Hp;
    public static float Armor => Player.Instance.Armor + GreenAttribute.Armor;
    public static float MoveSpeed => Player.Instance.MoveSpeed + GreenAttribute.MoveSpeed;
    public static float AttackSpeed => Player.Instance.AttackSpeed + GreenAttribute.AttackSpeed;
    public static float CritChance => Mathf.Clamp(Player.Instance.CritChance + GreenAttribute.CritChance, 0, 0.9f);
    public static float CritDamage => Player.Instance.CritDamage + GreenAttribute.CritDamage;
    public static float PickUpRange => Player.Instance.PickUpRange + GreenAttribute.PickUpRange;
    public static float Exp_GainRate => Player.Instance.Exp_GainRate + GreenAttribute.Exp_GainRate;
    public static float Gold_GainRate => Player.Instance.Gold_GainRate + GreenAttribute.Gold_GainRate;
    public static float FinalDamage => Player.Instance.FinalDamage + GreenAttribute.FinalDamage;
    public static float ExtraDamage => Player.Instance.ExtraDamage + GreenAttribute.ExtraDamage;
    public static float AdditionalDamage => Player.Instance.AdditionalDamage + GreenAttribute.AdditionalDamage;
}

public static class GreenAttribute
{
    public static int ProjectilesNum => TalentSkillData.ProjectilesNum();
    public static float Max_Hp => TalentSkillData.Max_Hp();
    public static float Re_Hp => TalentSkillData.Re_Hp();
    public static float Armor => TalentSkillData.Armor();
    public static float MoveSpeed => TalentSkillData.MoveSpeed();
    public static float AttackSpeed => TalentSkillData.AttackSpeed();
    public static float CritChance => TalentSkillData.CritChance();
    public static float CritDamage => TalentSkillData.CritDamage();
    public static float PickUpRange => TalentSkillData.PickUpRange();
    public static float Exp_GainRate => TalentSkillData.Exp_GainRate();
    public static float Gold_GainRate => TalentSkillData.Gold_GainRate();
    public static float FinalDamage => TalentSkillData.FinalDamage();
    public static float ExtraDamage => TalentSkillData.ExtraDamage();
    public static float AdditionalDamage => TalentSkillData.AdditionalDamage();
}


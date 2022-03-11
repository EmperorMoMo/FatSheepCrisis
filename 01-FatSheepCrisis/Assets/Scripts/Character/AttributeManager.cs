using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TotalAttribute
{
    public static int ProjectlesNum => Singleton<Player>.Instance.ProjectilesNum + GreenAttribute.ProjectilesNum;
    public static float Max_Hp => Singleton<Player>.Instance.Max_Hp + GreenAttribute.Max_Hp;
    public static float Re_Hp => Singleton<Player>.Instance.Re_Hp + GreenAttribute.Re_Hp;
    public static float Armor => Singleton<Player>.Instance.Armor + GreenAttribute.Armor;
    public static float MoveSpeed => Singleton<Player>.Instance.MoveSpeed + GreenAttribute.MoveSpeed;
    public static float AttackSpeed => Singleton<Player>.Instance.AttackSpeed + GreenAttribute.AttackSpeed;
    public static float CritChance => Singleton<Player>.Instance.CritChance + GreenAttribute.CritChance;
    public static float CritDamage => Singleton<Player>.Instance.CritDamage + GreenAttribute.CritDamage;
    public static float PickUpRange => Singleton<Player>.Instance.PickUpRange + GreenAttribute.PickUpRange;
    public static float Exp_GainRate => Singleton<Player>.Instance.Exp_GainRate + GreenAttribute.Exp_GainRate;
    public static float Gold_GainRate => Singleton<Player>.Instance.Gold_GainRate + GreenAttribute.Gold_GainRate;
    public static float FinalDamage => Singleton<Player>.Instance.FinalDamage + GreenAttribute.FinalDamage;
    public static float ExtraDamage => Singleton<Player>.Instance.ExtraDamage + GreenAttribute.ExtraDamage;
    public static float AdditionalDamage => Singleton<Player>.Instance.AdditionalDamage + GreenAttribute.AdditionalDamage;
}
public static class GreenAttribute
{
    public static int ProjectilesNum = 0;
    public static float Max_Hp = 0;
    public static float Re_Hp = 0;
    public static float Armor = 0;
    public static float MoveSpeed = 0;
    public static float AttackSpeed = 0;
    public static float CritChance = 0;
    public static float CritDamage = 0;
    public static float PickUpRange = 0;
    public static float Exp_GainRate = 0;
    public static float Gold_GainRate = 0;
    public static float FinalDamage = 0;
    public static float ExtraDamage = 0;
    public static float AdditionalDamage = 0;
}

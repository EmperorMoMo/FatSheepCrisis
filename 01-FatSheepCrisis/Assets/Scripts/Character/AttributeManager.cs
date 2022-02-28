using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TotalAttribute
{
    public static int ProjectlesNum => Player.instance.ProjectilesNum + GreenAttribute.ProjectilesNum;
    public static float Max_Hp => Player.instance.Max_Hp + GreenAttribute.Max_Hp;
    public static float Re_Hp => Player.instance.Re_Hp + GreenAttribute.Re_Hp;
    public static float Armor => Player.instance.Armor + GreenAttribute.Armor;
    public static float MoveSpeed => Player.instance.MoveSpeed + GreenAttribute.MoveSpeed;
    public static float AttackSpeed => Player.instance.AttackSpeed + GreenAttribute.AttackSpeed;
    public static float CritChance => Player.instance.CritChance + GreenAttribute.CritChance;
    public static float CritDamage => Player.instance.CritDamage + GreenAttribute.CritDamage;
    public static float PickUpRange => Player.instance.PickUpRange + GreenAttribute.PickUpRange;
    public static float Exp_GainRate => Player.instance.Exp_GainRate + GreenAttribute.Exp_GainRate;
    public static float Gold_GainRate => Player.instance.Gold_GainRate + GreenAttribute.Gold_GainRate;
    public static float FinalDamage => Player.instance.FinalDamage + GreenAttribute.FinalDamage;
    public static float ExtraDamage => Player.instance.ExtraDamage + GreenAttribute.ExtraDamage;
    public static float AdditionalDamage => Player.instance.AdditionalDamage + GreenAttribute.AdditionalDamage;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Damageable))]
public abstract class CharacterBaseAttribute : MonoBehaviour
{
    protected internal int Level = 1;
    protected internal float Cur_Hp { get; set; }

    protected internal float Max_Hp => float.Parse(Player.Instance.professionData.Max_Hp);
    protected internal float Re_Hp => float.Parse(Player.Instance.professionData.Re_Hp);
    protected internal float Armor => float.Parse(Player.Instance.professionData.Armor);
    protected internal float MoveSpeed => float.Parse(Player.Instance.professionData.MoveSpeed);
    protected internal float AttackSpeed => float.Parse(Player.Instance.professionData.AttackSpeed);
    protected internal float CritChance => float.Parse(Player.Instance.professionData.CritChance);
    protected internal float CritDamage => float.Parse(Player.Instance.professionData.CritDamage);
    protected internal float PickUpRange => float.Parse(Player.Instance.professionData.PickUpRange);
    protected internal float Exp_GainRate => float.Parse(Player.Instance.professionData.Exp_GainRate);
    protected internal float Gold_GainRate => float.Parse(Player.Instance.professionData.Gold_GainRate);
    protected internal int ProjectilesNum => int.Parse(Player.Instance.professionData.ProjectilesNum);
    protected internal float FinalDamage => float.Parse(Player.Instance.professionData.FinalDamage);
    protected internal float ExtraDamage => float.Parse(Player.Instance.professionData.ExtraDamage);
    protected internal float AdditionalDamage => float.Parse(Player.Instance.professionData.AdditionalDamage);


}

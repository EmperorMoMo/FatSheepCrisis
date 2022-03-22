using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Damageable))]
public abstract class CharacterBaseAttribute : Singleton<Player>
{
    protected internal int Level { get; set; }
    protected internal float Cur_Hp { get; set; }

    protected internal float Max_Hp => float.Parse(Instance.professionData.Max_Hp);
    protected internal float Re_Hp => float.Parse(Instance.professionData.Re_Hp);
    protected internal float Armor => float.Parse(Instance.professionData.Armor);
    protected internal float MoveSpeed => float.Parse(Instance.professionData.MoveSpeed);
    protected internal float AttackSpeed => float.Parse(Instance.professionData.AttackSpeed);
    protected internal float CritChance => float.Parse(Instance.professionData.CritChance);
    protected internal float CritDamage => float.Parse(Instance.professionData.CritDamage);
    protected internal float PickUpRange => float.Parse(Instance.professionData.PickUpRange);
    protected internal float Exp_GainRate => float.Parse(Instance.professionData.Exp_GainRate);
    protected internal float Gold_GainRate => float.Parse(Instance.professionData.Gold_GainRate);
    protected internal int ProjectilesNum => int.Parse(Instance.professionData.ProjectilesNum);
    protected internal float FinalDamage => float.Parse(Instance.professionData.FinalDamage);
    protected internal float ExtraDamage => float.Parse(Instance.professionData.ExtraDamage);
    protected internal float AdditionalDamage => float.Parse(Instance.professionData.AdditionalDamage);

    public CharacterBaseAttribute()
    {
        Level = 1;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBaseAttribute : MonoBehaviour
{
    protected internal int Level { get; set; }
    protected internal float Cur_Hp { get; set; }

    protected internal float Max_Hp { get; set; }
    protected internal float Re_Hp { get; set; }
    protected internal float Armor { get; set; }
    protected internal float MoveSpeed { get; set; }
    protected internal float AttackSpeed { get; set; }
    protected internal float CritChance { get; set; }
    protected internal float CritDamage { get; set; }
    protected internal float PickUpRange { get; set; }
    protected internal float Exp_GainRate { get; set; }
    protected internal float Gold_GainRate { get; set; }
    protected internal int ProjectilesNum { get; set; }
    protected internal float FinalDamage { get; set; }
    protected internal float ExtraDamage { get; set; }
    protected internal float AdditionalDamage { get; set; }

    public CharacterBaseAttribute()
    {
        Level = 1;
        Cur_Hp = 100;

        Max_Hp = 100;
        Re_Hp = 0.5f;
        Armor = 2;
        MoveSpeed = 1;
        AttackSpeed = 1;
        CritChance = 0.1f;
        CritDamage = 0.5f;
        PickUpRange = 1;
        Exp_GainRate = 1;
        ProjectilesNum = 1;
        FinalDamage = 1;
        ExtraDamage = 1;
        AdditionalDamage = 1;
    }

    public abstract void TalentSkill();
}

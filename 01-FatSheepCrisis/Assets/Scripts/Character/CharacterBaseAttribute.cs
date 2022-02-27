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
    protected internal float ProjectilesNum { get; set; }
    protected internal float FinalDamage { get; set; }
    protected internal float ExtraDamage { get; set; }

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
        Exp_GainRate = 0;
        ProjectilesNum = 0;
        FinalDamage = 0;
        ExtraDamage = 0;
    }

    public abstract void TalentSkill();
}

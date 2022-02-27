using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berserker : CharacterBaseAttribute
{
    void Awake()
    {

    }

    public override void TalentSkill()
    {
        ExtraDamage = 0.15f;
        FinalDamage = (Max_Hp - Cur_Hp) / 100;
    }
}

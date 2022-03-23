using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBaseAttribute : MonoBehaviour
{
    protected internal float Aggressivity { get; set; }
    protected internal float AttackInterval { get; set; }
    protected internal float CritChance { get; set; }
    protected internal float CritDamage { get; set; }
    protected internal float RepelNum { get; set; }
    protected internal int ProjectilesNum { get; set; }
    protected internal float AttackRange { get; set; }
    protected internal string SkillDescription { get; set; }
    
    public abstract void WeaponSkill();
    public abstract void SetAttribute(int id);
}

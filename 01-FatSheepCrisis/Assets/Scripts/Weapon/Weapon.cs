using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponName
{
    None,
    Ìú¸«=1001,
}

public class Weapon : WeaponBaseAttribute
{
    public WeaponName Name;

    private Animator anim;
    private void Awake()
    {
        SetAttribute();

        anim = GetComponent<Animator>();
    }
    public override void SetAttribute()
    {
        PackageItem weapons = Resources.Load<PackageItem>("WeaponConfig");
        Dictionary<string, ItemData> Data = weapons.GetItems();
        foreach (var item in Data.Values)
        {
            if (int.Parse(item.Id) == (int)Name)
            {
                Aggressivity = float.Parse(item.Aggressivity);
                AttackSpeed = float.Parse(item.AttackSpeed);
                CritChance = float.Parse(item.CritChance);
                CritDamage = float.Parse(item.CritDamage);
                RepelNum = float.Parse(item.RepelNum);
                ProjectilesNum = int.Parse(item.ProjectilesNum);
                AttackRange = float.Parse(item.AttackRange);
                SkillDescription = item.SkillDescription;
            }
        }
    }
    public override void WeaponSkill()
    {
        throw new System.NotImplementedException();
    }

    public void Attack()
    {
        anim.SetTrigger("attack");
    }
}

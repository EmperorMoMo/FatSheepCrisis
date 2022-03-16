using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponData
{
    public string Id;
    public string Name;
    public string Aggressivity;
    public string AttackSpeed;
    public string CritChance;
    public string CritDamage;
    public string RepelNum;
    public string ProjectilesNum;
    public string AttackRange;
    public string SkillDescription;
}

[System.Serializable]
public class EnemyData
{
    public string Id;
    public string Name;
    public string Aggressivity;
    public string Armor;
    public string Max_Hp;
    public string MoveSpeed;
    public string DefenseRepelNum;
}

public class PackageItem : ScriptableObject
{
    public List<WeaponData> weapons;
    public List<EnemyData> enemyes;
    public Dictionary<string, WeaponData> dicItem1 = new Dictionary<string, WeaponData>();
    public Dictionary<string, EnemyData> dicItem2 = new Dictionary<string, EnemyData>();
    public Dictionary<string,WeaponData> GetWeaponsData()
    {
        dicItem1.Clear();
        for (int i = 0; i < weapons.Count; i++)
        {
            dicItem1.Add(weapons[i].Id, weapons[i]);
        }
        return dicItem1;
    }
    public Dictionary<string,EnemyData> GetEnemyesData()
    {
        dicItem2.Clear();
        for (int i = 0; i < enemyes.Count; i++)
        {
            dicItem2.Add(enemyes[i].Id, enemyes[i]);
        }
        return dicItem2;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponData
{
    public string Id;
    public string Name;
    public string Type;
    public string Aggressivity;
    public string AttackInterval;
    public string CritChance;
    public string CritDamage;
    public string RepelNum;
    public string ProjectilesNum;
    public string AttackRange;
    public string SkillDescription;
    public string Quality;
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

[System.Serializable]
public class TicketData
{
    public string Id;
    public string Name;
    public string Probability;
    public string Quality;
}

[System.Serializable]
public class ProfessionData
{
    public string Id;
    public string Name;
    public string Max_Hp;
    public string Re_Hp;
    public string Armor;
    public string MoveSpeed;
    public string AttackSpeed;
    public string CritChance;
    public string CritDamage;
    public string PickUpRange;
    public string Exp_GainRate;
    public string Gold_GainRate;
    public string ProjectilesNum;
    public string FinalDamage;
    public string ExtraDamage;
    public string AdditionalDamage;
    public string WeaponType;
    public string Introduce;
    public string Weapon;
}

[System.Serializable]
public class SkillData
{
    public string Id;
    public string Name;
    public string AttackRatio;
    public string CoolTime;
    public string Range;
    public string Distance;
    public string MoveSpeed;
    public string DurationTime;
    public string BuffDurationTime;
    public string RepelNum;
    public string Description;
}

public class PackageItem : ScriptableObject
{
    public List<WeaponData> weapons;
    public List<EnemyData> enemyes;
    public List<TicketData> tickets;
    public List<ProfessionData> professions;
    public List<SkillData> skills;
    public Dictionary<string, WeaponData> dicItem1 = new Dictionary<string, WeaponData>();
    public Dictionary<string, EnemyData> dicItem2 = new Dictionary<string, EnemyData>();
    public Dictionary<string, TicketData> dicItem3 = new Dictionary<string, TicketData>();
    public Dictionary<string, ProfessionData> dicItem4 = new Dictionary<string, ProfessionData>();
    public Dictionary<string, SkillData> dicItem5 = new Dictionary<string, SkillData>();
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
    public Dictionary<string, TicketData> GetTicketData()
    {
        dicItem3.Clear();
        for (int i = 0; i < tickets.Count; i++)
        {
            dicItem3.Add(tickets[i].Id, tickets[i]);
        }
        return dicItem3;
    }
    public Dictionary<string, ProfessionData> GetProfessionData()
    {
        dicItem4.Clear();
        for (int i = 0; i < professions.Count; i++)
        {
            dicItem4.Add(professions[i].Id, professions[i]);
        }
        return dicItem4;
    }
    public Dictionary<string, SkillData> GetSkillData()
    {
        dicItem5.Clear();
        for (int i = 0; i < skills.Count; i++)
        {
            dicItem5.Add(skills[i].Id, skills[i]);
        }
        return dicItem5;
    }
}

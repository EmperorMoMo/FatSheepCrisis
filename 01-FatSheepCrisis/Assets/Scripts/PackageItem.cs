using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
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

public class PackageItem : ScriptableObject
{
    public List<ItemData> items;
    public Dictionary<string, ItemData> dicItem = new Dictionary<string, ItemData>();
    public Dictionary<string,ItemData> GetItems()
    {
        dicItem.Clear();
        for (int i = 0; i < items.Count; i++)
        {
            dicItem.Add(items[i].Id, items[i]);
        }
        return dicItem;
    }
}

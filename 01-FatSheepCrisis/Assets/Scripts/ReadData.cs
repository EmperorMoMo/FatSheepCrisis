using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadData : MonoBehaviour
{
    private Dictionary<string, ItemData> levels;
    private void Start()
    {
        PackageItem level = Resources.Load<PackageItem>("levelRegular");
        levels = level.GetItems();
        foreach (var item in levels)
        {
            //Debug.Log(string.Format("ID:{0},ÀàÐÍ:{1},Ãû×Ö:{2},¹¥»÷:{3}", item.Value.id, item.Value.type, item.Value.realname, item.Value.attack));
        }
    }
}

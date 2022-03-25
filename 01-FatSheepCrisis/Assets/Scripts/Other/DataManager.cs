using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private Dictionary<string, ProfessionData> Data;
    private PackageItem Profession;

    [MenuItem("Tool/Delete Player Info")]
    public static void DeletePlayerInfo()
    {
        PlayerPrefs.DeleteAll();
    }

    private void Awake()
    {
        Profession = Resources.Load<PackageItem>("Config");
        Data = Profession.GetProfessionData();

    }
    public ProfessionData ReadPlayerData(string key)
    {
        ProfessionData professionData=new ProfessionData();
        string str = Data[key].Name + "_";
        professionData.Id = PlayerPrefs.GetString(str + "ID", Data[key].Id);
        professionData.Name = PlayerPrefs.GetString(str + "Name", Data[key].Name);
        professionData.Max_Hp = PlayerPrefs.GetString(str+ "Max_HP", Data[key].Max_Hp);
        professionData.Re_Hp = PlayerPrefs.GetString(str + "Re_Hp", Data[key].Re_Hp);
        professionData.Armor = PlayerPrefs.GetString(str + "Armor", Data[key].Armor);
        professionData.MoveSpeed = PlayerPrefs.GetString(str + "MoveSpeed", Data[key].MoveSpeed);
        professionData.AttackSpeed = PlayerPrefs.GetString(str + "AttackSpeed", Data[key].AttackSpeed);
        professionData.CritChance = PlayerPrefs.GetString(str + "CritChance", Data[key].CritChance);
        professionData.CritDamage = PlayerPrefs.GetString(str + "CritDamage", Data[key].CritDamage);
        professionData.PickUpRange = PlayerPrefs.GetString(str + "PickUpRange", Data[key].PickUpRange);
        professionData.Exp_GainRate = PlayerPrefs.GetString(str + "Exp_GainRate", Data[key].Exp_GainRate);
        professionData.Gold_GainRate = PlayerPrefs.GetString(str + "Gold_GainRate", Data[key].Gold_GainRate);
        professionData.ProjectilesNum = PlayerPrefs.GetString(str + "ProjectilesNum", Data[key].ProjectilesNum);
        professionData.FinalDamage = PlayerPrefs.GetString(str + "FinalDamage", Data[key].FinalDamage);
        professionData.ExtraDamage = PlayerPrefs.GetString(str + "ExtraDamage", Data[key].ExtraDamage);
        professionData.AdditionalDamage = PlayerPrefs.GetString(str + "AdditionDamage", Data[key].AdditionalDamage);
        professionData.WeaponType = PlayerPrefs.GetString(str + "WeaponType", Data[key].WeaponType);
        professionData.Introduce = PlayerPrefs.GetString(str + "Introduce", Data[key].Introduce);
        professionData.Weapon = PlayerPrefs.GetString(str + "Weapon", Data[key].Weapon);
        //Player.Instance.Cur_Hp = float.Parse(professionData.Max_Hp);
        return professionData;
    }

    public void SavePlayerData(string key,ProfessionData professionData)
    {
        string str = Data[key].Name + "_";
        PlayerPrefs.SetString(str + "Max_HP", professionData.Max_Hp);
        PlayerPrefs.SetString(str + "Re_Hp", professionData.Re_Hp);
        PlayerPrefs.SetString(str + "Armor", professionData.Armor);
        PlayerPrefs.SetString(str + "MoveSpeed", professionData.MoveSpeed);
        PlayerPrefs.SetString(str + "AttackSpeed", professionData.AttackSpeed);
        PlayerPrefs.SetString(str + "CritChance", professionData.CritChance);
        PlayerPrefs.SetString(str + "CritDamage", professionData.CritDamage);
        PlayerPrefs.SetString(str + "PickUpRange", professionData.PickUpRange);
        PlayerPrefs.SetString(str + "Exp_GainRate", professionData.Exp_GainRate);
        PlayerPrefs.SetString(str + "Gold_GainRate", professionData.Gold_GainRate);
        PlayerPrefs.SetString(str + "ProjectilesNum", professionData.ProjectilesNum);
        PlayerPrefs.SetString(str + "FinalDamage", professionData.FinalDamage);
        PlayerPrefs.SetString(str + "ExtraDamage", professionData.ExtraDamage);
        PlayerPrefs.SetString(str + "AdditionDamage", professionData.AdditionalDamage);
        PlayerPrefs.SetString(str + "Weapon", professionData.Weapon);
        //if (PlayerPrefs.GetString(str + "Weapon", Data[key].Weapon) != professionData.Weapon)
        //{
        //    string[] weapons = GetProfessionWeapons(PlayerPrefs.GetString(str + "Weapon", Data[key].Weapon));
        //    bool newWeapon = true;
        //    for (int i = 0; i < weapons.Length; i++)
        //    {
        //        if (weapons[i] == professionData.Weapon)
        //        {
        //            newWeapon = false;
        //            break;
        //        }
        //    }
        //    if (newWeapon)
        //    {
        //        PlayerPrefs.SetString(str + "Weapon", PlayerPrefs.GetString(str + "Weapon", Data[key].Weapon) + "/" + professionData.Weapon);
        //    }
        //}
    }

    public string[] GetPlayerData(string str)
    {
        string[] s = str.Split('/');
        return s;
    }

    public string[] ReadPlayerWeaponsData()
    {
        return GetPlayerData(PlayerPrefs.GetString("Weapons"));
    }

    public void SavePlayerWeaponsData(string weapons)
    {
        string[] str = ReadPlayerWeaponsData();
        bool newWeapons = true;
        for (int i = 0; i < str.Length; i++)
        {
            if(str[i]==weapons)
            {
                newWeapons = false;
                break;
            }
        }
        if (newWeapons)
        {
            if (!PlayerPrefs.HasKey("Weapons"))
            {
                PlayerPrefs.SetString("Weapons", weapons);
            }
            else
            {
                PlayerPrefs.SetString("Weapons", PlayerPrefs.GetString("Weapons") + "/" + weapons);
            }
        }
    }

    public string[] ReadPlayerProfessionsData()
    {
        return GetPlayerData(PlayerPrefs.GetString("Professions"));
    }

    public void SavePlayerProfessionsData(string professions)
    {
        string[] str = ReadPlayerProfessionsData();
        bool newProfessions = true;
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == professions)
            {
                newProfessions = false;
                break;
            }
        }
        if (newProfessions)
        {
            if (!PlayerPrefs.HasKey("Professions"))
            {
                PlayerPrefs.SetString("Professions", professions);
            }
            else
            {
                PlayerPrefs.SetString("Professions", PlayerPrefs.GetString("Professions") + "/" + professions);
            }
        }
    }

    public int ReadPlayerGoldData()
    {
        return PlayerPrefs.GetInt("Gold", 0);
    }

    public void SavePlayerGoldData(int gold)
    {
        PlayerPrefs.SetInt("Gold", gold);
    }

    public int ReadPlayerTicketsData()
    {
        return PlayerPrefs.GetInt("Tickets", 0);
    }

    public void SavePlayerTicketsData(int tickets)
    {
        PlayerPrefs.SetInt("Tickets", tickets);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private Dictionary<string, ProfessionData> Data;
    private PackageItem Profession;
<<<<<<< Updated upstream

    [MenuItem("Tool/Delete Player Info")]
    public static void DeletePlayerInfo()
    {
        PlayerPrefs.DeleteAll();
    }

    private void Start()
=======
    private void Awake()
>>>>>>> Stashed changes
    {
        Profession = Resources.Load<PackageItem>("Config");
        Data = Profession.GetProfessionData();

    }
    public ProfessionData ReadPlayerData(string key)
    {
        ProfessionData professionData=new ProfessionData();
        string str = Data[key].Name + "_";
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
    }
}

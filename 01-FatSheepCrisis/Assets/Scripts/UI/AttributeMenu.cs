using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributeMenu : UIMenuBase
{
    public GameObject[] HP;
    private float HP_promoteValue = 20;
    public GameObject[] HPRecovery;
    private float HPRecovery_promoteValue = 0.5f;
    public GameObject[] Armor;
    private float Armor_promoteValue = 0.5f;
    public GameObject[] MoveSpeed;
    private float MoveSpeed_promoteValue = 0.2f;
    public GameObject[] AttackSpeed;
    private float AttackSpeed_promoteValue = 0.1f;
    public GameObject[] Crit;
    private float Crit_promoteValue = 0.05f;
    public GameObject[] CritDamage;
    private float CritDamage_promoteValue = 0.1f;
    public GameObject[] PickUpRange;
    private float PickUpRange_promoteValue = 0.2f;
    public GameObject[] ExtraExperience;
    private float ExtraExperience_promoteValue = 0.1f;
    public GameObject[] ExtraGoldCoin;
    private float ExtraGoldCoin_promoteValue = 0.1f;
    public GameObject[] ProjectileNumber;
    private float ProjectileNumber_promoteValue = 1;
    private Dictionary<string, int> promoteCountDic = new Dictionary<string, int>();
    private MainMenu mainMenu;

    public override void Setup()
    {
        base.Setup();
    }
    public override void Init()
    {
        base.Init();
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 0, 1);
        gameObject.GetComponent<RectTransform>().DOScaleY(1.0f, 0.25f).SetEase(Ease.OutBack);
        mainMenu = UIManager.Instance.GetMenu(MenuType.MainMenu) as MainMenu;
        ReadPromoteData(mainMenu.currentProfession);
    }

    public void ReadPromoteData(string profession)
    {
        promoteCountDic.Add("HP", PlayerPrefs.GetInt(profession+"_"+"HP_Promote_Count", 0));
        promoteCountDic.Add("HPRecovery", PlayerPrefs.GetInt(profession + "_" + "HPRecovery_Promote_Count", 0));
        promoteCountDic.Add("Armor", PlayerPrefs.GetInt(profession + "_" + "Armor_Promote_Count", 0));
        promoteCountDic.Add("MoveSpeed", PlayerPrefs.GetInt(profession + "_" + "MoveSpeed_Promote_Count", 0));
        promoteCountDic.Add("AttackSpeed", PlayerPrefs.GetInt(profession + "_" + "AttackSpeed_Promote_Count", 0));
        promoteCountDic.Add("Crit", PlayerPrefs.GetInt(profession + "_" + "Crit_Promote_Count", 0));
        promoteCountDic.Add("CritDamage", PlayerPrefs.GetInt(profession + "_" + "CritDamage_Promote_Count", 0));
        promoteCountDic.Add("PickUpRange", PlayerPrefs.GetInt(profession + "_" + "PickUpRange_Promote_Count", 0));
        promoteCountDic.Add("ExtraExperience", PlayerPrefs.GetInt(profession + "_" + "ExtraExperience_Promote_Count", 0));
        promoteCountDic.Add("ExtraGoldCoin", PlayerPrefs.GetInt(profession + "_" + "ExtraGoldCoin_Promote_Count", 0));
        promoteCountDic.Add("ProjectileNumber", PlayerPrefs.GetInt(profession + "_" + "ProjectileNumber_Promote_Count", 0));
        foreach (var item in promoteCountDic)
        {
            InitAttributeMenu(item.Key, item.Value);
        }
    }

    public void InitAttributeMenu(string name,int count)
    {
        switch (name)
        {
            case "HP":
                for (int i = 0; i < HP.Length; i++)
                {
                    HP[i].GetComponent<Image>().color = Color.white;
                }
                for (int i = 0; i < count; i++)
                {
                    HP[i].GetComponent<Image>().color = Color.yellow;
                }
                break;
            case "HPRecovery":
                for (int i = 0; i < HPRecovery.Length; i++)
                {
                    HPRecovery[i].GetComponent<Image>().color = Color.white;
                }
                for (int i = 0; i < count; i++)
                {
                    HPRecovery[i].GetComponent<Image>().color = Color.yellow;
                }
                break;
            case "Armor":
                for (int i = 0; i < Armor.Length; i++)
                {
                    Armor[i].GetComponent<Image>().color = Color.white;
                }
                for (int i = 0; i < count; i++)
                {
                    Armor[i].GetComponent<Image>().color = Color.yellow;
                }
                break;
            case "MoveSpeed":
                for (int i = 0; i < MoveSpeed.Length; i++)
                {
                    MoveSpeed[i].GetComponent<Image>().color = Color.white;
                }
                for (int i = 0; i < count; i++)
                {
                    MoveSpeed[i].GetComponent<Image>().color = Color.yellow;
                }
                break;
            case "AttackSpeed":
                for (int i = 0; i < AttackSpeed.Length; i++)
                {
                    AttackSpeed[i].GetComponent<Image>().color = Color.white;
                }
                for (int i = 0; i < count; i++)
                {
                    AttackSpeed[i].GetComponent<Image>().color = Color.yellow;
                }
                break;
            case "Crit":
                for (int i = 0; i < Crit.Length; i++)
                {
                    Crit[i].GetComponent<Image>().color = Color.white;
                }
                for (int i = 0; i < count; i++)
                {
                    Crit[i].GetComponent<Image>().color = Color.yellow;
                }
                break;
            case "CritDamage":
                for (int i = 0; i < CritDamage.Length; i++)
                {
                    CritDamage[i].GetComponent<Image>().color = Color.white;
                }
                for (int i = 0; i < count; i++)
                {
                    CritDamage[i].GetComponent<Image>().color = Color.yellow;
                }
                break;
            case "PickUpRange":
                for (int i = 0; i < PickUpRange.Length; i++)
                {
                    PickUpRange[i].GetComponent<Image>().color = Color.white;
                }
                for (int i = 0; i < count; i++)
                {
                    PickUpRange[i].GetComponent<Image>().color = Color.yellow;
                }
                break;
            case "ExtraExperience":
                for (int i = 0; i < ExtraExperience.Length; i++)
                {
                    ExtraExperience[i].GetComponent<Image>().color = Color.white;
                }
                for (int i = 0; i < count; i++)
                {
                    ExtraExperience[i].GetComponent<Image>().color = Color.yellow;
                }
                break;
            case "ExtraGoldCoin":
                for (int i = 0; i < ExtraGoldCoin.Length; i++)
                {
                    ExtraGoldCoin[i].GetComponent<Image>().color = Color.white;
                }
                for (int i = 0; i < count; i++)
                {
                    ExtraGoldCoin[i].GetComponent<Image>().color = Color.yellow;
                }
                break;
            case "ProjectileNumber":
                for (int i = 0; i < ProjectileNumber.Length; i++)
                {
                    ProjectileNumber[i].GetComponent<Image>().color = Color.white;
                }
                for (int i = 0; i < count; i++)
                {
                    ProjectileNumber[i].GetComponent<Image>().color = Color.yellow;
                }
                break;
        }
    }

    public void PromoteHP()
    {
        int i = promoteCountDic["HP"];
        if (i < HP.Length)
        {
            i++;
            InitAttributeMenu("HP", i);
            promoteCountDic["HP"] = i;
            PlayerPrefs.SetInt(mainMenu.currentProfession + "_" + "HP_Promote_Count", i);
            float f = float.Parse(mainMenu.professionData.Max_Hp) + HP_promoteValue;
            mainMenu.professionData.Max_Hp = f.ToString();
            DataManager.Instance.SavePlayerData(mainMenu.currentProfession, mainMenu.professionData);
        }
    }
    public void PromoteHPRecovery()
    {
        int i = promoteCountDic["HPRecovery"];
        if (i < HPRecovery.Length)
        {
            i++;
            InitAttributeMenu("HPRecovery", i);
            promoteCountDic["HPRecovery"] = i;
            PlayerPrefs.SetInt(mainMenu.currentProfession + "_" + "HPRecovery_Promote_Count", i);
            float f = float.Parse(mainMenu.professionData.Re_Hp) + HPRecovery_promoteValue;
            mainMenu.professionData.Re_Hp = f.ToString();
            DataManager.Instance.SavePlayerData(mainMenu.currentProfession, mainMenu.professionData);
        }
    }
    public void PromoteArmor()
    {
        int i = promoteCountDic["Armor"];
        if (i < Armor.Length)
        {
            i++;
            InitAttributeMenu("Armor", i);
            promoteCountDic["Armor"] = i;
            PlayerPrefs.SetInt(mainMenu.currentProfession + "_" + "Armor_Promote_Count", i);
            float f = float.Parse(mainMenu.professionData.Armor) + Armor_promoteValue;
            mainMenu.professionData.Armor = f.ToString();
            DataManager.Instance.SavePlayerData(mainMenu.currentProfession, mainMenu.professionData);
        }
    }
    public void PromoteMoveSpeed()
    {
        int i = promoteCountDic["MoveSpeed"];
        if (i < MoveSpeed.Length)
        {
            i++;
            InitAttributeMenu("MoveSpeed", i);
            promoteCountDic["MoveSpeed"] = i;
            PlayerPrefs.SetInt(mainMenu.currentProfession + "_" + "MoveSpeed_Promote_Count", i);
            float f = float.Parse(mainMenu.professionData.MoveSpeed) + MoveSpeed_promoteValue;
            mainMenu.professionData.MoveSpeed = f.ToString();
            DataManager.Instance.SavePlayerData(mainMenu.currentProfession, mainMenu.professionData);
        }
    }
    public void PromoteAttackSpeed()
    {
        int i = promoteCountDic["AttackSpeed"];
        if (i < AttackSpeed.Length)
        {
            i++;
            InitAttributeMenu("AttackSpeed", i);
            promoteCountDic["AttackSpeed"] = i;
            PlayerPrefs.SetInt(mainMenu.currentProfession + "_" + "AttackSpeed_Promote_Count", i);
            float f = float.Parse(mainMenu.professionData.AttackSpeed) + AttackSpeed_promoteValue;
            mainMenu.professionData.AttackSpeed = f.ToString();
            DataManager.Instance.SavePlayerData(mainMenu.currentProfession, mainMenu.professionData);
        }
    }
    public void PromoteCrit()
    {
        int i = promoteCountDic["Crit"];
        if (i < Crit.Length)
        {
            i++;
            InitAttributeMenu("Crit", i);
            promoteCountDic["Crit"] = i;
            PlayerPrefs.SetInt(mainMenu.currentProfession + "_" + "Crit_Promote_Count", i);
            float f = float.Parse(mainMenu.professionData.CritChance) + Crit_promoteValue;
            mainMenu.professionData.CritChance = f.ToString();
            DataManager.Instance.SavePlayerData(mainMenu.currentProfession, mainMenu.professionData);
        }
    }
    public void PromoteCritDamage()
    {
        int i = promoteCountDic["CritDamage"];
        if (i < CritDamage.Length)
        {
            i++;
            InitAttributeMenu("CritDamage", i);
            promoteCountDic["CritDamage"] = i;
            PlayerPrefs.SetInt(mainMenu.currentProfession + "_" + "CritDamage_Promote_Count", i);
            float f = float.Parse(mainMenu.professionData.CritDamage) + CritDamage_promoteValue;
            mainMenu.professionData.CritDamage = f.ToString();
            DataManager.Instance.SavePlayerData(mainMenu.currentProfession, mainMenu.professionData);
        }
    }
    public void PromotePickUpRange()
    {
        int i = promoteCountDic["PickUpRange"];
        if (i < PickUpRange.Length)
        {
            i++;
            InitAttributeMenu("PickUpRange", i);
            promoteCountDic["PickUpRange"] = i;
            PlayerPrefs.SetInt(mainMenu.currentProfession + "_" + "PickUpRange_Promote_Count", i);
            float f = float.Parse(mainMenu.professionData.PickUpRange) + PickUpRange_promoteValue;
            mainMenu.professionData.PickUpRange = f.ToString();
            DataManager.Instance.SavePlayerData(mainMenu.currentProfession, mainMenu.professionData);
        }
    }
    public void PromoteExtraExperience()
    {
        int i = promoteCountDic["ExtraExperience"];
        if (i < ExtraExperience.Length)
        {
            i++;
            InitAttributeMenu("ExtraExperience", i);
            promoteCountDic["ExtraExperience"] = i;
            PlayerPrefs.SetInt(mainMenu.currentProfession + "_" + "ExtraExperience_Promote_Count", i);
            float f = float.Parse(mainMenu.professionData.Exp_GainRate) + ExtraExperience_promoteValue;
            mainMenu.professionData.Exp_GainRate = f.ToString();
            DataManager.Instance.SavePlayerData(mainMenu.currentProfession, mainMenu.professionData);
        }
    }
    public void PromoteExtraGoldCoin()
    {
        int i = promoteCountDic["ExtraGoldCoin"];
        if (i < ExtraGoldCoin.Length)
        {
            i++;
            InitAttributeMenu("ExtraGoldCoin", i);
            promoteCountDic["ExtraGoldCoin"] = i;
            PlayerPrefs.SetInt(mainMenu.currentProfession + "_" + "ExtraGoldCoin_Promote_Count", i);
            float f = float.Parse(mainMenu.professionData.Gold_GainRate) + ExtraGoldCoin_promoteValue;
            mainMenu.professionData.Gold_GainRate = f.ToString();
            DataManager.Instance.SavePlayerData(mainMenu.currentProfession, mainMenu.professionData);
        }
    }
    public void PromoteProjectileNumber()
    {
        int i = promoteCountDic["ProjectileNumber"];
        if (i < ProjectileNumber.Length)
        {
            i++;
            InitAttributeMenu("ProjectileNumber", i);
            promoteCountDic["ProjectileNumber"] = i;
            PlayerPrefs.SetInt(mainMenu.currentProfession + "_" + "ProjectileNumber_Promote_Count", i);
            float f = float.Parse(mainMenu.professionData.ProjectilesNum) + ProjectileNumber_promoteValue;
            mainMenu.professionData.ProjectilesNum = f.ToString();
            DataManager.Instance.SavePlayerData(mainMenu.currentProfession, mainMenu.professionData);
        }
    }

    public void OnClickToClose()
    {
        mainMenu.ReadProfessionData(mainMenu.currentProfession);
        promoteCountDic.Clear();
        StartCoroutine(CloseIconSetting());
    }

    IEnumerator CloseIconSetting()
    {
        gameObject.GetComponent<RectTransform>().DOScaleY(0f, 0.25f).SetEase(Ease.InBack);
        yield return new WaitForSecondsRealtime(0.25f);
        base.UIResponse_Close();
    }
}

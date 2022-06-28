using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributeMenu : UIMenuBase
{
    public GameObject[] HP;
    public Text gold_HP;
    private GameObject HP_LevelUpBtn;
    private int HP_Cost = 200;
    private float HP_promoteValue = 20;
    public GameObject[] HPRecovery;
    public Text gold_HPRecovery;
    private GameObject HPRecovery_LevelUpBtn;
    private int HPRecovery_Cost = 100;
    private float HPRecovery_promoteValue = 0.5f;
    public GameObject[] Armor;
    public Text gold_Armor;
    private GameObject Armor_LevelUpBtn;
    private int Armor_Cost = 100;
    private float Armor_promoteValue = 0.5f;
    public GameObject[] MoveSpeed;
    public Text gold_MoveSpeed;
    private GameObject MoveSpeed_LevelUpBtn;
    private int MoveSpeed_Cost = 100;
    private float MoveSpeed_promoteValue = 0.2f;
    public GameObject[] AttackSpeed;
    public Text gold_AttackSpeed;
    private GameObject AttackSpeed_LevelUpBtn;
    private int AttackSpeed_Cost = 100;
    private float AttackSpeed_promoteValue = 0.1f;
    public GameObject[] Crit;
    public Text gold_Crit;
    private GameObject Crit_LevelUpBtn;
    private int Crit_Cost = 100;
    private float Crit_promoteValue = 0.05f;
    public GameObject[] CritDamage;
    public Text gold_CritDamage;
    private GameObject CritDamage_LevelUpBtn;
    private int CritDamage_Cost = 100;
    private float CritDamage_promoteValue = 0.1f;
    public GameObject[] PickUpRange;
    public Text gold_PickUpRange;
    private GameObject PickUpRange_LevelUpBtn;
    private int PickUpRange_Cost = 100;
    private float PickUpRange_promoteValue = 0.2f;
    public GameObject[] ExtraExperience;
    public Text gold_ExtraExperience;
    private GameObject ExtraExperience_LevelUpBtn;
    private int ExtraExperience_Cost = 100;
    private float ExtraExperience_promoteValue = 0.1f;
    public GameObject[] ExtraGoldCoin;
    public Text gold_ExtraGoldCoin;
    private GameObject ExtraGoldCoin_LevelUpBtn;
    private int ExtraGoldCoin_Cost = 100;
    private float ExtraGoldCoin_promoteValue = 0.1f;
    public GameObject[] ProjectileNumber;
    public Text gold_ProjectileNumber;
    private GameObject ProjectileNumber_LevelUpBtn;
    private int ProjectileNumber_Cost = 100;
    private float ProjectileNumber_promoteValue = 1;
    private Dictionary<string, int> promoteCountDic = new Dictionary<string, int>();
    private MainMenu mainMenu;
    private TipsMenu tipsMenu;

    public override void Setup()
    {
        base.Setup();
        tipsMenu = UIManager.Instance.GetMenu(MenuType.TipsMenu) as TipsMenu;
        UIManager.Instance.AddOverlayMenu(tipsMenu);
        HP_LevelUpBtn = gold_HP.transform.parent.gameObject;
        HPRecovery_LevelUpBtn = gold_HPRecovery.transform.parent.gameObject;
        Armor_LevelUpBtn = gold_Armor.transform.parent.gameObject;
        MoveSpeed_LevelUpBtn = gold_MoveSpeed.transform.parent.gameObject;
        AttackSpeed_LevelUpBtn = gold_AttackSpeed.transform.parent.gameObject;
        Crit_LevelUpBtn = gold_Crit.transform.parent.gameObject;
        CritDamage_LevelUpBtn = gold_CritDamage.transform.parent.gameObject;
        PickUpRange_LevelUpBtn = gold_PickUpRange.transform.parent.gameObject;
        ExtraExperience_LevelUpBtn = gold_ExtraExperience.transform.parent.gameObject;
        ExtraGoldCoin_LevelUpBtn = gold_ExtraGoldCoin.transform.parent.gameObject;
        ProjectileNumber_LevelUpBtn = gold_ProjectileNumber.transform.parent.gameObject;
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
                    //HP[i].GetComponent<Image>().color = Color.white;
                    HP[i].transform.GetChild(0).gameObject.SetActive(false);
                }
                for (int i = 0; i < count; i++)
                {
                    //HP[i].GetComponent<Image>().color = Color.yellow;
                    HP[i].transform.GetChild(0).gameObject.SetActive(true);
                }
                if (count > 0)
                    HP_Cost = (count + 1) * 200;
                else
                    HP_Cost = 200;
                gold_HP.text = HP_Cost.ToString();
                if (count > 4)
                    HP_LevelUpBtn.SetActive(false);
                else
                    HP_LevelUpBtn.SetActive(true);
                break;
            case "HPRecovery":
                for (int i = 0; i < HPRecovery.Length; i++)
                {
                    //HPRecovery[i].GetComponent<Image>().color = Color.white;
                    HPRecovery[i].transform.GetChild(0).gameObject.SetActive(false);
                }
                for (int i = 0; i < count; i++)
                {
                    //HPRecovery[i].GetComponent<Image>().color = Color.yellow;
                    HPRecovery[i].transform.GetChild(0).gameObject.SetActive(true);
                }
                if (count > 0)
                    HPRecovery_Cost = (count + 1) * 200;
                else
                    HPRecovery_Cost = 200;
                gold_HPRecovery.text = HPRecovery_Cost.ToString();
                if (count > 4)
                    HPRecovery_LevelUpBtn.SetActive(false);
                else
                    HPRecovery_LevelUpBtn.SetActive(true);
                break;
            case "Armor":
                for (int i = 0; i < Armor.Length; i++)
                {
                    //Armor[i].GetComponent<Image>().color = Color.white;
                    Armor[i].transform.GetChild(0).gameObject.SetActive(false);
                }
                for (int i = 0; i < count; i++)
                {
                    //Armor[i].GetComponent<Image>().color = Color.yellow;
                    Armor[i].transform.GetChild(0).gameObject.SetActive(true);
                }
                if (count > 0)
                    Armor_Cost = (count + 1) * 200;
                else
                    Armor_Cost = 200;
                gold_Armor.text = Armor_Cost.ToString();
                if (count > 4)
                    Armor_LevelUpBtn.SetActive(false);
                else
                    Armor_LevelUpBtn.SetActive(true);
                break;
            case "MoveSpeed":
                for (int i = 0; i < MoveSpeed.Length; i++)
                {
                    //MoveSpeed[i].GetComponent<Image>().color = Color.white;
                    MoveSpeed[i].transform.GetChild(0).gameObject.SetActive(false);
                }
                for (int i = 0; i < count; i++)
                {
                    //MoveSpeed[i].GetComponent<Image>().color = Color.yellow;
                    MoveSpeed[i].transform.GetChild(0).gameObject.SetActive(true);
                }
                if (count > 0)
                    MoveSpeed_Cost = (count + 1) * 200;
                else
                    MoveSpeed_Cost = 200;
                gold_MoveSpeed.text = MoveSpeed_Cost.ToString();
                if (count > 4)
                    MoveSpeed_LevelUpBtn.SetActive(false);
                else
                    MoveSpeed_LevelUpBtn.SetActive(true);
                break;
            case "AttackSpeed":
                for (int i = 0; i < AttackSpeed.Length; i++)
                {
                    //AttackSpeed[i].GetComponent<Image>().color = Color.white;
                    AttackSpeed[i].transform.GetChild(0).gameObject.SetActive(false);
                }
                for (int i = 0; i < count; i++)
                {
                    //AttackSpeed[i].GetComponent<Image>().color = Color.yellow;
                    AttackSpeed[i].transform.GetChild(0).gameObject.SetActive(true);
                }
                if (count > 0)
                    AttackSpeed_Cost = (count + 1) * 200;
                else
                    AttackSpeed_Cost = 200;
                gold_AttackSpeed.text = AttackSpeed_Cost.ToString();
                if (count > 4)
                    AttackSpeed_LevelUpBtn.SetActive(false);
                else
                    AttackSpeed_LevelUpBtn.SetActive(true);
                break;
            case "Crit":
                for (int i = 0; i < Crit.Length; i++)
                {
                    //Crit[i].GetComponent<Image>().color = Color.white;
                    Crit[i].transform.GetChild(0).gameObject.SetActive(false);
                }
                for (int i = 0; i < count; i++)
                {
                    //Crit[i].GetComponent<Image>().color = Color.yellow;
                    Crit[i].transform.GetChild(0).gameObject.SetActive(true);
                }
                if (count > 0)
                    Crit_Cost = (count + 1) * 200;
                else
                    Crit_Cost = 200;
                gold_Crit.text = Crit_Cost.ToString();
                if (count > 4)
                    Crit_LevelUpBtn.SetActive(false);
                else
                    Crit_LevelUpBtn.SetActive(true);
                break;
            case "CritDamage":
                for (int i = 0; i < CritDamage.Length; i++)
                {
                    //CritDamage[i].GetComponent<Image>().color = Color.white;
                    CritDamage[i].transform.GetChild(0).gameObject.SetActive(false);
                }
                for (int i = 0; i < count; i++)
                {
                    //CritDamage[i].GetComponent<Image>().color = Color.yellow;
                    CritDamage[i].transform.GetChild(0).gameObject.SetActive(true);
                }
                if (count > 0)
                    CritDamage_Cost = (count + 1) * 200;
                else
                    CritDamage_Cost = 200;
                gold_CritDamage.text = CritDamage_Cost.ToString();
                if (count > 4)
                    CritDamage_LevelUpBtn.SetActive(false);
                else
                    CritDamage_LevelUpBtn.SetActive(true);
                break;
            case "PickUpRange":
                for (int i = 0; i < PickUpRange.Length; i++)
                {
                    //PickUpRange[i].GetComponent<Image>().color = Color.white;
                    PickUpRange[i].transform.GetChild(0).gameObject.SetActive(false);
                }
                for (int i = 0; i < count; i++)
                {
                    //PickUpRange[i].GetComponent<Image>().color = Color.yellow;
                    PickUpRange[i].transform.GetChild(0).gameObject.SetActive(true);
                }
                if (count > 0)
                    PickUpRange_Cost = (count + 1) * 200;
                else
                    PickUpRange_Cost = 200;
                gold_PickUpRange.text = PickUpRange_Cost.ToString();
                if (count > 4)
                    PickUpRange_LevelUpBtn.SetActive(false);
                else
                    PickUpRange_LevelUpBtn.SetActive(true);
                break;
            case "ExtraExperience":
                for (int i = 0; i < ExtraExperience.Length; i++)
                {
                    //ExtraExperience[i].GetComponent<Image>().color = Color.white;
                    ExtraExperience[i].transform.GetChild(0).gameObject.SetActive(false);
                }
                for (int i = 0; i < count; i++)
                {
                    //ExtraExperience[i].GetComponent<Image>().color = Color.yellow;
                    ExtraExperience[i].transform.GetChild(0).gameObject.SetActive(true);
                }
                if (count > 0)
                    ExtraExperience_Cost = (count + 1) * 200;
                else
                    ExtraExperience_Cost = 200;
                gold_ExtraExperience.text = ExtraExperience_Cost.ToString();
                if (count > 4)
                    ExtraExperience_LevelUpBtn.SetActive(false);
                else
                    ExtraExperience_LevelUpBtn.SetActive(true);
                break;
            case "ExtraGoldCoin":
                for (int i = 0; i < ExtraGoldCoin.Length; i++)
                {
                    //ExtraGoldCoin[i].GetComponent<Image>().color = Color.white;
                    ExtraGoldCoin[i].transform.GetChild(0).gameObject.SetActive(false);
                }
                for (int i = 0; i < count; i++)
                {
                    //ExtraGoldCoin[i].GetComponent<Image>().color = Color.yellow;
                    ExtraGoldCoin[i].transform.GetChild(0).gameObject.SetActive(true);
                }
                if (count > 0)
                    ExtraGoldCoin_Cost = (count + 1) * 200;
                else
                    ExtraGoldCoin_Cost = 200;
                gold_ExtraGoldCoin.text = ExtraGoldCoin_Cost.ToString();
                if (count > 4)
                    ExtraGoldCoin_LevelUpBtn.SetActive(false);
                else
                    ExtraGoldCoin_LevelUpBtn.SetActive(true);
                break;
            case "ProjectileNumber":
                for (int i = 0; i < ProjectileNumber.Length; i++)
                {
                    //ProjectileNumber[i].GetComponent<Image>().color = Color.white;
                    ProjectileNumber[i].transform.GetChild(0).gameObject.SetActive(false);
                }
                for (int i = 0; i < count; i++)
                {
                    //ProjectileNumber[i].GetComponent<Image>().color = Color.yellow;
                    ProjectileNumber[i].transform.GetChild(0).gameObject.SetActive(true);
                }
                if (count > 0)
                    ProjectileNumber_Cost = (count + 1) * 200;
                else
                    ProjectileNumber_Cost = 200;
                gold_ProjectileNumber.text = ProjectileNumber_Cost.ToString();
                if (count > 4)
                    ProjectileNumber_LevelUpBtn.SetActive(false);
                else
                    ProjectileNumber_LevelUpBtn.SetActive(true);
                break;
        }
    }

    public void PromoteHP()
    {
        AudioManager.Instance.PlayClickBtnAudio();
        int gold = DataManager.Instance.ReadPlayerGoldData();
        if (gold >= HP_Cost)
        {
            gold -= HP_Cost;
            DataManager.Instance.SavePlayerGoldData(gold);
            mainMenu.playerGold.text = DataManager.Instance.ReadPlayerGoldData().ToString();
            int i = promoteCountDic["HP"];
            if (i < HP.Length)
            {
                i++;
                InitAttributeMenu("HP", i);
                promoteCountDic["HP"] = i;
                tipsMenu.SetTips("生命值+"+ HP_promoteValue, 0.5f, Color.green);
                PlayerPrefs.SetInt(mainMenu.currentProfession + "_" + "HP_Promote_Count", i);
                float f = float.Parse(mainMenu.professionData.Max_Hp) + HP_promoteValue;
                mainMenu.professionData.Max_Hp = f.ToString();
                DataManager.Instance.SavePlayerData(mainMenu.currentProfession, mainMenu.professionData);
            }
        }
        else
        {
            tipsMenu.SetTips("金币不足", 0.5f,Color.red);
        }
    }
    public void PromoteHPRecovery()
    {
        AudioManager.Instance.PlayClickBtnAudio();
        int gold = DataManager.Instance.ReadPlayerGoldData();
        if (gold >= HPRecovery_Cost)
        {
            gold -= HPRecovery_Cost;
            DataManager.Instance.SavePlayerGoldData(gold);
            mainMenu.playerGold.text = DataManager.Instance.ReadPlayerGoldData().ToString();
            int i = promoteCountDic["HPRecovery"];
            if (i < HPRecovery.Length)
            {
                i++;
                InitAttributeMenu("HPRecovery", i);
                promoteCountDic["HPRecovery"] = i;
                tipsMenu.SetTips("生命恢复+" + HPRecovery_promoteValue, 0.5f, Color.green);
                PlayerPrefs.SetInt(mainMenu.currentProfession + "_" + "HPRecovery_Promote_Count", i);
                float f = float.Parse(mainMenu.professionData.Re_Hp) + HPRecovery_promoteValue;
                mainMenu.professionData.Re_Hp = f.ToString();
                DataManager.Instance.SavePlayerData(mainMenu.currentProfession, mainMenu.professionData);
            }
        }
        else
        {
            tipsMenu.SetTips("金币不足", 0.5f,Color.red);
        }
    }
    public void PromoteArmor()
    {
        AudioManager.Instance.PlayClickBtnAudio();
        int gold = DataManager.Instance.ReadPlayerGoldData();
        if (gold >= Armor_Cost)
        {
            gold -= Armor_Cost;
            DataManager.Instance.SavePlayerGoldData(gold);
            mainMenu.playerGold.text = DataManager.Instance.ReadPlayerGoldData().ToString();
            int i = promoteCountDic["Armor"];
            if (i < Armor.Length)
            {
                i++;
                InitAttributeMenu("Armor", i);
                promoteCountDic["Armor"] = i;
                tipsMenu.SetTips("护甲值+" + Armor_promoteValue, 0.5f, Color.green);
                PlayerPrefs.SetInt(mainMenu.currentProfession + "_" + "Armor_Promote_Count", i);
                float f = float.Parse(mainMenu.professionData.Armor) + Armor_promoteValue;
                mainMenu.professionData.Armor = f.ToString();
                DataManager.Instance.SavePlayerData(mainMenu.currentProfession, mainMenu.professionData);
            }
        }
        else
        {
            tipsMenu.SetTips("金币不足", 0.5f, Color.red);
        }
    }
    public void PromoteMoveSpeed()
    {
        AudioManager.Instance.PlayClickBtnAudio();
        int gold = DataManager.Instance.ReadPlayerGoldData();
        if (gold >= MoveSpeed_Cost)
        {
            gold -= MoveSpeed_Cost;
            DataManager.Instance.SavePlayerGoldData(gold);
            mainMenu.playerGold.text = DataManager.Instance.ReadPlayerGoldData().ToString();
            int i = promoteCountDic["MoveSpeed"];
            if (i < MoveSpeed.Length)
            {
                i++;
                InitAttributeMenu("MoveSpeed", i);
                promoteCountDic["MoveSpeed"] = i;
                tipsMenu.SetTips("移动速度+" + MoveSpeed_promoteValue, 0.5f, Color.green);
                PlayerPrefs.SetInt(mainMenu.currentProfession + "_" + "MoveSpeed_Promote_Count", i);
                float f = float.Parse(mainMenu.professionData.MoveSpeed) + MoveSpeed_promoteValue;
                mainMenu.professionData.MoveSpeed = f.ToString();
                DataManager.Instance.SavePlayerData(mainMenu.currentProfession, mainMenu.professionData);
            }
        }
        else
        {
            tipsMenu.SetTips("金币不足", 0.5f, Color.red);
        }
    }
    public void PromoteAttackSpeed()
    {
        AudioManager.Instance.PlayClickBtnAudio();
        int gold = DataManager.Instance.ReadPlayerGoldData();
        if (gold >= AttackSpeed_Cost)
        {
            gold -= AttackSpeed_Cost;
            DataManager.Instance.SavePlayerGoldData(gold);
            mainMenu.playerGold.text = DataManager.Instance.ReadPlayerGoldData().ToString();
            int i = promoteCountDic["AttackSpeed"];
            if (i < AttackSpeed.Length)
            {
                i++;
                InitAttributeMenu("AttackSpeed", i);
                promoteCountDic["AttackSpeed"] = i;
                tipsMenu.SetTips("攻击速度+" + AttackSpeed_promoteValue*100+"%", 0.5f, Color.green);
                PlayerPrefs.SetInt(mainMenu.currentProfession + "_" + "AttackSpeed_Promote_Count", i);
                float f = float.Parse(mainMenu.professionData.AttackSpeed) + AttackSpeed_promoteValue;
                mainMenu.professionData.AttackSpeed = f.ToString();
                DataManager.Instance.SavePlayerData(mainMenu.currentProfession, mainMenu.professionData);
            }
        }
        else
        {
            tipsMenu.SetTips("金币不足", 0.5f, Color.red);
        }
    }
    public void PromoteCrit()
    {
        AudioManager.Instance.PlayClickBtnAudio();
        int gold = DataManager.Instance.ReadPlayerGoldData();
        if (gold >= Crit_Cost)
        {
            gold -= Crit_Cost;
            DataManager.Instance.SavePlayerGoldData(gold);
            mainMenu.playerGold.text = DataManager.Instance.ReadPlayerGoldData().ToString();
            int i = promoteCountDic["Crit"];
            if (i < Crit.Length)
            {
                i++;
                InitAttributeMenu("Crit", i);
                promoteCountDic["Crit"] = i;
                tipsMenu.SetTips("暴击率+" + Crit_promoteValue * 100 + "%", 0.5f, Color.green);
                PlayerPrefs.SetInt(mainMenu.currentProfession + "_" + "Crit_Promote_Count", i);
                float f = float.Parse(mainMenu.professionData.CritChance) + Crit_promoteValue;
                mainMenu.professionData.CritChance = f.ToString();
                DataManager.Instance.SavePlayerData(mainMenu.currentProfession, mainMenu.professionData);
            }
        }
        else
        {
            tipsMenu.SetTips("金币不足", 0.5f, Color.red);
        }
    }
    public void PromoteCritDamage()
    {
        AudioManager.Instance.PlayClickBtnAudio();
        int gold = DataManager.Instance.ReadPlayerGoldData();
        if (gold >= CritDamage_Cost)
        {
            gold -= CritDamage_Cost;
            DataManager.Instance.SavePlayerGoldData(gold);
            mainMenu.playerGold.text = DataManager.Instance.ReadPlayerGoldData().ToString();
            int i = promoteCountDic["CritDamage"];
            if (i < CritDamage.Length)
            {
                i++;
                InitAttributeMenu("CritDamage", i);
                promoteCountDic["CritDamage"] = i;
                tipsMenu.SetTips("暴击伤害+" + CritDamage_promoteValue * 100 + "%", 0.5f, Color.green);
                PlayerPrefs.SetInt(mainMenu.currentProfession + "_" + "CritDamage_Promote_Count", i);
                float f = float.Parse(mainMenu.professionData.CritDamage) + CritDamage_promoteValue;
                mainMenu.professionData.CritDamage = f.ToString();
                DataManager.Instance.SavePlayerData(mainMenu.currentProfession, mainMenu.professionData);
            }
        }
        else
        {
            tipsMenu.SetTips("金币不足", 0.5f, Color.red);
        }
    }
    public void PromotePickUpRange()
    {
        AudioManager.Instance.PlayClickBtnAudio();
        int gold = DataManager.Instance.ReadPlayerGoldData();
        if (gold >= PickUpRange_Cost)
        {
            gold -= PickUpRange_Cost;
            DataManager.Instance.SavePlayerGoldData(gold);
            mainMenu.playerGold.text = DataManager.Instance.ReadPlayerGoldData().ToString();
            int i = promoteCountDic["PickUpRange"];
            if (i < PickUpRange.Length)
            {
                i++;
                InitAttributeMenu("PickUpRange", i);
                promoteCountDic["PickUpRange"] = i;
                tipsMenu.SetTips("拾取范围+" + PickUpRange_promoteValue, 0.5f, Color.green);
                PlayerPrefs.SetInt(mainMenu.currentProfession + "_" + "PickUpRange_Promote_Count", i);
                float f = float.Parse(mainMenu.professionData.PickUpRange) + PickUpRange_promoteValue;
                mainMenu.professionData.PickUpRange = f.ToString();
                DataManager.Instance.SavePlayerData(mainMenu.currentProfession, mainMenu.professionData);
            }
        }
        else
        {
            tipsMenu.SetTips("金币不足", 0.5f, Color.red);
        }
    }
    public void PromoteExtraExperience()
    {
        AudioManager.Instance.PlayClickBtnAudio();
        int gold = DataManager.Instance.ReadPlayerGoldData();
        if (gold >= ExtraExperience_Cost)
        {
            gold -= ExtraExperience_Cost;
            DataManager.Instance.SavePlayerGoldData(gold);
            mainMenu.playerGold.text = DataManager.Instance.ReadPlayerGoldData().ToString();
            int i = promoteCountDic["ExtraExperience"];
            if (i < ExtraExperience.Length)
            {
                i++;
                InitAttributeMenu("ExtraExperience", i);
                promoteCountDic["ExtraExperience"] = i;
                tipsMenu.SetTips("额外经验+" + ExtraExperience_promoteValue * 100 + "%", 0.5f, Color.green);
                PlayerPrefs.SetInt(mainMenu.currentProfession + "_" + "ExtraExperience_Promote_Count", i);
                float f = float.Parse(mainMenu.professionData.Exp_GainRate) + ExtraExperience_promoteValue;
                mainMenu.professionData.Exp_GainRate = f.ToString();
                DataManager.Instance.SavePlayerData(mainMenu.currentProfession, mainMenu.professionData);
            }
        }
        else
        {
            tipsMenu.SetTips("金币不足", 0.5f, Color.red);
        }
    }
    public void PromoteExtraGoldCoin()
    {
        AudioManager.Instance.PlayClickBtnAudio();
        int gold = DataManager.Instance.ReadPlayerGoldData();
        if (gold >= ExtraGoldCoin_Cost)
        {
            gold -= ExtraGoldCoin_Cost;
            DataManager.Instance.SavePlayerGoldData(gold);
            mainMenu.playerGold.text = DataManager.Instance.ReadPlayerGoldData().ToString();
            int i = promoteCountDic["ExtraGoldCoin"];
            if (i < ExtraGoldCoin.Length)
            {
                i++;
                InitAttributeMenu("ExtraGoldCoin", i);
                promoteCountDic["ExtraGoldCoin"] = i;
                tipsMenu.SetTips("额外金币+" + ExtraGoldCoin_promoteValue * 100 + "%", 0.5f, Color.green);
                PlayerPrefs.SetInt(mainMenu.currentProfession + "_" + "ExtraGoldCoin_Promote_Count", i);
                float f = float.Parse(mainMenu.professionData.Gold_GainRate) + ExtraGoldCoin_promoteValue;
                mainMenu.professionData.Gold_GainRate = f.ToString();
                DataManager.Instance.SavePlayerData(mainMenu.currentProfession, mainMenu.professionData);
            }
        }
        else
        {
            tipsMenu.SetTips("金币不足", 0.5f, Color.red);
        }
    }
    public void PromoteProjectileNumber()
    {
        AudioManager.Instance.PlayClickBtnAudio();
        int gold = DataManager.Instance.ReadPlayerGoldData();
        if (gold >= ProjectileNumber_Cost)
        {
            gold -= ProjectileNumber_Cost;
            DataManager.Instance.SavePlayerGoldData(gold);
            mainMenu.playerGold.text = DataManager.Instance.ReadPlayerGoldData().ToString();
            int i = promoteCountDic["ProjectileNumber"];
            if (i < ProjectileNumber.Length)
            {
                i++;
                InitAttributeMenu("ProjectileNumber", i);
                promoteCountDic["ProjectileNumber"] = i;
                tipsMenu.SetTips("投射物数量+" + ProjectileNumber_promoteValue, 0.5f, Color.green);
                PlayerPrefs.SetInt(mainMenu.currentProfession + "_" + "ProjectileNumber_Promote_Count", i);
                float f = float.Parse(mainMenu.professionData.ProjectilesNum) + ProjectileNumber_promoteValue;
                mainMenu.professionData.ProjectilesNum = f.ToString();
                DataManager.Instance.SavePlayerData(mainMenu.currentProfession, mainMenu.professionData);
            }
        }
        else
        {
            tipsMenu.SetTips("金币不足", 0.5f, Color.red);
        }
    }

    public void OnClickToClose()
    {
        AudioManager.Instance.PlayCloseUIAudio();
        mainMenu.ReadProfessionData(mainMenu.currentProfession);
        promoteCountDic.Clear();
        StartCoroutine(CloseIconSetting());
    }

    IEnumerator CloseIconSetting()
    {
        gameObject.GetComponent<RectTransform>().DOScaleY(0f, 0.25f).SetEase(Ease.InBack);
        yield return new WaitForSecondsRealtime(0.25f);
        mainMenu.SetProfessionsActive(mainMenu.currentProfession, true);
        base.UIResponse_Close();
    }
}

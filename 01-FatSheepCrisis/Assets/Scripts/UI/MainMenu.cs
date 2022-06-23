using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : UIMenuBase
{
    private Dictionary<string, ProfessionData> Data;
    private PackageItem profession;
    private Dictionary<string, WeaponData> weaponData;
    public ProfessionData professionData;
    public Text Name;
    public Text Max_Hp;
    public Text Re_Hp;
    public Text Armor;
    public Text MoveSpeed;
    public Text AttackSpeed;
    public Text CritChance;
    public Text CritDamage;
    public Text PickUpRange;
    public Text Exp_GainRate;
    public Text Gold_GainRate;
    public Text ProjectilesNum;
    public Text playerGold;
    public GameObject attributeLevelBtn;
    public GameObject startGameBtn;
    public GameObject choiseProfessionBtn;
    public GameObject buyProfessionBtn;
    public Image EquipedWeapon;
    public string currentProfession;
    public string[] playerProfessionsData;
    public string[] playerWeaponsData;
    private List<string> professionID = new List<string>();
    private List<GameObject> professions = new List<GameObject>();
    private TipsMenu tipsMenu;

    public override void Setup()
    {
        base.Setup();
        profession = Resources.Load<PackageItem>("Config");
        Data = profession.GetProfessionData();
        weaponData = profession.GetWeaponsData();
        foreach (var item in Data)
        {
            professionID.Add(item.Key);
        }
        professions = UIManager.Instance.professions;
        tipsMenu = UIManager.Instance.GetMenu(MenuType.TipsMenu) as TipsMenu;
        UIManager.Instance.AddOverlayMenu(tipsMenu);
    }

    public override void Init()
    {
        base.Init();
        currentProfession = PlayerPrefs.GetString("DefaultProfession", "3001");
        StartCoroutine(ShowMainMenu());
        ReadProfessionData(currentProfession);
        if (!PlayerPrefs.HasKey("Professions"))
        {
            choiseProfessionBtn.SetActive(true);
            startGameBtn.SetActive(false);
            attributeLevelBtn.SetActive(false);
            buyProfessionBtn.SetActive(false);
        }
        else
        {
            playerWeaponsData = DataManager.Instance.ReadPlayerWeaponsData();
        }
        playerGold.text = DataManager.Instance.ReadPlayerGoldData().ToString();
    }

    public bool CheckPlayerProfessions()
    {
        playerProfessionsData = DataManager.Instance.ReadPlayerProfessionsData();
        bool haveProfessions = false;
        for (int i = 0; i < playerProfessionsData.Length; i++)
        {
            if (playerProfessionsData[i] == professionData.Name)
            {
                haveProfessions = true;
                break;
            }
        }
        if (haveProfessions)
        {
            EquipedWeapon.sprite = XTool.LoadAssetAtPath<Sprite>("Assets/RawResources/Weapons/", CheckWeaponsIndex(professionData.Weapon) + ".png");
            Player.Instance.SetWeapon(int.Parse(CheckWeaponsIndex(professionData.Weapon)));
        }
        EquipedWeapon.gameObject.SetActive(haveProfessions);
        startGameBtn.SetActive(haveProfessions);
        attributeLevelBtn.SetActive(haveProfessions);
        if (PlayerPrefs.HasKey("Professions"))
            buyProfessionBtn.SetActive(!haveProfessions);
        return haveProfessions;
    }

    public void ReadProfessionData(string key)
    {
        professionData = DataManager.Instance.ReadPlayerData(key);
        Name.text = professionData.Name;
        Max_Hp.text = professionData.Max_Hp;
        Re_Hp.text = professionData.Re_Hp;
        Armor.text = professionData.Armor;
        MoveSpeed.text = professionData.MoveSpeed;
        AttackSpeed.text = (float.Parse(professionData.AttackSpeed) * 100).ToString() + "%";
        CritChance.text = (float.Parse(professionData.CritChance)*100).ToString()+"%";
        CritDamage.text = (float.Parse(professionData.CritDamage) * 100).ToString() + "%";
        PickUpRange.text = professionData.PickUpRange;
        Exp_GainRate.text = (float.Parse(professionData.Exp_GainRate) * 100).ToString() + "%";
        Gold_GainRate.text = (float.Parse(professionData.Gold_GainRate) * 100).ToString() + "%";
        ProjectilesNum.text = professionData.ProjectilesNum;
    }

    public void SetProfessionsActive(string key,bool active)
    {
        int index = int.Parse(key.Substring(3, 1))-1;
        if((index+1)> professions.Count)
        {
            return;
        }
        if (active)
        {
            for (int i = 0; i < professions.Count; i++)
            {
                if (professions[i] == professions[index])
                {
                    professions[i].SetActive(active);
                }
                else
                {
                    professions[i].SetActive(!active);
                }
            }
        }
        else
        {
            for (int i = 0; i < professions.Count; i++)
            {
                professions[i].SetActive(active);
            }
        }
    }

    public void OnClickToBuyProfession()
    {
        AudioManager.Instance.PlayClickBtnAudio();
        int gold = int.Parse(playerGold.text);
        if (gold >= 5000)
        {
            DataManager.Instance.SavePlayerProfessionsData(professionData.Name);
            DataManager.Instance.SavePlayerWeaponsData(professionData.Weapon);
            gold -= 5000;
            DataManager.Instance.SavePlayerGoldData(gold);
            playerGold.text = DataManager.Instance.ReadPlayerGoldData().ToString();
        }
        else
        {
            tipsMenu.SetTips("½ð±Ò²»×ã",0.5f);
        }
        CheckPlayerProfessions();
    }

    public void OnClickToChioseProfession()
    {
        AudioManager.Instance.PlayClickBtnAudio();
        DataManager.Instance.SavePlayerProfessionsData(professionData.Name);
        DataManager.Instance.SavePlayerWeaponsData(professionData.Weapon);
        PlayerPrefs.SetString("DefaultProfession", professionData.Id);
        choiseProfessionBtn.SetActive(false);
        CheckPlayerProfessions();
    }

    public void OnClickToNextProfession()
    {
        AudioManager.Instance.PlayClickBtnAudio();
        int i = int.Parse(currentProfession);
        if (i < int.Parse(professionID[professionID.Count - 1]))
        {
            i++;
            currentProfession = i.ToString();
            ReadProfessionData(Data[currentProfession].Id);
        }
        else
        {
            currentProfession = professionID[0];
            ReadProfessionData(Data[currentProfession].Id);
        }
        SetProfessionsActive(currentProfession, true);
        CheckPlayerProfessions();
    }

    public void OnClickToLastProfession()
    {
        AudioManager.Instance.PlayClickBtnAudio();
        int i = int.Parse(currentProfession);
        if (i > int.Parse(professionID[0]))
        {
            i--;
            currentProfession = i.ToString();
            ReadProfessionData(Data[currentProfession].Id);
        }
        else
        {
            currentProfession = professionID[professionID.Count - 1];
            ReadProfessionData(Data[currentProfession].Id);
        }
        SetProfessionsActive(currentProfession, true);
        CheckPlayerProfessions();
    }

    IEnumerator ShowMainMenu()
    {
        int i = Random.Range(0, 2);
        float waitTime = 0.25f;
        if (i == 0)
        {
            gameObject.GetComponent<RectTransform>().localScale = new Vector3(0, 1, 1);
            //yield return new WaitForSecondsRealtime(waitTime);
            gameObject.GetComponent<RectTransform>().DOScaleX(1.0f, 0.5f).SetEase(Ease.OutSine);
            yield return new WaitForSecondsRealtime(waitTime);
        }
        else
        {
            gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 0, 1);
            //yield return new WaitForSecondsRealtime(waitTime);
            gameObject.GetComponent<RectTransform>().DOScaleY(1.0f, 0.5f).SetEase(Ease.OutSine);
            yield return new WaitForSecondsRealtime(waitTime);
        }
        SetProfessionsActive(currentProfession, true);
        CheckPlayerProfessions();
    }
    public void OnClickToClose()
    {
        AudioManager.Instance.PlayClickBtnAudio();
        StartCoroutine(CloseIconSetting());
        SetProfessionsActive(currentProfession, false);
    }

    public string CheckWeaponsIndex(string weapon)
    {
        string weaponIndex = null;
        foreach (var item in weaponData)
        {
            if (weapon == item.Value.Name)
            {
                weaponIndex = item.Key;
            }
        }
        return weaponIndex;
    }

    IEnumerator CloseIconSetting()
    {
        float waitTime = 0.25f;
        int j = Random.Range(0, 2);
        if (j == 0)
        {
            gameObject.GetComponent<RectTransform>().DOScaleX(0f, waitTime).SetEase(Ease.InSine);
        }
        else
        {
            gameObject.GetComponent<RectTransform>().DOScaleY(0f, waitTime).SetEase(Ease.InSine);
        }
        yield return new WaitForSecondsRealtime(waitTime);
        StartMenu startMenu = UIManager.Instance.GetMenu(MenuType.StartMenu) as StartMenu;
        int i = Random.Range(0, 2);
        if (i == 0)
        {
            startMenu.gameObject.GetComponent<RectTransform>().localScale = new Vector3(0, 1, 1);
            startMenu.gameObject.GetComponent<RectTransform>().DOScaleX(1f, waitTime).SetEase(Ease.OutSine);
        }
        else
        {
            startMenu.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 0, 1);
            startMenu.gameObject.GetComponent<RectTransform>().DOScaleY(1f, waitTime).SetEase(Ease.OutSine);
        }
        base.UIResponse_Close();
    }
    public void ShowWeaponMenu()
    {
        AudioManager.Instance.PlayClickBtnAudio();
        WeaponMenu weaponMenu = UIManager.Instance.GetMenu(MenuType.WeaponMenu) as WeaponMenu;
        weaponMenu.weaponMenuType = WeaponMenu.WeaponMenuType.AllWeapon;
        UIManager.Instance.AddOverlayMenu(weaponMenu);
        SetProfessionsActive(currentProfession, false);
    }
    public void ShowProfessionWeaponMenu()
    {
        AudioManager.Instance.PlayClickBtnAudio();
        WeaponMenu weaponMenu = UIManager.Instance.GetMenu(MenuType.WeaponMenu) as WeaponMenu;
        weaponMenu.weaponMenuType = WeaponMenu.WeaponMenuType.ProfessionWeapon;
        UIManager.Instance.AddOverlayMenu(weaponMenu);
        SetProfessionsActive(currentProfession, false);
    }
    public void ShowAttributeMenu()
    {
        AudioManager.Instance.PlayClickBtnAudio();
        AttributeMenu attributeMenu = UIManager.Instance.GetMenu(MenuType.AttributeMenu) as AttributeMenu;
        UIManager.Instance.AddOverlayMenu(attributeMenu);
        SetProfessionsActive(currentProfession, false);
    }
    public void ShowTicketMenu()
    {
        AudioManager.Instance.PlayClickBtnAudio();
        TicketMenu ticketMenu = UIManager.Instance.GetMenu(MenuType.TicketMenu) as TicketMenu;
        UIManager.Instance.AddOverlayMenu(ticketMenu);
        SetProfessionsActive(currentProfession, false);
    }

    public void LoadNextScene(string scene)
    {
        AudioManager.Instance.PlayClickBtnAudio();
        UIResponse_Close();
        PlayerPrefs.SetString("DefaultProfession", professionData.Id);
        if (scene.Length > 0)
        {
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
        }
        foreach (var item in professions)
        {
            if (item.activeInHierarchy)
            {
                Instantiate(item).GetComponent<Player>().Init(int.Parse(CheckWeaponsIndex(professionData.Weapon)));
            }
        }
        SetProfessionsActive(currentProfession, false);
        AudioManager.Instance.PlayFightAudio();
    }
}

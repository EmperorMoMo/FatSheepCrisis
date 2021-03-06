using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class WeaponMenu : UIMenuBase
{
    public enum WeaponMenuType
    {
        ProfessionWeapon,
        AllWeapon
    }

    private Dictionary<string, WeaponData> Data;
    private PackageItem Weapon;
    public GameObject weaponGrid;
    public RectTransform content;
    public Text weaponsName;
    public Text aggressivity;
    public Text attackInterval;
    public Text critChance;
    public Text critDamage;
    public Text repelNum;
    public Text projectilesNum;
    public Text SkillDescription;
    public Text Title;
    private MainMenu mainMenu;
    public WeaponMenuType weaponMenuType;
    public GameObject OKBtn;
    public string currentWeapon;
    private bool isProfession;
    private TipsMenu tipsMenu;
    public Sprite[] sprites;
    public Color[] weaponColors;
    public Color[] nameColors;

    public override void Setup()
    {
        base.Setup();
        Weapon = Resources.Load<PackageItem>("Config");
        Data = Weapon.GetWeaponsData();
    }
    public override void Init()
    {
        base.Init();
        gameObject.GetComponent<RectTransform>().localScale = Vector3.zero;
        gameObject.GetComponent<RectTransform>().DOScale(1.0f, 0.25f).SetEase(Ease.OutBack);
        mainMenu = UIManager.Instance.GetMenu(MenuType.MainMenu) as MainMenu;
        tipsMenu= UIManager.Instance.GetMenu(MenuType.TipsMenu) as TipsMenu;
        isProfession = mainMenu.CheckPlayerProfessions();
        ShowWeapons(weaponMenuType);
        currentWeapon = null;
        if (CheckPlayerWeapons(mainMenu.professionData.Weapon)&& isProfession)
        {
            OnClickToSelect(mainMenu.professionData.Weapon);
        }
        else
        {
            InitWeaponsText();
            OKBtn.SetActive(false);
        }
    }

    public void ShowWeapons(WeaponMenuType type)
    {
        for (int i = 0; i < content.childCount; i++)
        {
            GameObject.Destroy(content.GetChild(i).gameObject);
        }
        GameObject obj;
        List<GameObject> gameObjects = new List<GameObject>();
        if (type == WeaponMenuType.AllWeapon)
        {
            Title.text = "????????";
            foreach (var item in Data)
            {
                obj = Instantiate(weaponGrid.gameObject);
                obj.name = item.Value.Id + "_" + item.Value.Quality;
                RectTransform rt = obj.GetComponent<RectTransform>();
                rt.SetParent(content);
                rt.localScale = Vector3.one;
                rt.localPosition = Vector3.zero;
                obj.GetComponent<Image>().color = weaponColors[int.Parse(item.Value.Quality)];
                Image image1 = obj.transform.GetChild(0).GetComponent<Image>();
                image1.sprite = sprites[int.Parse(item.Value.Quality)];
                Image image = obj.transform.GetChild(1).GetComponent<Image>();
#if UNITY_EDITOR
                image.sprite = XTool.LoadAssetAtPath<Sprite>("Assets/RawResources/Weapons/", item.Value.Id + ".png");
#else
                image.sprite = XTool.LoadTextureAssestBundle(item.Value.Id + ".png");
#endif
                obj.SetActive(true);
                obj.transform.GetChild(2).gameObject.SetActive(!CheckPlayerWeapons(item.Value.Name));
                gameObjects.Add(obj);
            }
        }
        else
        {
            Title.text = "????????";
            foreach (var item in Data)
            {
                if (mainMenu.professionData.WeaponType == item.Value.Type&&CheckPlayerWeapons(item.Value.Name)&& isProfession)
                {
                    obj = Instantiate(weaponGrid.gameObject);
                    obj.name = item.Value.Id + "_" + item.Value.Quality;
                    RectTransform rt = obj.GetComponent<RectTransform>();
                    rt.SetParent(content);
                    rt.localScale = Vector3.one;
                    rt.localPosition = Vector3.zero;
                    obj.GetComponent<Image>().color = weaponColors[int.Parse(item.Value.Quality)];
                    Image image1 = obj.transform.GetChild(0).GetComponent<Image>();
                    image1.sprite = sprites[int.Parse(item.Value.Quality)];
                    Image image = obj.transform.GetChild(1).GetComponent<Image>();
#if UNITY_EDITOR
                    image.sprite = XTool.LoadAssetAtPath<Sprite>("Assets/RawResources/Weapons/", item.Value.Id + ".png");
#else
                    image.sprite = XTool.LoadTextureAssestBundle(item.Value.Id + ".png");
#endif
                    obj.SetActive(true);
                    gameObjects.Add(obj);
                }
            }
        }
        for (int i = 0; i < gameObjects.Count; i++)
        {
            for (int j = 0; j < gameObjects.Count - i - 1; j++)
            {
                if (int.Parse(gameObjects[j].name.Substring(5, 1)) > int.Parse(gameObjects[j + 1].name.Substring(5, 1)))
                {
                    GameObject go = gameObjects[j];
                    gameObjects[j] = gameObjects[j + 1];
                    gameObjects[j + 1] = go;
                }
            }
        }
        for (int i = 0; i < gameObjects.Count; i++)
        {
            gameObjects[i].transform.SetSiblingIndex(i);
        }
    }

    public bool CheckPlayerWeapons(string weapons)
    {
        string[] str = DataManager.Instance.ReadPlayerWeaponsData();
        bool haveWeapons = false;
        for (int i = 0; i < str.Length; i++)
        {
            if(str[i]==weapons)
            {
                haveWeapons = true;
            }
        }
        return haveWeapons;
    }

    public void InitWeaponsText()
    {
        weaponsName.text = "";
        aggressivity.text = "";
        attackInterval.text = "";
        critChance.text = "";
        critDamage.text = "";
        repelNum.text = "";
        projectilesNum.text = "";
        SkillDescription.text = "";
    }

    public void EquipWeapons()
    {
        AudioManager.Instance.PlayEquipWeaponAudio();
        mainMenu.professionData.Weapon = Data[currentWeapon].Name;
        DataManager.Instance.SavePlayerData(mainMenu.currentProfession, mainMenu.professionData);
#if UNITY_EDITOR
        mainMenu.EquipedWeapon.sprite = XTool.LoadAssetAtPath<Sprite>("Assets/RawResources/Weapons/", currentWeapon + ".png");
#else
        mainMenu.EquipedWeapon.sprite = XTool.LoadTextureAssestBundle(currentWeapon + ".png");
#endif
        Player.Instance.SetWeapon(int.Parse(currentWeapon));
        tipsMenu.SetTips("????????", 0.5f, Color.green);
    }

    public void OnClickToSelect(string weapon)
    {
        AudioManager.Instance.PlayClickBtnAudio();
        string str = "";
        if (weapon == "none")
        {
            var button = EventSystem.current.currentSelectedGameObject;
            str = button.name.Substring(0, 4);
        }
        else
        {
            foreach (var item in Data)
            {
                if (weapon == item.Value.Name)
                {
                    str = item.Key;
                }
            }
        }
        weaponsName.text = Data[str].Name;
        weaponsName.color = nameColors[int.Parse(Data[str].Quality)];
        aggressivity.text = Data[str].Aggressivity;
        attackInterval.text = Data[str].AttackInterval;
        critChance.text = Data[str].CritChance;
        critDamage.text = Data[str].CritDamage;
        repelNum.text = Data[str].RepelNum;
        projectilesNum.text = Data[str].ProjectilesNum;
        SkillDescription.text = Data[str].SkillDescription;
        if (mainMenu.professionData.WeaponType != Data[str].Type||!CheckPlayerWeapons(Data[str].Name)||!isProfession)
        {
            OKBtn.SetActive(false);
        }
        else
        {
            OKBtn.SetActive(true);
        }
        for (int i = 0; i < content.childCount; i++)
        {
            if(content.GetChild(i).name.Substring(0, 4) == str)
            {
                content.GetChild(i).GetChild(3).gameObject.SetActive(true);
                content.GetChild(i).transform.DOScale(1.2f, 0.25f).SetEase(Ease.OutBack);
                //content.GetChild(i).transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            }
            else
            {
                content.GetChild(i).GetChild(3).gameObject.SetActive(false);
                content.GetChild(i).transform.localScale = Vector3.one;
            }
        }
        currentWeapon = str;
    }
    public void OnClickToClose()
    {
        AudioManager.Instance.PlayCloseUIAudio();
        StartCoroutine(CloseIconSetting());
    }
    IEnumerator CloseIconSetting()
    {
        gameObject.GetComponent<RectTransform>().DOScale(0f, 0.25f).SetEase(Ease.InBack);
        yield return new WaitForSecondsRealtime(0.25f);
        mainMenu.SetProfessionsActive(mainMenu.currentProfession, true);
        base.UIResponse_Close();
    }

}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : UIMenuBase
{
    private Dictionary<string, ProfessionData> Data;
    private PackageItem profession;
    private ProfessionData professionData;
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
    private string currentProfession;
    private List<string> professionID = new List<string>();

    public override void Setup()
    {
        base.Setup();
        profession = Resources.Load<PackageItem>("Config");
        Data = profession.GetProfessionData();
        foreach (var item in Data)
        {
            professionID.Add(item.Key);
        }
    }

    public override void Init()
    {
        base.Init();
        StartCoroutine(ShowMainMenu());
        currentProfession = PlayerPrefs.GetString("DefaultProfession", "3001");
        ReadProfessionData(currentProfession);
    }

    public void ReadProfessionData(string key)
    {
        professionData = DataManager.Instance.ReadPlayerData(key);
        Name.text = professionData.Name;
        Max_Hp.text = professionData.Max_Hp;
        Re_Hp.text = professionData.Re_Hp;
        Armor.text = professionData.Armor;
        MoveSpeed.text = professionData.MoveSpeed;
        AttackSpeed.text = professionData.AttackSpeed;
        CritChance.text = professionData.CritChance;
        CritDamage.text = professionData.CritDamage;
        PickUpRange.text = professionData.PickUpRange;
        Exp_GainRate.text = professionData.Exp_GainRate;
        Gold_GainRate.text = professionData.Gold_GainRate;
        ProjectilesNum.text = professionData.ProjectilesNum;
    }

    public void OnClickToNextProfession()
    {
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
    }

    public void OnClickToLastProfession()
    {
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
    }
    public void OnClickToClose()
    {
        StartCoroutine(CloseIconSetting());
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
        WeaponMenu weaponMenu = UIManager.Instance.GetMenu(MenuType.WeaponMenu) as WeaponMenu;
        UIManager.Instance.AddOverlayMenu(weaponMenu);
    }
    public void ShowAttributeMenu()
    {
        AttributeMenu attributeMenu = UIManager.Instance.GetMenu(MenuType.AttributeMenu) as AttributeMenu;
        UIManager.Instance.AddOverlayMenu(attributeMenu);
    }
    public void ShowTicketMenu()
    {
        TicketMenu ticketMenu = UIManager.Instance.GetMenu(MenuType.TicketMenu) as TicketMenu;
        UIManager.Instance.AddOverlayMenu(ticketMenu);
    }
}

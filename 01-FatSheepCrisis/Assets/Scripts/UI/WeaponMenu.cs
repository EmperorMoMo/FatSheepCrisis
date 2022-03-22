using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class WeaponMenu : UIMenuBase
{
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

    public override void Setup()
    {
        base.Setup();
        Weapon = Resources.Load<PackageItem>("Config");
        Data = Weapon.GetWeaponsData();
        GameObject obj;
        foreach (var item in Data)
        {
            obj = Instantiate(weaponGrid.gameObject);
            obj.name = item.Value.Id;
            RectTransform rt = obj.GetComponent<RectTransform>();
            rt.SetParent(content);
            rt.localScale = Vector3.one;
            rt.localPosition = Vector3.zero;
            Image image = obj.transform.GetChild(0).GetComponent<Image>();
            image.sprite = UIManager.Instance.LoadAssetAtPath<Sprite>("Assets/RawResources/Weapons/", item.Value.Id + ".png");
            obj.SetActive(true);
        }
    }
    public override void Init()
    {
        base.Init();
        gameObject.GetComponent<RectTransform>().localScale = Vector3.zero;
        gameObject.GetComponent<RectTransform>().DOScale(1.0f, 0.25f).SetEase(Ease.OutBack);
    }
    public void OnClickToSelect()
    {
        var button = EventSystem.current.currentSelectedGameObject;
        string str = button.name;
        weaponsName.text = Data[str].Name;
        aggressivity.text = Data[str].Aggressivity;
        attackInterval.text = Data[str].AttackInterval;
        critChance.text = Data[str].CritChance;
        critDamage.text = Data[str].CritDamage;
        repelNum.text = Data[str].RepelNum;
        projectilesNum.text = Data[str].ProjectilesNum;
        SkillDescription.text = Data[str].SkillDescription;
    }
    public void OnClickToClose()
    {
        StartCoroutine(CloseIconSetting());
    }
    IEnumerator CloseIconSetting()
    {
        gameObject.GetComponent<RectTransform>().DOScale(0f, 0.25f).SetEase(Ease.InBack);
        yield return new WaitForSecondsRealtime(0.25f);
        base.UIResponse_Close();
    }

}

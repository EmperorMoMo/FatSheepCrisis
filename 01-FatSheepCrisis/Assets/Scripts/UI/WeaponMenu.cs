using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponMenu : UIMenuBase
{
    private Dictionary<string, WeaponData> Data;
    private PackageItem Weapon;
    public GameObject weaponGrid;
    public RectTransform content;
    public Text weaponsName;
    public Text aggressivity;
    public Text attackSpeed;
    public Text critChance;
    public Text critDamage;
    public Text repelNum;
    public Text projectilesNum;
    public Text SkillDescription;
    public override void Init()
    {
        base.Init();
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
    public void OnClickToSelect()
    {
        var button = EventSystem.current.currentSelectedGameObject;
        string str = button.name;
        weaponsName.text = Data[str].Name;
        aggressivity.text = Data[str].Aggressivity;
        attackSpeed.text = Data[str].AttackSpeed;
        critChance.text = Data[str].CritChance;
        critDamage.text = Data[str].CritDamage;
        repelNum.text = Data[str].RepelNum;
        projectilesNum.text = Data[str].ProjectilesNum;
        SkillDescription.text = Data[str].SkillDescription;
    }
}

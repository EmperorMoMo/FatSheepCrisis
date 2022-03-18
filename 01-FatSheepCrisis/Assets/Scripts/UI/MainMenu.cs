using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : UIMenuBase
{
    public override void Init()
    {
        base.Init();
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

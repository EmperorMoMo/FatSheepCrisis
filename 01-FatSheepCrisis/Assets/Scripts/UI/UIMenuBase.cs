using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuBase : MonoBehaviour
{
    public enum MenuType
    {
        None=0,
        StartMenu=1,
        MainMenu=2,
        WeaponMenu=3,
        AttributeMenu=4,
        TicketMenu=5,
    }

    public MenuType MenuID;

    public float startPosZ;

    public virtual void Setup()
    {

    }

    public virtual void Init()
    {

    }

    public virtual void UIResponse_Close()
    {
        if (Singleton<UIManager>.Instance.IsOverlayMenu(this))
        {
            Singleton<UIManager>.Instance.RemoveOverlayMenu(this);
        }
        else if (Singleton<UIManager>.Instance.IsMenu(this))
        {
            Singleton<UIManager>.Instance.PopMenu();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : UIMenuBase
{
    public Button StartBtn;

    private void Start()
    {

    }

    public void EnterGame()
    {
        MainMenu mainMenu = UIManager.Instance.GetMenu(MenuType.MainMenu) as MainMenu;
        UIManager.Instance.PushMenu(mainMenu);
    }

}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : UIMenuBase
{
    public override void Init()
    {
        base.Init();
        StartCoroutine(ShowMainMenu());
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

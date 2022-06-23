using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : UIMenuBase
{
    public Button StartBtn;
    private bool firstClick=true;

    private void Awake()
    {
        AudioManager.Instance.PlayBackGroundAudio();
    }
    public void EnterGame()
    {
        AudioManager.Instance.PlayClickBtnAudio();
        if (firstClick)
        {
            firstClick = false;
            MainMenu mainMenu = UIManager.Instance.GetMenu(MenuType.MainMenu) as MainMenu;
            int i = Random.Range(0, 2);
            float waitTime = 0.25f;
            if (i == 0)
            {
                gameObject.GetComponent<RectTransform>().DOScaleX(0f, waitTime).SetEase(Ease.InSine).OnComplete(() => { UIManager.Instance.PushMenu(mainMenu); firstClick = true; });
            }
            else
            {
                gameObject.GetComponent<RectTransform>().DOScaleY(0f, waitTime).SetEase(Ease.InSine).OnComplete(() => { UIManager.Instance.PushMenu(mainMenu); firstClick = true; });
            }
        }
    }

}

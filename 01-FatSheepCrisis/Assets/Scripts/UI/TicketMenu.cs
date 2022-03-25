using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketMenu : UIMenuBase
{
    private Dictionary<string, TicketData> Data;
    private PackageItem Ticket;
    private MainMenu mainMenu;
    public override void Setup()
    {
        base.Setup();
        Ticket = Resources.Load<PackageItem>("Config");
        Data = Ticket.GetTicketData();
    }
    public override void Init()
    {
        base.Init();
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(0, 1, 1);
        gameObject.GetComponent<RectTransform>().DOScaleX(1.0f, 0.25f).SetEase(Ease.OutBack);
        mainMenu = UIManager.Instance.GetMenu(MenuType.MainMenu) as MainMenu;
    }
    public void OnClickToClose()
    {
        StartCoroutine(CloseIconSetting());
    }
    IEnumerator CloseIconSetting()
    {
        gameObject.GetComponent<RectTransform>().DOScaleX(0f, 0.25f).SetEase(Ease.InBack);
        yield return new WaitForSecondsRealtime(0.25f);
        mainMenu.SetProfessionsActive(mainMenu.currentProfession, true);
        base.UIResponse_Close();
    }
    /// <summary>
    /// 得到总权重
    /// </summary>
    private int GetTotalWeight()
    {
        int totalWeight = 0;
        foreach (var temp in Data)
        {
            totalWeight += int.Parse(temp.Value.Probability);
        }
        return totalWeight;
    }

    /// <summary>
    /// 得到随机道具
    /// </summary>
    private TicketData GetPowerUp()
    {
        int ranNum = Random.Range(0, GetTotalWeight() + 1);
        float counter = 0;
        foreach (var temp in Data)
        {
            counter += int.Parse(temp.Value.Probability);
            if (ranNum <= counter)
            {
                return (temp.Value);
            }
        }
        return null;
    }

    public int RandomCoin()
    {
        int Coin = Random.Range(0, 101);
        return Coin;
    }

    public void OneTicket()
    {
        string name = GetPowerUp().Name;
        if (name == "随机金币")
        {
            Debug.LogError("获得" + RandomCoin() + "金币");
        }
        else
        {
            Debug.LogError(name);
        }
    }
    public void TenTicket()
    {
        int ssr = 0;
        int sr = 0;
        int r = 0;
        for (int i = 0; i < 10; i++)
        {
            string s = GetPowerUp().Quality;
            switch (s)
            {
                case "SSR":
                    ssr++;
                    break;
                case "SR":
                    sr++;
                    break;
                case "R":
                    r++;
                    break;
                default:
                    break;
            }
        }
        Debug.LogError("10次抽奖中获得SSR、SR、R的次数分别为：" + ssr+ "-----"+sr+"-----" +r);
    }
}

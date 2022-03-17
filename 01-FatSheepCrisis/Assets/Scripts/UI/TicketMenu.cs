using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketMenu : UIMenuBase
{
    private Dictionary<string, TicketData> Data;
    private PackageItem Ticket;
    public override void Setup()
    {
        base.Setup();
        Ticket = Resources.Load<PackageItem>("Config");
        Data = Ticket.GetTicketData();
    }
    /// <summary>
    /// �õ���Ȩ��
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
    /// �õ��������
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
        if (name == "������")
        {
            Debug.LogError("���" + RandomCoin() + "���");
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
        Debug.LogError("10�γ齱�л��SSR��SR��R�Ĵ����ֱ�Ϊ��" + ssr+ "-----"+sr+"-----" +r);
    }
}

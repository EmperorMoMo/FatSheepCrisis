using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBar : MonoBehaviour
{
    // Start is called before the first frame update

    private int Id;
    public GameObject[] buff;

    void Start()
    {
        Id = GetComponentInParent<Enemy>().gameObject.GetInstanceID();
        EventCenter.AddListener<int,BuffType,bool>(EventType.StatusBarHideOrShow, StatusManager);
    }

    void StatusManager(int _id,BuffType buffType,bool isShow)
    {
        if (_id != Id) return;
        switch (buffType)
        {
            case BuffType.¼õËÙ:
                buff[0].SetActive(isShow);
                break;
            case BuffType.×ÆÉÕ:
                buff[1].SetActive(isShow);
                break;
            case BuffType.¸Ðµç:
                buff[2].SetActive(isShow);
                break;
        }
    }
}

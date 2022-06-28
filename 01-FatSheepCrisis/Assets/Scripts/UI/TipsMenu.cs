using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TipsMenu : UIMenuBase
{
    public GameObject tips;
    public Text tipText;
    public Image image;

    public void SetTips(string tip,float time,Color color)
    {
        GameObject obj;
        //Text tipText = obj.GetComponent<Text>();
        tipText.text = tip;
        tipText.color = color;
        image.color = color;
        obj = Instantiate(tips);
        obj.SetActive(true);
        RectTransform rt = obj.GetComponent<RectTransform>();
        rt.SetParent(transform);
        rt.position = tips.GetComponent<RectTransform>().position;
        //rt.localScale = Vector3.zero;
        rt.localScale = new Vector3(1, 0, 1);
        rt.DOScaleY(1.0f, 0.25f).SetEase(Ease.OutBack);
        StartCoroutine(DestroyTips(obj, time));
    }

    IEnumerator DestroyTips(GameObject obj,float time)
    {
        yield return new WaitForSecondsRealtime(time);
        RectTransform rt = obj.GetComponent<RectTransform>();
        rt.DOScaleY(0f, 0.25f).SetEase(Ease.InBack).OnComplete(()=>{ Destroy(obj); });
    }
}

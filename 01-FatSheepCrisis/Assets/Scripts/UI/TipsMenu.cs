using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TipsMenu : UIMenuBase
{
    public GameObject tips;

    public void SetTips(string tip,float time)
    {
        GameObject obj;
        obj = Instantiate(tips);
        obj.SetActive(true);
        Text tipText = obj.GetComponent<Text>();
        tipText.text = tip;
        RectTransform rt = obj.GetComponent<RectTransform>();
        rt.SetParent(transform);
        rt.localScale = Vector3.zero;
        rt.DOScale(1.0f, 0.25f).SetEase(Ease.OutBack);
        StartCoroutine(DestroyTips(obj, time));
    }

    IEnumerator DestroyTips(GameObject obj,float time)
    {
        yield return new WaitForSecondsRealtime(time);
        RectTransform rt = obj.GetComponent<RectTransform>();
        rt.DOScale(0f, 0.25f).SetEase(Ease.InBack).OnComplete(()=>{ Destroy(obj); });
    }
}

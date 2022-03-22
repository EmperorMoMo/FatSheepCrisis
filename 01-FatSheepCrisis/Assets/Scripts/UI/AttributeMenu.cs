using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributeMenu : UIMenuBase
{
    public override void Init()
    {
        base.Init();
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 0, 1);
        gameObject.GetComponent<RectTransform>().DOScaleY(1.0f, 0.25f).SetEase(Ease.OutBack);
    }
    public void OnClickToClose()
    {
        StartCoroutine(CloseIconSetting());
    }

    IEnumerator CloseIconSetting()
    {
        gameObject.GetComponent<RectTransform>().DOScaleY(0f, 0.25f).SetEase(Ease.InBack);
        yield return new WaitForSecondsRealtime(0.25f);
        base.UIResponse_Close();
    }
}

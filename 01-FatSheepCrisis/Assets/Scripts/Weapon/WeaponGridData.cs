using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponGridData : UIMenuBase
{
    public GameObject weaponImage;
    private void OnEnable()
    {
        gameObject.GetComponent<RectTransform>().localScale = Vector3.zero;
        gameObject.GetComponent<RectTransform>().DOScale(1.0f, 0.25f).SetEase(Ease.OutBack);
    }
}

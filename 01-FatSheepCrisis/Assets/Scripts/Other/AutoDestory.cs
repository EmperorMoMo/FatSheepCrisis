using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestory : MonoBehaviour
{
    public float delayTime = 1f;
    private float timer;

    public PrefabType type;

    private void OnEnable()
    {
        timer = 0;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > delayTime)
        {
            timer = 0;
            ObjectPool.Instance.ReturnCacheGameObject(type, gameObject);
        }
    }
}

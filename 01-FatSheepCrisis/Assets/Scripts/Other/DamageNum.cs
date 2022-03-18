using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNum : MonoBehaviour
{
    private float time = 0.6f;
    private float timer;
    private int dir;
    private float num;

    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void OnEnable()
    {
        dir = 0;
        text.fontSize = 15;
        while (dir == 0)
        {
            dir = Random.Range(-1, 2);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > time)
        {
            timer = 0;
            ObjectPool.Instance.ReturnCacheGameObject(PrefabType.DamageText, this.gameObject);
        }

        Move();
    }

    public void SetText(float _f)
    {
        text.text = _f.ToString();
    }

    private void Move()
    {
        num += 0.1f;
        if (timer <= 0.2f)
        {
            transform.position += Vector3.up * Time.deltaTime * 6f;
            transform.position += Vector3.left * Time.deltaTime * 3f * dir;
            if (num >= 0.25f)
            {
                text.fontSize += 1;
                num = 0;
            }
        }
        else
        {
            transform.position += Vector3.down * Time.deltaTime * 6f;
            text.fontSize -= (int)Time.deltaTime * 10;
            if (num >= 0.6f)
            {
                text.fontSize -= 1;
                num = 0;
            }
        }
    }
}
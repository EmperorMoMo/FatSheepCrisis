using UnityEngine;
using UnityEngine.UI;

public class DamageNum : MonoBehaviour
{
    private float time = 0.6f;
    private float timer;
    private int dir;
    private float num;

    private Text text;
    private Animator anim;

    private void Awake()
    {
        text = GetComponent<Text>();
        anim = GetComponent<Animator>();
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
            ObjectPool.Instance.ReturnCacheGameObject(PrefabType.NumberText, this.gameObject);
        }

        Move();
    }

    public void SetTextAndAnimator(float _f,int numType)
    {
        Player.Instance.isCrit = false;
        if (_f == 0) return;
        anim.SetBool("crit", numType == 1 ? true : false);
        anim.SetBool("rehp", numType == 2 ? true : false);
        text.text = _f.ToString();
    }

    private void Move()
    {
        num += 0.1f;
        if (timer <= 0.2f)
        {
            transform.position += Vector3.up * Time.deltaTime * 6f;
            transform.position += Vector3.left * Time.deltaTime * 3.5f * dir;
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

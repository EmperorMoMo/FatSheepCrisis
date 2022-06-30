using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeBomb : MonoBehaviour
{
    private List<GameObject> attackList = new List<GameObject>();

    private float attackRatio, repelNum;

    private Animator anim;
    private Animator Anim
    {
        get
        {
            if (anim == null)
            {
                anim = GetComponent<Animator>();
            }
            return anim;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Damageable>(out Damageable damageable))
        {
            if (collision.CompareTag("Player")) return;
            if (attackList.Contains(collision.gameObject)) return;
            DamageMessage data = new DamageMessage()
            {
                damage = XTool.CalculateDamage(TotalAttribute.Aggressivity * attackRatio),
                direction = (collision.transform.position - transform.position).normalized * repelNum
            }; 
            damageable.OnDamage(data);
            attackList.Add(collision.gameObject);
        }
    }

    public void SetParameter(Vector2 _target, float _attackRatio, float _repelNum)
    {
        attackRatio = _attackRatio;
        repelNum = _repelNum;
        Bomb(_target);
    }

    private void Bomb(Vector2 _target)
    {
        if (_target.Equals(Vector2.one * 1000f))
        {
            transform.position = new Vector3(Player.Instance.transform.position.x + Random.Range(1, 6), Player.Instance.transform.position.y + Random.Range(1, 6), 0);
        }
        else
        {
            transform.position = _target;
        }
        Anim.enabled = true;
    }

    public void BombOver()
    {
        Anim.enabled = false;
        attackList.Clear();
        ObjectPool.Instance.ReturnCacheGameObject(PrefabType.RangeBombFX, this.gameObject);
    }
}

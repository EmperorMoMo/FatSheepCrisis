using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBall : MonoBehaviour
{
    private Animator _anim;
    private CapsuleCollider2D _capsuleCollider;
    private AutoDestory autoDestroy;

    private Animator Anim
    {
        get
        {
            if (_anim == null) _anim = GetComponent<Animator>();
            return _anim;
        }
    }
    private CapsuleCollider2D CapsuleCollider
    {
        get
        {
            if (_capsuleCollider == null) _capsuleCollider = GetComponent<CapsuleCollider2D>();
            return _capsuleCollider;
        }
    }

    private Vector2 target;
    private float attackRatio;
    private bool activeSelf, canMove;

    private void Start()
    {
        autoDestroy = gameObject.AddComponent<AutoDestory>();
        autoDestroy.delayTime = 2f;
        autoDestroy.type = PrefabType.WaterBallFX;
    }

    private void OnEnable()
    {
        activeSelf = canMove = true;
    }

    void Update()
    {
        if (activeSelf && canMove)
        {
            transform.Translate(transform.right * Time.deltaTime * 6f, Space.World);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Damageable>(out Damageable damageable))
        {
            if (collision.CompareTag("Player")) return;

            Anim.SetTrigger("hit");
            DamageMessage data = new DamageMessage()
            {
                damage = XTool.CalculateDamage(TotalAttribute.Aggressivity * attackRatio),
            };
            damageable.OnDamage(data);
        }
    }

    public void SetParamter(Vector2 _target, float _attackRatio)
    {
        target = _target;
        attackRatio = _attackRatio;
        AdjustRotation(target);
        CapsuleCollider.enabled = true;
    }

    private void AdjustRotation(Vector3 _target)
    {
        transform.eulerAngles = Vector3.zero;
        if (transform.position.y > _target.y)
        {
            transform.eulerAngles = new Vector3(0, 0, -Vector2.Angle((_target - transform.position), transform.right));
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, Vector2.Angle((_target - transform.position), transform.right));
        }
    }

    public void HitStart()
    {
        canMove = false;
        CapsuleCollider.enabled = false;
    }

    public void HitFinish()
    {
        activeSelf = false;
        canMove = true;
        ObjectPool.Instance.ReturnCacheGameObject(PrefabType.WaterBallFX, this.gameObject);
    }
}

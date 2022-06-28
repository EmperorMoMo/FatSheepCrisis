using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderBird : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer sprite;
    private CircleCollider2D circleCollider;

    private float timer;
    private float attackRatio;
    private Vector2 target;
    private int level;

    private bool canHit = true;
    private bool start;

    private void OnEnable()
    {
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, 0.02f);
            if (transform.position.Equals(target)) anim.SetTrigger("hit");
            timer -= Time.deltaTime;
            if (timer <= 0) anim.SetTrigger("hit");
        }
    }

    public void SetTimerAndTarget(Vector2 _target, float _timer, int _level, float _attackRatio)
    {
        level = _level;
        target = _target;
        timer = _timer;
        attackRatio = _attackRatio;
        start = true;

        AdjustRotation(_target);
        AdjustCircleColliderValue(Vector2.zero, level == 5 ? 1 : 0.5f);

        transform.localScale = Vector3.one;
        anim.SetInteger("level", _level);
    }

    private void AdjustRotation(Vector3 _target)
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
        if (transform.position.y > _target.y)
        {
            if (transform.position.x > _target.x)
            {
                sprite.flipY = true;
            }
            else
            {
                sprite.flipY = false;
            }
            transform.eulerAngles = new Vector3(0, 0, -Vector2.Angle((_target - transform.position), transform.right));
        }
        else
        {
            if (transform.position.x > _target.x)
            {
                sprite.flipY = true;
            }
            else
            {
                sprite.flipY = false;
            }
            transform.eulerAngles = new Vector3(0, 0, Vector2.Angle((_target - transform.position), transform.right));
        }
    }
    private void AdjustCircleColliderValue(Vector2 _offset,float _radius)
    {
        circleCollider.offset = _offset;
        circleCollider.radius = _radius;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Damageable>(out Damageable damageable))
        {
            if (collision.CompareTag("Player")) return;

            if (canHit) anim.SetTrigger("hit");

            DamageMessage data = new DamageMessage()
            {
                damage = XTool.CalculateDamage(TotalAttribute.Aggressivity * attackRatio)
            };
            damageable.OnDamage(data);

        }


    }

    public void HitStart()
    {
        start = false;
        canHit = false;
        AdjustCircleColliderValue(new Vector2(0.33f, 0), 1.2f);
        transform.localScale = new Vector3(1, 1, 1) * 0.5f * level;
    }

    public void HitFinish()
    {
        canHit = true;
        ObjectPool.Instance.ReturnCacheGameObject(PrefabType.ThunderBirdFX, this.gameObject);
    }
}

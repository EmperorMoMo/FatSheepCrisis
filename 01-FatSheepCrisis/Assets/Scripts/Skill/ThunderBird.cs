using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderBird : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer sprite;
    private CircleCollider2D circleCollider;
    private AutoDestory autoDestroy;

    private float attackRatio;
    private Vector2 target;
    private int level;

    private bool canMove, activeSelf;

    private void Start()
    {
        autoDestroy = gameObject.AddComponent<AutoDestory>();
        autoDestroy.delayTime = 2f;
        autoDestroy.type = PrefabType.ThunderBirdFX;
    }

    private void OnEnable()
    {
        if (anim == null)
        {
            sprite = GetComponent<SpriteRenderer>();
            anim = GetComponent<Animator>();
            circleCollider = GetComponent<CircleCollider2D>();
        }
        activeSelf = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove && activeSelf)
        {
            transform.Translate(transform.right * Time.deltaTime * 6f, Space.World);
        }
    }

    public void SetParameter(Vector2 _target, int _level, float _attackRatio)
    {
        level = _level;
        target = _target;
        attackRatio = _attackRatio;
        canMove = true;

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

            anim.SetTrigger("hit");
            DamageMessage data = new DamageMessage()
            {
                damage = XTool.CalculateDamage(TotalAttribute.Aggressivity * attackRatio)
            };
            damageable.OnDamage(data);

        }


    }

    public void HitStart()
    {
        canMove = false;
        AdjustCircleColliderValue(new Vector2(0.33f, 0), 1.2f);
        transform.localScale = new Vector3(1, 1, 1) * 0.5f * level;
    }

    public void HitFinish()
    {
        activeSelf = false;
        ObjectPool.Instance.ReturnCacheGameObject(PrefabType.ThunderBirdFX, this.gameObject);
    }
}

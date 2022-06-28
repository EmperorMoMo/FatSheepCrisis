using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeBomb : MonoBehaviour
{
    public SkillInfo skill;

    private float _timer;
    private Animator anim;
    private List<GameObject> attackList = new List<GameObject>();

    private void Start()
    {
        skill = SkillManager.Instance.ReadSkillConfig("·¶Î§Õ¨µ¯");
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= (skill.coolTime - SkillManager.Instance.GetLevel(skill.id) * 0.4f))
        {
            _timer = 0f;
            Bomb();
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
                damage = XTool.CalculateDamage(TotalAttribute.Aggressivity * skill.attackRatio),
                direction = (collision.transform.position - transform.position).normalized * skill.repelNum
            }; 
            damageable.OnDamage(data);
            attackList.Add(collision.gameObject);
        }
    }

    private void Bomb()
    {
        transform.parent = null;
        if (EnemyManager.Instance.ChooseEnemy(6, false, true).Equals(Vector2.one * 1000f))
        {
            transform.position = new Vector3(Player.Instance.transform.position.x + Random.Range(1, 6), Player.Instance.transform.position.y + Random.Range(1, 6), 0);
        }
        else
        {
            transform.position = EnemyManager.Instance.ChooseEnemy(6, false, true);
        }
        anim.enabled = true;
    }

    public void BombOver()
    {
        transform.parent = Player.Instance.transform;
        anim.enabled = false;
        attackList.Clear();
    }
}

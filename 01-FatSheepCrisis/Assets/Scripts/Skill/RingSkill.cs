using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RingType
{
    火元素环,
    水元素环,
    土元素环,
    雷元素环,
    木元素环
}

public class RingSkill : MonoBehaviour
{
    public RingType ringType;
    public SkillInfo skill;

    private Animator anim;
    private float _timer;

    private List<GameObject> attackList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        skill = SkillManager.Instance.ReadSkillConfig(ringType.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            anim.SetTrigger("attack");
            WoodBuff();
            _timer = skill.coolTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ringType == RingType.木元素环) return;

        if (collision.TryGetComponent<Damageable>(out Damageable damageable))
        {
            if (attackList.Contains(collision.gameObject)) return;
            DamageMessage data = new DamageMessage();

            switch (ringType)
            {
                case RingType.火元素环:
                    collision.GetComponent<BuffRun>().Run(BuffType.灼烧, skill.buffDurationTime, TotalAttribute.Aggressivity * (SkillManager.Instance.GetLevel(skill.id) * 0.1f));
                    break;
                case RingType.水元素环:
                    collision.GetComponent<BuffRun>().Run(BuffType.减速, skill.buffDurationTime + SkillManager.Instance.GetLevel(skill.id) * 0.2f);
                    break;
                case RingType.雷元素环:
                    data.damage = XTool.CalculateDamage(TotalAttribute.Aggressivity * (skill.attackRatio + SkillManager.Instance.GetLevel(skill.id) * 0.1f));
                    collision.GetComponent<BuffRun>().Run(BuffType.感电, skill.buffDurationTime);
                    damageable.OnDamage(data);
                    break;
                case RingType.土元素环:
                    data.direction = (collision.transform.position - Player.Instance.transform.position).normalized * (skill.repelNum + SkillManager.Instance.GetLevel(skill.id) * 0.1f);
                    damageable.OnDamage(data);
                    break;
            }
            attackList.Add(collision.gameObject);
        }
    }

    public void WoodBuff()
    {
        if (ringType != RingType.木元素环) return;
        if (Player.Instance.Cur_Hp != TotalAttribute.Max_Hp)
        {
            Player.Instance.RecoverHp(TotalAttribute.Aggressivity * (skill.attackRatio + SkillManager.Instance.GetLevel(skill.id) * 0.1f));
        }
    }


    public void AttackOver()
    {
        attackList.Clear();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RingType
{
    ��Ԫ�ػ�,
    ˮԪ�ػ�,
    ��Ԫ�ػ�,
    ��Ԫ�ػ�,
    ľԪ�ػ�
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
        if (ringType == RingType.ľԪ�ػ�) return;

        if (collision.TryGetComponent<Damageable>(out Damageable damageable))
        {
            if (attackList.Contains(collision.gameObject)) return;
            DamageMessage data = new DamageMessage();

            switch (ringType)
            {
                case RingType.��Ԫ�ػ�:
                    collision.GetComponent<BuffRun>().Run(BuffType.����, skill.buffDurationTime, TotalAttribute.Aggressivity * (SkillManager.Instance.GetLevel(skill.id) * 0.1f));
                    break;
                case RingType.ˮԪ�ػ�:
                    collision.GetComponent<BuffRun>().Run(BuffType.����, skill.buffDurationTime + SkillManager.Instance.GetLevel(skill.id) * 0.2f);
                    break;
                case RingType.��Ԫ�ػ�:
                    data.damage = XTool.CalculateDamage(TotalAttribute.Aggressivity * (skill.attackRatio + SkillManager.Instance.GetLevel(skill.id) * 0.1f));
                    collision.GetComponent<BuffRun>().Run(BuffType.�е�, skill.buffDurationTime);
                    damageable.OnDamage(data);
                    break;
                case RingType.��Ԫ�ػ�:
                    data.direction = (collision.transform.position - Player.Instance.transform.position).normalized * (skill.repelNum + SkillManager.Instance.GetLevel(skill.id) * 0.1f);
                    damageable.OnDamage(data);
                    break;
            }
            attackList.Add(collision.gameObject);
        }
    }

    public void WoodBuff()
    {
        if (ringType != RingType.ľԪ�ػ�) return;
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

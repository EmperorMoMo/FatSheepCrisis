using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public enum WeaponName
{
    None,
    Ìú¸«=1001,
}

public class Weapon : WeaponBaseAttribute
{
    public WeaponName Name;

    private Animator anim;
    private AnimatorController AC;

    private List<GameObject> attackList = new List<GameObject>();

    private void Awake()
    {
        SetAttribute();

        anim = GetComponent<Animator>();
        AC = anim.runtimeAnimatorController as AnimatorController;
    }

    public override void SetAttribute()
    {
        PackageItem weapons = Resources.Load<PackageItem>("Config");
        Dictionary<string, WeaponData> Data = weapons.GetWeaponsData();
        foreach (var item in Data.Values)
        {
            if (int.Parse(item.Id) == (int)Name)
            {
                Aggressivity = float.Parse(item.Aggressivity);
                AttackSpeed = float.Parse(item.AttackSpeed);
                CritChance = float.Parse(item.CritChance);
                CritDamage = float.Parse(item.CritDamage);
                RepelNum = float.Parse(item.RepelNum);
                ProjectilesNum = int.Parse(item.ProjectilesNum);
                AttackRange = float.Parse(item.AttackRange);
                SkillDescription = item.SkillDescription;
            }
        }
    }

    public override void WeaponSkill()
    {
        throw new System.NotImplementedException();
    }

    public void Attack(float interval = 1f)
    {
        AC.layers[0].stateMachine.states[0].state.speed = interval;
        anim.SetTrigger("attack");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable;
        if (collision.TryGetComponent<Damageable>(out damageable))
        {
            if (attackList.Contains(collision.gameObject)) return;
            DamageMessage data = new DamageMessage()
            {
                damage = 3,
                direction = (collision.transform.position - Player.Instance.transform.position).normalized * RepelNum
            };
            damageable.OnDamage(data);
            //CameraController.Instance.CameraShake(0.1f);

            attackList.Add(collision.gameObject);
        }
    }

    public void AttackEnd()
    {
        attackList.Clear(); 
    }
}

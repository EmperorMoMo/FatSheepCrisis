using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Profession
{
    None=3000,
    Thieves,
    Berserker,
    Destoryer,
    Believer,
    Scholar,
}
public static class TalentSkillData
{
    public static float Max_Hp()
    {
        switch (Player.Instance.profession)
        {
            case Profession.Believer:
                return Player.Instance.Max_Hp * 1.5f;
        }
        return 0f;
    }
    public static float Re_Hp()
    {
        return 0f;
    }
    public static float Armor()
    {
        switch (Player.Instance.profession)
        {
            case Profession.Destoryer:
                return Mathf.Clamp(((int)(Player.Instance.Level / 5)) * 3 / 10f, 0, 3f);
        }
        return 0f;
    }
    public static float MoveSpeed()
    {
        return 0f;
    }
    public static float AttackSpeed()
    {
        return 0f;
    }
    public static float CritChance()
    {
        return 0f;
    }
    public static float CritDamage()
    {
        return 0f;
    }
    public static float PickUpRange()
    {
        return 0f;
    }
    public static float Exp_GainRate()
    {
        switch (Player.Instance.profession)
        {
            case Profession.Scholar:
                return 0.2f + Mathf.Clamp(((int)(Player.Instance.Level / 5)) * 10 / 100f, 0, 0.3f);
        }
        return 0f;
    }
    public static float Gold_GainRate()
    {
        return 0f;
    }
    public static int ProjectilesNum()
    {
        return 0;
    }
    public static float FinalDamage()
    {
        switch (Player.Instance.profession)
        {
            case Profession.Berserker:
                return (TotalAttribute.Max_Hp - Player.Instance.Cur_Hp) / 100f;
            case Profession.Destoryer:
                return Mathf.Clamp(((int)(Player.Instance.Level / 5)) * 3 / 100f, 0, 0.3f);
        }
        return 0f;
    }
    public static float ExtraDamage()
    {
        return 0f;
    }
    public static float AdditionalDamage()
    {
        switch (Player.Instance.profession)
        {
            case Profession.Believer:
                return (TotalAttribute.Max_Hp) * (0.5f + Mathf.Clamp(((int)(Player.Instance.Level / 5) * 5) / 100f, 0, 0.5f));
        }
        return 0f;
    }
}

public class Player : CharacterBaseAttribute
{
    public Profession profession;
    public SpriteRenderer armWeaponSprite;

    [HideInInspector]
    public ProfessionData professionData;
    [HideInInspector]
    public bool isCrit = false;

    private Transform Model;
    private Animator anim;
    private Rigidbody2D rigid;
    private Weapon weapon;
    private Damageable damageable;

    private Vector2 input;
    private float timer_01;
    private float timer_02;
    private bool death;

    private void OnEnable()
    {
        professionData = DataManager.Instance.ReadPlayerData("" + (int)profession);
        Cur_Hp = TotalAttribute.Max_Hp;

        rigid = GetComponent<Rigidbody2D>();
        Model = transform.Find("Model").GetComponent<Transform>();
        anim = Model.GetComponentInChildren<Animator>();

        damageable = GetComponent<Damageable>(); 
        damageable.invinciableTime = 0.5f;
        damageable.onHurtStart.AddListener(OnHurtStart);
        damageable.onHurtEnd.AddListener(OnHurtEnd);
        damageable.onDeath.AddListener(OnDeath);

        //SelectWeapon(1005, "½£");
    }


    private void Update()
    {
        if (death) return;
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        AnimatorControl();
        AutoAttack();
        AutoRecoverHp();
    }

    private void FixedUpdate()
    {
        Move();
    }

    ///TODO!!!
    private void SelectWeapon(int id,string type)
    {
        armWeaponSprite.sprite = XTool.LoadAssetAtPath<Sprite>("Assets/RawResources/Weapons/", id + ".png");
        armWeaponSprite.material.SetColor("_001Color", new Color(255 * 0.05f, 255 * 0.05f, 255 * 0.05f));

        List<Weapon> weapons = new List<Weapon>();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Weapon>() != null)
            {
                weapons.Add(transform.GetChild(i).GetComponent<Weapon>());
            }
        }
        foreach (Weapon item in weapons)
        {
            if (item.Type == (WeaponType)System.Enum.Parse(typeof(WeaponType), type, false))
            {
                item.gameObject.SetActive(true);
                item.SetAttribute(id);
                weapon = item;
            }
        }
    }

    private void Move()
    {
        if (death)
        {
            rigid.velocity = Vector3.zero;
            return;
        }
        rigid.velocity = input.normalized * MoveSpeed * 5;
    }

    private void AnimatorControl()
    {
        FlipControl();
        if (input != Vector2.zero)
        {
            anim.SetFloat("RunState", 0.5f);
        }
        else
        {
            anim.SetFloat("RunState", 0f);
        }
    }

    private void FlipControl()
    {
        if (input.x > 0)
        {
            Model.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (input.x < 0)
        {
            Model.eulerAngles = Vector3.zero;
        }
    }

    private void AutoAttack()
    {
        if (weapon == null) return;
        if (Input.GetMouseButtonDown(1))
            weapon.Attack(AttackSpeed);
        //timer += Time.deltaTime;
        //if (timer >= weapon.AttackInterval)
        //{
        //    timer = 0f;
        //    weapon.Attack(AttackSpeed);
        //}
    }

    private void AutoRecoverHp()
    {
        if (Cur_Hp != TotalAttribute.Max_Hp)
        {
            timer_02 += Time.deltaTime;
            if (timer_02 >= 1)
            {
                timer_02 = 0;
                Cur_Hp += TotalAttribute.Re_Hp;
                ObjectPool.Instance.RequestCacheGameObject(PrefabType.NumberText, transform.position + Vector3.up * 1.5f, TotalAttribute.Re_Hp, 2);
            }
        }
    }

    private void OnHurtStart(Damageable damageable,DamageMessage data)
    {
        Debug.Log("Max_Hp:" + Max_Hp + "---Cur_Hp:" + Cur_Hp);
        anim.SetTrigger("Hurt");
        ObjectPool.Instance.RequestCacheGameObject(PrefabType.NumberText, transform.position + Vector3.up * 1.5f, data.damage);
    }

    private void OnDeath(Damageable damageable,DamageMessage data)
    {
        death = true;
        anim.SetBool("Death", death);
        ObjectPool.Instance.RequestCacheGameObject(PrefabType.NumberText, transform.position + Vector3.up * 1.5f, data.damage);
    }

    private void OnHurtEnd()
    {

    }
}

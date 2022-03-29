using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public enum WeaponName
{
    None,
    Ìú¸«=1001,
    Ìú´¸,
    ÁÒÑæÉñ¸«,
    À×öªÖ®´¸,
    Ìú½£,
}

public enum WeaponType
{
    ÎÞ,
    ¸«,
    ´¸,
    ½£,
    Êé,
    ÕÈ
}


public class Weapon : WeaponBaseAttribute
{
    public WeaponName Name;
    public WeaponType Type;
    public SpriteRenderer weaponSprite;

    [System.Serializable]
    public struct FX
    {
        public WeaponName _Name;
        public GameObject _FX;
    }
    public FX[] fx;

    private Animator anim;
    private AnimatorController AC;

    private List<GameObject> attackList = new List<GameObject>();

    private Damageable damageable;
    private float _Aggressivity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        AC = anim.runtimeAnimatorController as AnimatorController;
    }

    public override void SetAttribute(int id)
    {
        weaponSprite.sprite = XTool.LoadAssetAtPath<Sprite>("Assets/RawResources/Weapons/", id + ".png");
        Name = (WeaponName)Enum.Parse(typeof(WeaponName), id.ToString(), false);
        PackageItem weapons = Resources.Load<PackageItem>("Config");
        Dictionary<string, WeaponData> Data = weapons.GetWeaponsData();
        foreach (var item in Data.Values)
        {
            if (int.Parse(item.Id) == (int)Name)
            {
                Type = (WeaponType)Enum.Parse(typeof(WeaponType), item.Type, false);
                Aggressivity = float.Parse(item.Aggressivity);
                AttackInterval = float.Parse(item.AttackInterval);
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

    public void Attack(float speed = 1f)
    {
        if (anim == null) return;
        AC.layers[0].stateMachine.states[0].state.speed = speed;
        if(Player.Instance.Model.eulerAngles.y == 180)
        {
            anim.SetTrigger("attack_positive");
        }
        else
        {
            anim.SetTrigger("attack_negative");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Damageable>(out damageable))
        {
            if (attackList.Contains(collision.gameObject)) return;
            DamageMessage data = new DamageMessage()
            {
                damage = CalculateDamage(),
                direction = (collision.transform.position - Player.Instance.transform.position).normalized * RepelNum
            };
            damageable.OnDamage(data);
            //CameraController.Instance.CameraShake(0.1f);

            attackList.Add(collision.gameObject);
        }
    }

    private float CalculateDamage()
    {
        Player.Instance.isCrit = false;
        if (UnityEngine.Random.value < (CritChance + TotalAttribute.CritChance))
        {
            _Aggressivity = Aggressivity * (1 + CritDamage + TotalAttribute.CritDamage);
            Player.Instance.isCrit = true;
        }
        else
        {
            _Aggressivity = Aggressivity;
        }
        return _Aggressivity * (1 + TotalAttribute.FinalDamage) + TotalAttribute.AdditionalDamage;
    }


    #region ¶¯»­ÊÂ¼þ
    public void AttackOver()
    {
        attackList.Clear(); 
    }

    public void WeaponFXStart(int i = 0)
    {
        foreach (var item in fx)
        {
            if (item._Name == Name)
            {
                switch (item._Name)
                {
                    case WeaponName.Ìú´¸:
                        item._FX.SetActive(true);
                        break;
                    case WeaponName.Ìú¸«:
                        Debug.Log("Ìú¸«");
                        item._FX.SetActive(true);
                        break;
                    case WeaponName.ÁÒÑæÉñ¸«:
                        item._FX.GetComponent<SpriteRenderer>().flipX = (i == 0 ? false : true);
                        item._FX.SetActive(true);
                        break;
                }
            }
        }
    }

    public void WeaponFXEnd()
    {
        foreach (var item in fx)
        {
            if (item._FX.activeInHierarchy)
            {
                item._FX.SetActive(false);
            }
        }
    }
    #endregion
}

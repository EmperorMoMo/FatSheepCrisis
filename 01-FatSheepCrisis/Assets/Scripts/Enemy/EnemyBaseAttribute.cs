using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Damageable))]
public abstract class EnemyBaseAttribute : MonoBehaviour
{
    protected internal float Aggressivity { get; set; }
    protected internal float Armor { get; set; }
    protected internal float Max_Hp { get; set; }
    protected internal float Cur_Hp { get; set; }
    protected internal float MoveSpeed { get; set; }
    protected internal float DefenseRepelNum { get; set; }

    public abstract void SetAttribute();

}

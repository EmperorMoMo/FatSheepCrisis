using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SkillInfo
{

    [Header("ExcelData")]
    public int id;

    public string name;

    public string description;

    public float attackRatio;

    public float coolTime;

    public float range;

    public float distance;

    public float moveSpeed;

    public float durationTime;

    public float buffDurationTime;

    public float repelNum;
}

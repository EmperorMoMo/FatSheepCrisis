using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{
    [System.Serializable]
    public struct SkillPrefabs
    {
        public int id;
        [Min(1)]
        private int level;
        public GameObject gameObject;
    }

    public List<SkillPrefabs> skillPrefabs;

    public Dictionary<int, int> playerSkillGroup = new Dictionary<int, int>();

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) InstantiateFX(4001, Player.Instance.transform.position);
        if (Input.GetKeyDown(KeyCode.X)) InstantiateFX(4002, Player.Instance.transform.position);
        if (Input.GetKeyDown(KeyCode.C)) InstantiateFX(4003, Player.Instance.transform.position);
        if (Input.GetKeyDown(KeyCode.V)) InstantiateFX(4004, Player.Instance.transform.position);
        if (Input.GetKeyDown(KeyCode.B)) InstantiateFX(4005, Player.Instance.transform.position);
        //if (Input.GetKeyDown(KeyCode.Z)) InstantiateFX(4006, Player.Instance.transform.position);
    }

    public SkillInfo ReadSkillConfig(string name)
    {
        PackageItem skills = Resources.Load<PackageItem>("Config");
        Dictionary<string, SkillData> Data = skills.GetSkillData();
        foreach (var item in Data.Values)
        {
            if (item.Name == name)
            {
                SkillInfo _data = new SkillInfo()
                {
                    id = int.Parse(item.Id),
                    name = item.Name,
                    attackRatio = float.Parse(item.AttackRatio),
                    coolTime = float.Parse(item.CoolTime),
                    range = float.Parse(item.Range),
                    distance = float.Parse(item.Distance),
                    moveSpeed = float.Parse(item.MoveSpeed),
                    durationTime = float.Parse(item.DurationTime),
                    buffDurationTime = float.Parse(item.BuffDurationTime),
                    repelNum = float.Parse(item.RepelNum),
                    description = item.Description
                };
                return _data;
            }
        }
        return null;
    }

    void InstantiateFX(int id, Vector3 pos)
    {
        int level = 1;
        foreach (var item in skillPrefabs)
        {
            if (id == item.id)
            {
                if (!playerSkillGroup.ContainsKey(id))
                {
                    playerSkillGroup.Add(id, level);
                    Instantiate(item.gameObject, pos, Quaternion.identity, Player.Instance.transform);
                }
                else
                {
                    playerSkillGroup.TryGetValue(id, out level);
                    level += 1;
                    level = Mathf.Clamp(level, 1, 5);
                    playerSkillGroup[id] = level;
                }
            }
        }
    }

    public int GetLevel(int id)
    {
        int level = 0;
        if (playerSkillGroup.ContainsKey(id)) level = playerSkillGroup[id];
        return level;
    }

    private void OnDestroy()
    {
        playerSkillGroup.Clear();
    }
}

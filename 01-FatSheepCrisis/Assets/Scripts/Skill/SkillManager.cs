using System;
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

    private Action autoRelease;

    private Dictionary<int, float> timer = new Dictionary<int, float>();
    private Dictionary<string, SkillInfo> _skillInfo = new Dictionary<string, SkillInfo>();

    void Start()
    {
        AddTimer();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Test();
        autoRelease?.Invoke();
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
                    if (id < 4006) Instantiate(item.gameObject, pos, Quaternion.identity, Player.Instance.transform);
                    else RegisterSkill(id);
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
        autoRelease = null;
    }

    private void Test()
    {
        if (Input.GetKeyDown(KeyCode.Z)) InstantiateFX(4001, Player.Instance.transform.position);
        if (Input.GetKeyDown(KeyCode.X)) InstantiateFX(4002, Player.Instance.transform.position);
        if (Input.GetKeyDown(KeyCode.C)) InstantiateFX(4003, Player.Instance.transform.position);
        if (Input.GetKeyDown(KeyCode.V)) InstantiateFX(4004, Player.Instance.transform.position);
        if (Input.GetKeyDown(KeyCode.B)) InstantiateFX(4005, Player.Instance.transform.position);
        if (Input.GetKeyDown(KeyCode.N)) InstantiateFX(4006, Player.Instance.transform.position);
        if (Input.GetKeyDown(KeyCode.M)) InstantiateFX(4007, Player.Instance.transform.position);
        if (Input.GetKeyDown(KeyCode.L)) InstantiateFX(4008, Player.Instance.transform.position);
    }

    private void AddTimer()
    {
        foreach (var item in skillPrefabs)
        {
            timer[item.id] = 0;
        }
    }
    private SkillInfo skillInfo(string _name)
    {
        if (!_skillInfo.ContainsKey(_name))
        {
            _skillInfo[_name] = ReadSkillConfig(_name);
        }
        return _skillInfo[_name];
    }
    private Vector2 Target(string _name)
    {
        Vector2 _temp = Vector2.zero;
        switch (_name)
        {
            case "����":
                _temp = EnemyManager.Instance.ChooseEnemy(10, true);
                break;
            case "��Χը��":
                _temp = EnemyManager.Instance.ChooseEnemy(6, false, true);
                break;
            case "ˮ��":
                _temp = EnemyManager.Instance.ChooseEnemy(10, false, true);
                break;
        }
        return _temp;
    }
    private void RegisterSkill(int _id)
    {
        switch (_id)
        {
            case 4006:
                autoRelease += RangeBombLauncher;
                break;
            case 4007:
                autoRelease += ThunderBirdLauncher;
                break;
            case 4008:
                autoRelease += WaterBallLauncher;
                break;
        }
    }

    #region SkillMethod

    private void RangeBombLauncher()
    {
        timer[skillInfo("��Χը��").id] += Time.deltaTime;
        if (timer[skillInfo("��Χը��").id] >= (skillInfo("��Χը��").coolTime - (GetLevel(skillInfo("��Χը��").id) - 1) * 0.5f)) 
        {
            GameObject temp = ObjectPool.Instance.RequestCacheGameObject(PrefabType.RangeBombFX, Player.Instance.transform.position);
            temp.GetComponent<RangeBomb>().SetParameter(Target("��Χը��"), skillInfo("��Χը��").attackRatio, skillInfo("��Χը��").repelNum);
            timer[skillInfo("��Χը��").id] = 0f;
        }
    }

    private void ThunderBirdLauncher()
    {
        timer[skillInfo("����").id] += Time.deltaTime;
        if (timer[skillInfo("����").id] >= (skillInfo("����").coolTime - (GetLevel(skillInfo("����").id) - 1) * 0.25f))
        {
            timer[skillInfo("����").id] = 0f;
            if (Target("����").Equals(Vector2.one * 1000f)) return;
            GameObject temp = ObjectPool.Instance.RequestCacheGameObject(PrefabType.ThunderBirdFX, Player.Instance.transform.position + new Vector3(0, 0.5f, 0));
            temp.GetComponent<ThunderBird>().SetParameter(Target("����"), GetLevel(skillInfo("����").id), skillInfo("����").attackRatio);
        }
    }

    private void WaterBallLauncher()
    {
        timer[skillInfo("ˮ��").id] += Time.deltaTime;
        if (timer[skillInfo("ˮ��").id] >= (skillInfo("ˮ��").coolTime - (GetLevel(skillInfo("ˮ��").id) - 1) * 0.25f)) 
        {
            timer[skillInfo("ˮ��").id] = 0f;
            if (Target("ˮ��").Equals(Vector2.one * 1000f)) return;
            GameObject temp = ObjectPool.Instance.RequestCacheGameObject(PrefabType.WaterBallFX, Player.Instance.transform.position + new Vector3(0, 0.5f, 0));
            temp.GetComponent<WaterBall>().SetParamter(Target("ˮ��"), skillInfo("ˮ��").attackRatio);
        }
    }

    #endregion
}

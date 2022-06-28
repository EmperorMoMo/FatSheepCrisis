using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderBirdLauncher : MonoBehaviour
{
    public SkillInfo skill;

    private float _timer;
    private Vector2 Target
    {
        get
        {
            return EnemyManager.Instance.ChooseEnemy(10, true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        skill = SkillManager.Instance.ReadSkillConfig("À×Äñ");
        _timer = skill.coolTime;
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= (skill.coolTime - SkillManager.Instance.GetLevel(skill.id) * 0.2f))
        {
            if (Target != (Vector2.one * 1000f))
            {
                GameObject temp = ObjectPool.Instance.RequestCacheGameObject(PrefabType.ThunderBirdFX, Player.Instance.transform.position);
                temp.GetComponent<ThunderBird>().SetTimerAndTarget(Target, 3, SkillManager.Instance.GetLevel(skill.id), skill.attackRatio);
            }
            _timer = 0f;
        }
    }

    
}

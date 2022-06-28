using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    public List<GameObject> enemyPrefabs;
    bool startGame;
    float _timer;

    [HideInInspector]
    public List<Transform> enemyList = new List<Transform>();

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        EventCenter.AddListener(EventType.StartGame, StartGame);
    }

    private void StartGame()
    {
        startGame = true;
    }

    private void Update()
    {
        if (startGame)
        {
            //_timer += Time.deltaTime;
            //if (_timer > 1f)
            //{
            //    _timer = 0f;
            //    RandomInstantiate();
            //}
            if(Input.GetMouseButton(2)) RandomInstantiate();
        }
    }

    private void RandomInstantiate()
    {
        GameObject obj = Instantiate(enemyPrefabs[Random.Range(0, 5)], XTool.RangeInsideCirclePosition(Player.Instance.transform.position, 9, 13), Quaternion.identity);
        enemyList.Add(obj.transform);
    }

    public Vector2 ChooseEnemy(float distance, bool isNearst = false, bool isRandom = false)
    {
        List<Vector2> temp = new List<Vector2>();
        foreach (var item in enemyList)
        {
            if (Vector2.Distance(Player.Instance.transform.position, item.position) <= distance)
            {
                temp.Add(item.position);
            }
        }
        if (temp.Count != 0)
        {
            if (isRandom)
            {
                return temp[Random.Range(0, temp.Count)];
            }
            if (isNearst)
            {
                for (int i = 0; i < temp.Count; i++)
                {
                    for (int j = 0; j < temp.Count - 1 - i; j++)
                    {
                        if (Vector2.Distance(Player.Instance.transform.position, temp[j]) > Vector2.Distance(Player.Instance.transform.position, temp[j + 1]))
                        {
                            Vector2 _temp = temp[j];
                            temp[j] = temp[j + 1];
                            temp[j + 1] = _temp;
                        }
                    }
                }
            }
            return temp[0];
        }
        else
        {
            return Vector2.one * 1000f;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> enemyes;
    bool startGame;
    float _timer;

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
            _timer += Time.deltaTime;
            if (_timer > 1f)
            {
                _timer = 0f;
                RandomInstantiate();
            }
        }
    }

    private void RandomInstantiate()
    {
        Instantiate(enemyes[Random.Range(0, 5)], XTool.RangeInsideCirclePosition(Player.Instance.transform.position, 9, 12), Quaternion.identity);
    }
}

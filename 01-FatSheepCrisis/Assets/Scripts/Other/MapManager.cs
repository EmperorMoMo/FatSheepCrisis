using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private List<Transform> transforms = new List<Transform>();

    public Vector2 temp;

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transforms.Add(transform.GetChild(i).GetComponent<Transform>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.Instance == null) return;
        for (int i = 0; i < transforms.Count; i++)
        {
            if (Mathf.Abs(Player.Instance.transform.position.x - transforms[i].position.x) >= (temp.x * 2)) 
            {
                //if (Player.Instance.transform.position.x > transforms[i].position.x)
                //{
                //    transforms[i].position = new Vector2(transforms[i].position.x + (temp.x * 3), transforms[i].position.y);
                //}
                //else if(Player.Instance.transform.position.x < transforms[i].position.x)
                //{
                //    transforms[i].position = new Vector2(transforms[i].position.x - (temp.x * 3), transforms[i].position.y);
                //}
                transforms[i].position = new Vector2(transforms[i].position.x + (temp.x * 3 * (Player.Instance.transform.position.x >= transforms[i].position.x ? 1 : -1)), transforms[i].position.y);
            }
            if (Mathf.Abs(Player.Instance.transform.position.y - transforms[i].position.y) >= (temp.y * 2)) 
            {
                //if (Player.Instance.transform.position.y > transforms[i].position.y) 
                //{
                //    transforms[i].position = new Vector2(transforms[i].position.x, transforms[i].position.y + (temp.y * 3));
                //}
                //else if (Player.Instance.transform.position.y < transforms[i].position.y) 
                //{
                //    transforms[i].position = new Vector2(transforms[i].position.x, transforms[i].position.y - (temp.y * 3));
                //}
                transforms[i].position = new Vector2(transforms[i].position.x, transforms[i].position.y + (temp.y * 3 * (Player.Instance.transform.position.y >= transforms[i].position.y ? 1 : -1)));
            }
        }
    }
}

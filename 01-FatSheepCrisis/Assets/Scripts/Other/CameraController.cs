using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    //private Transform target;

    //[Header("SmoothSpeed and Clamp size")]
    //[SerializeField] private float smoothSpeed;

    //[Header("Camera Shake")]
    //private Vector3 shakeActive;
    //private float shakeAmplify;
    //private bool shakeEnd = true;
    private Cinemachine.CinemachineVirtualCamera cine;

    // Start is called before the first frame update
    void Start()
    {
        //target = GameObject.FindGameObjectWithTag("Player").transform;
        cine = GameObject.Find("CM vcam1").GetComponent<Cinemachine.CinemachineVirtualCamera>();
        EventCenter.AddListener(EventType.StartGame, StartGame);
    }

    void StartGame()
    {
        cine.Follow = Player.Instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //if (shakeAmplify > 0)
        //{
        //    shakeEnd = false;
        //    shakeActive = new Vector3(Random.Range(-shakeAmplify, shakeAmplify), Random.Range(-shakeAmplify, shakeAmplify), 0f);
        //    shakeAmplify -= Time.deltaTime;
        //}
        //else
        //{
        //    shakeEnd = true;
        //    shakeActive = Vector3.zero;
        //}
        //transform.position += shakeActive;
    }

    //private void LateUpdate()
    //{
    //    transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, -10f), smoothSpeed * Time.deltaTime);
    //    //SceneView.lastActiveSceneView.pivot = transform.position;
    //}

    //public void CameraShake(float _amount)
    //{
    //    //if (!shakeEnd) return;
    //    shakeAmplify = _amount;
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    private Transform target;

    [Header("SmoothSpeed and Clamp size")]
    [SerializeField] private float smoothSpeed;

    [Header("Camera Shake")]
    private Vector3 shakeActive;
    private float shakeAmplify;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeAmplify > 0)
        {
            shakeActive = new Vector3(Random.Range(-shakeAmplify, shakeAmplify), Random.Range(-shakeAmplify, shakeAmplify), 0f);
            shakeAmplify -= Time.deltaTime;
        }
        else
        {
            shakeActive = Vector3.zero;
        }
        transform.position += shakeActive;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, -10f), smoothSpeed * Time.deltaTime);
        //SceneView.lastActiveSceneView.pivot = transform.position;
    }

    public void CameraShake(float _amount)
    {
        shakeAmplify = _amount;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if ((object)instance == (object)null)
            {
                try
                {
                    instance = (T)UnityEngine.Object.FindObjectOfType(typeof(T));
                }
                catch(Exception e)
                {
                    Debug.LogError(e);
                }
            }
            return instance;
        }
    }
}

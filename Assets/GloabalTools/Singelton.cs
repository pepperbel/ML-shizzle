﻿using UnityEngine;

public class Singelton<T> : MonoBehaviour  where T : MonoBehaviour{
    private static bool shuttingDown = false;
    private static object lockObj = new object();
    private static T instance;

    public static T Instance {
        get {
            if (shuttingDown) {
                Debug.LogError("[Singelton::Get] Instance '" + typeof(T) + "' destroyed!");
                return null;
            }
            lock (lockObj) {
                if (!instance) instance = (T)FindObjectOfType(typeof(T));
                if (!instance) {
                    var singletonObj = new GameObject();
                    instance = singletonObj.AddComponent<T>();
                    singletonObj.name = typeof(T).ToString() + " (Singelton)";
                    DontDestroyOnLoad(singletonObj);
                }
            }
            return instance;
        }
    }
}

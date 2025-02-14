using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected Singleton() { }
    protected bool dontDestroy = true;
    private static bool isQuitting = false;
    private static T instance;
    public static T Instance
    {
        get
        {
            if (isQuitting)
            {
                // Debug.LogWarning($"[Singleton] {typeof(T).Name} 인스턴스에 접근할 수 없습니다. 앱 종료중입니다.");
                return null;
            }

            if (instance == null)
            {
                instance = FindObjectOfType(typeof(T)) as T;
                if (instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name, typeof(T));
                    instance = obj.GetComponent<T>();
                }
            }
            return instance;
        }
    }
    protected virtual void Awake()
    {
        if (instance != null)
        {
            Destroy(transform.root.gameObject);
            return;
        }
        else
        {
            instance = this as T;
        }

        if (dontDestroy && transform.parent != null && transform.root != null)
        {
            DontDestroyOnLoad(transform.root.gameObject);
        }
        else 
        {
            GameObject rootManagerGO = GameObject.FindGameObjectWithTag("Manager");
            if (rootManagerGO != null)
            {
                transform.SetParent(rootManagerGO.transform);
            }
            else if (dontDestroy)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }

    private void OnApplicationQuit()
    {
        isQuitting = true;
    }

    protected virtual void OnDestroy()
    {
        // Debug.Log($"[Singleton] {typeof(T).Name} 인스턴스가 삭제됩니다.");
        isQuitting = true;
        instance = null;
    }
}

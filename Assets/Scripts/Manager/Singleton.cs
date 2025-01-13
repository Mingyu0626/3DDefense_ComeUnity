using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected bool dontDestroy = true;
    protected bool isDestroyed = false; // Destroy 이전에 등록한 이벤트 함수 중복 호출 방지
    private static T instance;
    public static T Instance
    {
        get
        {
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
            isDestroyed = true;
            return;
        }

        if (transform.parent != null && transform.root != null)
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
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
    protected virtual void OnDestroy()
    {

    }
}

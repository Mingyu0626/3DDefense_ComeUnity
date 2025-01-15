using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected Singleton() { }
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

    protected bool dontDestroy = true;
    protected bool isDestroyed = false; // Destroy ������ ����� �̺�Ʈ �Լ� �ߺ� ȣ�� ����

    protected virtual void Awake()
    {
        if (instance != null)
        {
            Destroy(transform.root.gameObject);
            isDestroyed = true;
            return;
        }
        else
        {
            instance = this as T;
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

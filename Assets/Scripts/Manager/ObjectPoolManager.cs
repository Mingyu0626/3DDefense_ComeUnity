using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{
    [System.Serializable]
    private class ObjectInfo
    {
        public string objectName;
        public GameObject prefab;
        public int count; // 미리 생성해놓을 오브젝트 수
    }
    [SerializeField]
    private ObjectInfo[] objectInfos = null;

    public static ObjectPoolManager Instance { get; private set; }
    public bool isReady { get; private set; }
    private string objectName;
    private Dictionary<string, IObjectPool<GameObject>> objectPoolDic = new Dictionary<string, IObjectPool<GameObject>>();
    private Dictionary<string, GameObject> goDic = new Dictionary<string, GameObject>();

    // 풀링 오브젝트 일괄 비활성화를 위한 List
    private List<GameObject> activeObjects = new List<GameObject>(); 

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        Init();
    }

    void Init()
    {
        for (int i = 0; i < objectInfos.Length; i++)
        {
            IObjectPool<GameObject> pool = new ObjectPool<GameObject>(
                CreatePooledItem, 
                OnTakeFromPool, 
                OnReturnedPool,
                OnDestroyPoolObject, 
                true, 
                objectInfos[i].count, 
                objectInfos[i].count);

            if (goDic.ContainsKey(objectInfos[i].objectName))
            {
                Debug.LogFormat("{0}은 이미 등록된 오브젝트입니다.",  objectInfos[i].objectName);
                return;
            }
            goDic.Add(objectInfos[i].objectName, objectInfos[i].prefab);
            objectPoolDic.Add(objectInfos[i].objectName, pool);

            for (int j = 0; j < objectInfos[i].count; j++)
            {
                objectName = objectInfos[i].objectName;
                PoolAble poolAbleGo = CreatePooledItem().GetComponent<PoolAble>();
                poolAbleGo.Pool.Release(poolAbleGo.gameObject);
            }
        }
        Debug.Log("오브젝트 풀링 준비 완료");
        isReady = true;
    }

    private GameObject CreatePooledItem()
    {
        GameObject poolGameObject = Instantiate(goDic[objectName]);
        poolGameObject.GetComponent<PoolAble>().Pool = objectPoolDic[objectName];
        return poolGameObject;
    }

    private void OnTakeFromPool(GameObject poolGameObject)
    {
        poolGameObject.SetActive(true);
        activeObjects.Add(poolGameObject);
    }

    private void OnReturnedPool(GameObject poolGameObject)
    {
        poolGameObject.SetActive(false);
        activeObjects.Remove(poolGameObject);
    }

    private void OnDestroyPoolObject(GameObject poolGameObject)
    {
        Destroy(poolGameObject);
    }

    public GameObject GetGameObject(string gameObjectName)
    {
        objectName = gameObjectName;
        if (!goDic.ContainsKey(objectName))
        {
            Debug.LogFormat("{0}은 오브젝트풀에 등록되지 않은 오브젝트입니다.", objectName);
            return null;
        }
        return objectPoolDic[objectName].Get();
    }

    // 모든 활성화된 오브젝트를 풀에 반환
    public void ReturnAllActiveObjectsToPool()
    {
        foreach (GameObject activeObject in activeObjects.ToArray())
        {
            string activeObjectName = activeObject.name.Replace("(Clone)", "");
            if (objectPoolDic.ContainsKey(activeObjectName))
            {
                objectPoolDic[activeObjectName].Release(activeObject);
            }
            else
            {
                Debug.LogWarning("풀에 해당 오브젝트가 존재하지 않음 : " + activeObjectName);
            }
        }
        activeObjects.Clear();
    }
}

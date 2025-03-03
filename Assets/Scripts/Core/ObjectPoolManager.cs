using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    private string objectName;

    [System.Serializable]
    private class ObjectInfo
    {
        public string objectName; // 생성할 오브젝트의 이름
        public GameObject prefab; // 생성할 GO 
        public int count; // 미리 생성해놓을 오브젝트 수
    }

    [SerializeField]
    private ObjectInfo[] objectInfoArray = null;

    private class ObjectPoolData
    {
        public GameObject prefab;
        public IObjectPool<GameObject> pool;

        public ObjectPoolData(GameObject prefab, IObjectPool<GameObject> pool)
        {
            this.prefab = prefab;
            this.pool = pool;
        }
    };

    // 오브젝트를 최초로 생성하고, 생성 후 풀링을 위한 Dictionary
    private Dictionary<string, ObjectPoolData> objectPoolDic 
        = new Dictionary<string, ObjectPoolData>();

    // 풀링 오브젝트 일괄 비활성화를 위한 List
    private List<GameObject> activeObjects = new List<GameObject>(); 

    protected override void Awake()
    {
        Init();
    }

    void Init()
    {
        for (int i = 0; i < objectInfoArray.Length; i++)
        {
            IObjectPool<GameObject> pool = new ObjectPool<GameObject>(
                CreatePooledObject, 
                OnTakeFromPool, 
                OnReturnedPool,
                OnDestroyPoolObject, 
                true, 
                objectInfoArray[i].count, 
                objectInfoArray[i].count);

            if (objectPoolDic.ContainsKey(objectInfoArray[i].objectName))
            {
                Debug.LogFormat("{0}은 이미 등록된 오브젝트입니다.",  objectInfoArray[i].objectName);
                return;
            }
            objectPoolDic.Add(objectInfoArray[i].objectName, new ObjectPoolData(objectInfoArray[i].prefab, pool));

            for (int j = 0; j < objectInfoArray[i].count; j++)
            {
                objectName = objectInfoArray[i].objectName;
                PoolAble poolAbleGo = CreatePooledObject().GetComponent<PoolAble>();
                if (poolAbleGo != null)
                {
                    poolAbleGo.Pool.Release(poolAbleGo.gameObject);
                }
            }
        }
    }

    private GameObject CreatePooledObject()
    {
        if (!objectPoolDic.ContainsKey(objectName))
        {
            Debug.LogFormat("{0}은 오브젝트풀에 등록되지 않은 오브젝트입니다.", objectName);
            return null;
        }
        GameObject poolGameObject = Instantiate(objectPoolDic[objectName].prefab);

        if (poolGameObject != null && poolGameObject.GetComponent<PoolAble>() != null)
        {
            poolGameObject.GetComponent<PoolAble>().Pool = objectPoolDic[objectName].pool;
            return poolGameObject;
        }
        else
        {
            Debug.LogWarning("poolGameObject를 가져오는데 실패했습니다.");
            return null;
        }
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

    public GameObject GetObject(string gameObjectName)
    {
        objectName = gameObjectName;
        if (!objectPoolDic.ContainsKey(objectName))
        {
            Debug.LogFormat("{0}은 오브젝트풀에 등록되지 않은 오브젝트입니다.", objectName);
            return null;
        }
        return objectPoolDic[objectName].pool.Get();
    }

    public void ReturnAllActiveObjectsToPool()
    {
        foreach (GameObject activeObject in activeObjects.ToArray())
        {
            string activeObjectName = activeObject.name.Replace("(Clone)", "");
            if (objectPoolDic.ContainsKey(activeObjectName))
            {
                objectPoolDic[activeObjectName].pool.Release(activeObject);
            }
            else
            {
                Debug.LogWarning("풀에 해당 오브젝트가 존재하지 않습니다. : " + activeObjectName);
            }
        }
        activeObjects.Clear();
    }
}

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
        public string objectName; // ������ ������Ʈ�� �̸�
        public GameObject prefab; // ������ GO 
        public int count; // �̸� �����س��� ������Ʈ ��
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

    private Dictionary<string, ObjectPoolData> objectPoolDic 
        = new Dictionary<string, ObjectPoolData>(); // ������Ʈ�� ���ʷ� �����ϰ�, ���� �� Ǯ���� ���� Dictionary

    // Ǯ�� ������Ʈ �ϰ� ��Ȱ��ȭ�� ���� List
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
                CreatePooledItem, 
                OnTakeFromPool, 
                OnReturnedPool,
                OnDestroyPoolObject, 
                true, 
                objectInfoArray[i].count, 
                objectInfoArray[i].count);

            if (objectPoolDic.ContainsKey(objectInfoArray[i].objectName))
            {
                Debug.LogFormat("{0}�� �̹� ��ϵ� ������Ʈ�Դϴ�.",  objectInfoArray[i].objectName);
                return;
            }
            objectPoolDic.Add(objectInfoArray[i].objectName, new ObjectPoolData(objectInfoArray[i].prefab, pool));

            for (int j = 0; j < objectInfoArray[i].count; j++)
            {
                objectName = objectInfoArray[i].objectName;
                PoolAble poolAbleGo = CreatePooledItem().GetComponent<PoolAble>();
                if (poolAbleGo != null)
                {
                    poolAbleGo.Pool.Release(poolAbleGo.gameObject);
                }
            }
        }
        Debug.Log("������Ʈ Ǯ�� �غ� �Ϸ�");
    }

    private GameObject CreatePooledItem()
    {
        if (!objectPoolDic.ContainsKey(objectName))
        {
            Debug.LogFormat("{0}�� ������ƮǮ�� ��ϵ��� ���� ������Ʈ�Դϴ�.", objectName);
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
            Debug.LogWarning("poolGameObject�� �������µ� �����߽��ϴ�.");
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

    public GameObject GetGameObject(string gameObjectName)
    {
        objectName = gameObjectName;
        if (!objectPoolDic.ContainsKey(objectName))
        {
            Debug.LogFormat("{0}�� ������ƮǮ�� ��ϵ��� ���� ������Ʈ�Դϴ�.", objectName);
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
                Debug.LogWarning("Ǯ�� �ش� ������Ʈ�� �������� ���� : " + activeObjectName);
            }
        }
        activeObjects.Clear();
    }
}

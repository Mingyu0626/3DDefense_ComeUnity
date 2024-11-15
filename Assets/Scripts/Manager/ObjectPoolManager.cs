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
        public int count; // �̸� �����س��� ������Ʈ ��
    }
    [SerializeField]
    private ObjectInfo[] objectInfos = null;

    public static ObjectPoolManager Instance { get; private set; }
    public bool isReady { get; private set; }
    private string objectName;
    private Dictionary<string, IObjectPool<GameObject>> objectPoolDic = new Dictionary<string, IObjectPool<GameObject>>();
    private Dictionary<string, GameObject> goDic = new Dictionary<string, GameObject>();

    // Ǯ�� ������Ʈ �ϰ� ��Ȱ��ȭ�� ���� List
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
                Debug.LogFormat("{0}�� �̹� ��ϵ� ������Ʈ�Դϴ�.",  objectInfos[i].objectName);
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
        Debug.Log("������Ʈ Ǯ�� �غ� �Ϸ�");
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
            Debug.LogFormat("{0}�� ������ƮǮ�� ��ϵ��� ���� ������Ʈ�Դϴ�.", objectName);
            return null;
        }
        return objectPoolDic[objectName].Get();
    }

    // ��� Ȱ��ȭ�� ������Ʈ�� Ǯ�� ��ȯ
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
                Debug.LogWarning("Ǯ�� �ش� ������Ʈ�� �������� ���� : " + activeObjectName);
            }
        }
        activeObjects.Clear();
    }
}

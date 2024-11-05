using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private string spawnedEnemyName;
    private float spawnIntervalTime = 3f;

    void Awake()
    {
        
    }

    void Start()
    {
        StartCoroutine(SpawnEnemyCoroutine());
    }

    private IEnumerator SpawnEnemyCoroutine()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnIntervalTime);
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemyGameObject = ObjectPoolManager.Instance.GetGameObject(spawnedEnemyName);
        if (enemyGameObject != null)
        {
            float randX = Random.Range(-5f, 5f);
            float randZ = Random.Range(-5f, 5f);
            enemyGameObject.transform.position = transform.position + new Vector3(randX, 0, randZ);
            Debug.Log("Enemy Spawned!");
        }
    }
}

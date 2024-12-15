using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private string spawnedEnemyName;
    private float spawnIntervalTime = 3f;

    private void Awake()
    {

    }
    private void OnEnable()
    {
        StartCoroutine(SpawnEnemyCoroutine());
    }
    private void OnDisable()
    {
        StopCoroutine(SpawnEnemyCoroutine());
    }
    private IEnumerator SpawnEnemyCoroutine()
    {
        var wait = new WaitForSeconds(spawnIntervalTime);
        yield return wait;
        while (true)
        {
            SpawnEnemy();
            StageManager.Instance.CurrentEnemyCount++;
            StageManager.Instance.CheckFailCondition();
            yield return wait;
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
        }
    }
}

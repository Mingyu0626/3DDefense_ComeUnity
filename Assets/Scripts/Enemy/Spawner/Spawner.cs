using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private string spawnedEnemyName;
    [SerializeField]
    private int activateStageNumber;
    [SerializeField]
    private float spawnInterval;

    public int ActivateStageNumber
    {
        get { return activateStageNumber; }
    }

    private void Awake()
    {

    }
    private void OnEnable()
    {
        StartCoroutine(SpawnEnemy());
    }
    private void OnDisable()
    {
        StopCoroutine(SpawnEnemy());
    }
    private IEnumerator SpawnEnemy()
    {
        var wait = new WaitForSeconds(spawnInterval);
        yield return wait;
        while (true)
        {
            GameObject enemyGameObject = ObjectPoolManager.Instance.GetObject(spawnedEnemyName);
            if (enemyGameObject != null)
            {
                float randX = Random.Range(-5f, 5f);
                float randZ = Random.Range(-5f, 5f);
                enemyGameObject.transform.position = transform.position + new Vector3(randX, 0, randZ);
            }
            StageManager.Instance.OnEnemySpawned();
            yield return wait;
        }
    }
}

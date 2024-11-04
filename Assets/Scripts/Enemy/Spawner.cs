using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private string spawnedEnemyName;

    void Awake()
    {
        
    }

    void Start()
    {
        SpawnEnemy();
    }

    void Update()
    {
        
    }


    private void SpawnEnemy()
    {
        GameObject enemyGameObject = ObjectPoolManager.Instance.GetGameObject(spawnedEnemyName);
        if (enemyGameObject != null)
        {
            enemyGameObject.transform.position = transform.position;
            Debug.Log("Enemy Spawned!");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnedEnemy; // 해당 스포너에서 소환할 Enemy, 해당 스크립트가 들어있는 게임 오브젝트의 inspector 창에 적 GameObject를 드래그 & 드랍. 
    private float spawnIntervalTime = 3f; // 스폰 간격
    void Start()
    {
        StartCoroutine(SpawnEnemy2());
    }

    private void SpawnEnemy()
    {
        if (spawnedEnemy != null)
        {
            // Spawner의 위치에 소환
            Instantiate(spawnedEnemy, transform.position, transform.rotation);

            // 일정 범위 내 랜덤 위치에 소환
            // float randX = Random.Range(-5f, 5f);
            // float randZ = Random.Range(-5f, 5f);
            // Instantiate(spawnedEnemy, transform.position + new Vector3(randX, 0, randZ), transform.rotation);
        }
    }

    private IEnumerator SpawnEnemy2()
    {
        yield return new WaitForSeconds(spawnIntervalTime);
        while (true)
        {
            if (spawnedEnemy != null)
            {
                float randX = Random.Range(-5f, 5f);
                float randZ = Random.Range(-5f, 5f);
                Instantiate(spawnedEnemy, transform.position + new Vector3(randX, 0, randZ), transform.rotation);
                StageManager.Instance.CurrentEnemyCount++;
                yield return new WaitForSeconds(spawnIntervalTime);
            }
        }
    }
}

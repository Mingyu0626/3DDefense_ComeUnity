using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnerExercise : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnedEnemy; // �ش� �����ʿ��� ��ȯ�� Enemy, �ش� ��ũ��Ʈ�� ����ִ� ���� ������Ʈ�� inspector â�� �� GameObject�� �巡�� & ���. 
    private float spawnIntervalTime = 3f; // ���� ����
    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 5f, spawnIntervalTime);
    }

    private void SpawnEnemy()
    {
        if (spawnedEnemy != null)
        {
            // Spawner�� ��ġ�� ��ȯ
            Instantiate(spawnedEnemy, transform.position, transform.rotation);

            // ���� ���� �� ���� ��ġ�� ��ȯ
            float randX = Random.Range(-5f, 5f);
            float randZ = Random.Range(-5f, 5f);
            Instantiate(spawnedEnemy, transform.position + new Vector3(randX, 0, randZ), transform.rotation);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnerWeek5 : MonoBehaviour
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
            float randX = Random.Range(-5f, 5f);
            float randZ = Random.Range(-5f, 5f);
            // Instantiate(spawnedEnemy, transform.position + new Vector3(randX, 0, randZ), transform.rotation);

            Instantiate(spawnedEnemy, transform.position, transform.rotation);
        }
    }
}

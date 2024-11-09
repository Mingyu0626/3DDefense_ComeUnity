using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeek5 : MonoBehaviour
{
    protected int maxHp;
    private int hp;
    protected int damage;
    protected float speed;


    protected virtual void Update()
    {
        TracePlayer();
    }

    private void OnDestroy()
    {
        StageManager.Instance.CurrentKilledEnemyCount++;
        StageManager.Instance.CurrentEnemyCount--;
        UIManager.Instance.SetKilledEnemyCountTMP(StageManager.Instance.CurrentKilledEnemyCount);
        UIManager.Instance.SetCurrentEnemyCountTMP(StageManager.Instance.CurrentEnemyCount);
    }

    public void ApplyDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            Bullet playerBullet = other.GetComponent<Bullet>();
            ApplyDamage(playerBullet.GetDamage());
        }
    }

    private void TracePlayer()
    {
        Transform playerTransform = PlayerInfo.Instance.PlayerTransform;
        transform.LookAt(playerTransform);
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
    }
}

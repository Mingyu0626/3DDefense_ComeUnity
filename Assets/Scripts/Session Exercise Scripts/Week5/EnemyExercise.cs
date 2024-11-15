using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExercise : MonoBehaviour
{
    protected int maxHp;
    protected int hp;
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
        Transform playerTransform = Player.Instance.PlayerTransform; // 플레이어의 실시간 Transform를 가져온다.
        transform.LookAt(playerTransform); // Enemy가 플레이어의 위치를 바라보게 만든다.
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
        // 현재 위치(transform.position, 적 위치)에서, 목표 위치(playerTransform.position, 플레이어의 위치)로 (speed * Time.deltaTime)만큼의 속도로 이동한다.
    }
}

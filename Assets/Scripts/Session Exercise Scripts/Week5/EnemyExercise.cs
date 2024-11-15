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
        Transform playerTransform = Player.Instance.PlayerTransform; // �÷��̾��� �ǽð� Transform�� �����´�.
        transform.LookAt(playerTransform); // Enemy�� �÷��̾��� ��ġ�� �ٶ󺸰� �����.
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
        // ���� ��ġ(transform.position, �� ��ġ)����, ��ǥ ��ġ(playerTransform.position, �÷��̾��� ��ġ)�� (speed * Time.deltaTime)��ŭ�� �ӵ��� �̵��Ѵ�.
    }
}

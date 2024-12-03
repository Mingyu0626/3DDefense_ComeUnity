using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PoolAble
{
    protected int maxHp = 1;
    private int hp;
    protected int damage;
    protected float speed = 5f;
    void OnEnable()
    {
        hp = maxHp;
    }

    protected virtual void Update()
    {
        TracePlayer();
    }

    protected virtual void OnDisable()
    {
        // ���� ������ ���� ����ؼ� �����ɰŰ�����..
        // ���⺸�� �� Fit�� ó�� �κ��� ������ ����
        StageManager.Instance.CurrentKilledEnemyCount++;
        StageManager.Instance.CurrentEnemyCount--;
    }

    public void ApplyDamage(int damage) 
    {
        hp -= damage;
        if (hp <= 0)
        {
            ReleaseObject();
            StageManager.Instance.CheckClearCondition();
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
        Transform playerTransform = Player.Instance.PlayerTransform;
        transform.LookAt(playerTransform);
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
    }
}

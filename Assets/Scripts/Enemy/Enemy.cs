using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PoolAble
{
    protected int maxHp = 1;
    protected int hp;
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

    void OnDisable()
    {
        
    }

    public void SetHP(int val) { hp = val; }
    public int GetHP() { return hp; }
    public void ApplyDamage(int damage) 
    {
        hp -= damage;
        if (hp <= 0)
        {
            ReleaseObject();
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

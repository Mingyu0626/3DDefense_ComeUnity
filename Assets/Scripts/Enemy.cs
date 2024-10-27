using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PoolAble
{
    private int hp;
    void Start()
    {
        
    }

    void Update()
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
}

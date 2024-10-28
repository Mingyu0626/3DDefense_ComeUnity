using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolAble
{
    private int damage = 1;
    private float speed = 0.5f;
    private float durationTime = 3f;
    void Start()
    {
        Invoke("ReturnToPool", durationTime);
    }

    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }
    private void ReturnToPool()
    {
        ReleaseObject();
    }

    public int GetDamage() { return damage; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            return;
        }
        
        else if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.ApplyDamage(GetDamage());
        }
        else
        {
            ReturnToPool();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : PoolAble
{
    private int damage = 1;
    private float speed = 10f;
    private float durationTime = 3f;
    void OnEnable()
    {
        Invoke("ReturnToPool", durationTime);
    }

    void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }

    void OnDisable()
    {
        CancelInvoke("ReturnToPool");
    }

    private void ReturnToPool()
    {
        ReleaseObject();
    }

    public int GetDamage() { return damage; }

    private void OnTriggerEnter(Collider other)
    {
        ReturnToPool();
    }
}

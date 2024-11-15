using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletExercise : PoolAble
{
    private int damage = 1;
    private float speed = 10f;
    private float duration = 3f;
    void OnEnable()
    {
        Invoke(nameof(DestroyBullet), duration);
    }

    void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }

    void OnDestroy()
    {
        CancelInvoke(nameof(DestroyBullet));
    }
    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    public int GetDamage() { return damage; }

    private void OnTriggerEnter(Collider other)
    {
        DestroyBullet();
    }
}

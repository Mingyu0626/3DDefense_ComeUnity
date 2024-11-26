using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float speed = 10f;
    private float duration = 3f;
    public int Damage { get; private set; } = 1;
    void Start()
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

    private void OnTriggerEnter(Collider other)
    {
        DestroyBullet();
    }
}

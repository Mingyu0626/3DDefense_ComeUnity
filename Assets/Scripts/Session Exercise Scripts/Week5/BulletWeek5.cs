using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWeek5 : MonoBehaviour
{
    public int Damage { get; private set; }
    private float speed = 40f;
    private float duration = 3f;
    void Start()
    {
        Invoke("DestroyBullet", duration);
    }

    void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }

    void OnDestroy()
    {
        CancelInvoke("ReturnToPool");
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

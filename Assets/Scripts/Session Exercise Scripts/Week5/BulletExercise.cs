using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExercise : MonoBehaviour
{
    private float speed = 40f;
    private float duration = 3f;
    public int Damage { get; private set; }
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
        CancelInvoke("DestroyBullet");
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

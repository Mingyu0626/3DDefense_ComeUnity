using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    private int slimeMaxHp = 1;
    private int slimeDamage = 1;
    private float slimeSpeed = 5f;
    private float attackInterval = 3f;
    private float attackableDistance = 10f;

    [SerializeField]
    private GameObject slimeBullet;

    void Awake()
    {
        maxHp = slimeMaxHp;
        damage = slimeDamage;
        speed = slimeSpeed;
    }
    void Start()
    {
        InvokeRepeating(nameof(Attack), 2f, attackInterval);
    }
    protected override void Update()
    {
        base.Update();
    }

    void OnDisable()
    {

    }

    void Attack()
    {
        if (Vector3.Distance(Player.Instance.PlayerTransform.position, transform.position) < attackableDistance)
        {
            Instantiate(slimeBullet, transform.position + transform.forward * 2f, transform.rotation);
        }
    }

    //IEnumerator Attack()
    //{
    //    yield return null;
    //}
}

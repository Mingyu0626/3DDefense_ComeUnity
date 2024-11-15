using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeExcersie : EnemyExercise
{
    private int slimeMaxHp = 1;
    private int slimeDamage = 1;
    private float slimeSpeed = 5f;

    private float attackInterval = 3f;

    [SerializeField]
    private GameObject slimeBullet;
    private GameObject attackPoint;

    void Awake()
    {
        maxHp = slimeMaxHp;
        damage = slimeDamage;
        speed = slimeSpeed;
        attackPoint = transform.Find("AttackPoint").gameObject;
    }
    void Start()
    {
        InvokeRepeating(nameof(Attack), 2f, attackInterval);
    }

    protected override void Update()
    {
        base.Update();
    }

    void OnDestroy()
    {

    }

    void Attack()
    {
        Instantiate(slimeBullet, attackPoint.transform);
    }
}

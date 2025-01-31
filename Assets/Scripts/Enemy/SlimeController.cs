using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyControlState;

public class SlimeController : EnemyController
{
    private int slimeMaxHp = 1;
    private int slimeDamage = 1;
    private float slimeSpeed = 8f;
    private float slimeRotateionSpeed = 4f;

    void Awake()
    {
        MaxHP = slimeMaxHp;
        Damage = slimeDamage;
        Speed = slimeSpeed;
        RotationSpeed = slimeRotateionSpeed;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void Update()
    {
        base.Update();
    }
}

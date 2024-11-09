using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeWeek5 : EnemyWeek5
{
    private int slimeMaxHp = 1;
    private int slimeDamage = 1;
    private float slimeSpeed = 5f;
    void Awake()
    {
        maxHp = slimeMaxHp;
        damage = slimeDamage;
        speed = slimeSpeed;
    }

    protected override void Update()
    {
        base.Update();
    }
}

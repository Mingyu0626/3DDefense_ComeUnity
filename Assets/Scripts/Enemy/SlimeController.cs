using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyControlState;

public class SlimeController : EnemyController
{
    protected override void Awake()
    {
        base.Awake();
        enemyData = EnemyDataDictionary.GetEnemyData(EnemyName.Slime);
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

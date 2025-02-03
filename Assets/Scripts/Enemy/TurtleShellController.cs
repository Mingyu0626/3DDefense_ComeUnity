using EnemyControlState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleShellController : EnemyController
{
    protected override void Awake()
    {
        base.Awake();
        enemyData = EnemyDataDictionary.GetEnemyData(EnemyName.TurtleShell);
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

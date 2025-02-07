using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDataModel : MonoBehaviour
{
    public event Action<int> CurrentStageChanged;
    public event Action<int> AliveEnemyCountChanged;
    public event Action<int> KilledEnemyCountChanged;
    public event Action<int> KilledEnemyCountToClearChanged;
    public event Action<bool> IsWaitingNextStageChanged;

    private int numOfStages = 4;
    private int currentStage = 1;
    private int aliveEnemyCount = 0;
    private int aliveEnemyCountToFail = 50;
    private int killedEnemyCount = 0;
    private int killedEnemyCountToClear = 0;
    private int[] killedEnemyCountToClearArray;
    private bool isWaitingNextStage = false;

    public int NumOfStages
    {
        get => numOfStages;
        set { }
    }
    public int CurrentStage
    {
        get => currentStage;
        set
        {
            currentStage = value;
            CurrentStageChanged?.Invoke(value);
        }
    }
    public int AliveEnemyCount
    {
        get => aliveEnemyCount;
        set
        {
            aliveEnemyCount = value;
            AliveEnemyCountChanged?.Invoke(value);
        }
    }
    public int AliveEnemyCountToFail
    {
        get => aliveEnemyCountToFail;
        set { }
    }
    public int KilledEnemyCount
    {
        get => killedEnemyCount;
        set
        {
            killedEnemyCount = value;
            KilledEnemyCountChanged?.Invoke(value);
        }
    }
    public int KilledEnemyCountToClear
    {
        get => killedEnemyCountToClear;
        set 
        {
            killedEnemyCountToClear = value;
            KilledEnemyCountToClearChanged?.Invoke(value);
        }
    }
    public int[] KilledEnemyCountToClearArray
    {
        get => killedEnemyCountToClearArray;
        set { }
    }
    public bool IsWaitingNextStage
    {
        get => isWaitingNextStage;
        set
        {
            isWaitingNextStage = value;
            IsWaitingNextStageChanged?.Invoke(value);
        }
    }

    private void Awake()
    {
        killedEnemyCountToClearArray = new int[numOfStages + 1];
        for (int i = 1; i <= numOfStages; i++)
        {
            killedEnemyCountToClearArray[i] = i * 2;
        }
    }
}

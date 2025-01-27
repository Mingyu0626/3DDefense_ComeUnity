using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    private int numOfStages = 4; // 전체 스테이지 수 
    private int currentStage = 1; // 현재 스테이지  
 
    private int currentEnemyCount; // 현재 소환되어 있는 적의 수
    public int CurrentEnemyCount
    {
        get
        {
            return currentEnemyCount;
        }
        set
        {
            currentEnemyCount = value;
            UIGameScene.Instance.SetCurrentEnemyCountTMP(currentEnemyCount);
        }
    }
    private int currentKilledEnemyCount; // 현재 스테이지에서의 적 처치수
    public int CurrentKilledEnemyCount
    {
        get
        {
            return currentKilledEnemyCount;
        }
        set
        {
            currentKilledEnemyCount = value;
            UIGameScene.Instance.SetKilledEnemyCountTMP(currentKilledEnemyCount);
        }
    }
    private int[] goalEnemyCount; // 스테이지 별 목표 적 처치수를 저장하는 배열 
    private int enemyCountToFail = 50; // 게임 오버가 되는 최소 적의 수
    private float delayBeforeNextStage = 3f; // 스테이지 클리어 후 다음 스테이지 시작 전까지의 딜레이
    private bool waitingNextStage;     // 스테이지 클리어 후 다음 스테이지를 기다리는 중인지 여부를 나타내는 변수
    public bool WaitingNextStage 
    { 
        get
        {
            return waitingNextStage;
        }
        private set
        {
            waitingNextStage = value;
            SetActiveAllSpawners(!value);
            UIGameScene.Instance.ClearStageAnimation(value);
            if (InputManager.Instance.IsPlayerActionEnabled())
            {
                InputManager.Instance.SetPlayerActionState(false);
            }
            else
            {
                InputManager.Instance.SetPlayerActionState(true);
            }
        }
    } 

    [SerializeField] private List<Spawner> stageSpawnerList = new List<Spawner>();

    protected override void Awake()
    {
    }
    private void Start()
    {
        // goalEnemyCount 배열 전체 스테이지 수만큼 동적 할당
        // 스테이지 별 목표 적 처치 수를 설정(원하는 값으로 커스터마이즈)
        // 예시) 1스테이지는 20마리, 2스테이지는 50마리, 3스테이지는 100마리 ...
        goalEnemyCount = new int[numOfStages + 1];
        for (int i = 1; i <= numOfStages; i++)
        {
            goalEnemyCount[i] = i;
        }
        InitCountAndGoalEnemyCountUI();
    }
    private void InitCountAndGoalEnemyCountUI()
    {
        CurrentEnemyCount = 0;
        CurrentKilledEnemyCount = 0;
        UIGameScene.Instance.SetGoalEnemyCountTMP(goalEnemyCount[currentStage]);
    }
    public void CheckClearCondition()
    {
        if (goalEnemyCount[currentStage] <= CurrentKilledEnemyCount)
        {
            StartCoroutine(ClearStage());
        }
    }
    public void CheckFailCondition()
    {
        if (enemyCountToFail <= CurrentEnemyCount)
        {
            FailStage();
        }
    }
    private IEnumerator ClearStage()
    {
        if (currentStage == numOfStages)
        {
            GameManager.Instance.EndGame(true);
            yield break;
        }
        else
        {
            currentStage++;
            ObjectPoolManager.Instance.ReturnAllActiveObjectsToPool();
            WaitingNextStage = true;
            yield return new WaitForSeconds(delayBeforeNextStage);
            WaitingNextStage = false;
            InitCountAndGoalEnemyCountUI();
        }
        yield break;
    }
    private void FailStage()
    {
        GameManager.Instance.EndGame(false);
    }

    private void SetActiveAllSpawners(bool val)
    {
        for (int i = 0; i < stageSpawnerList.Count; i++)
        {
            stageSpawnerList[i].gameObject.SetActive(val);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; } // 외부에서 StageManager에 접근하기 위한 인스턴스
    private int[] goalEnemyCount; // 스테이지 별 목표 적 처치수를 저장하는 배열 
    private int numOfStages = 4; // 전체 스테이지 수 
    private int currentStage = 1; // 현재 스테이지  
    private float delayBeforeNextStage = 3f; // 스테이지 클리어 후 다음 스테이지 시작 전까지의 딜레이
    private int enemyCountToFail = 50; // 게임 오버가 되는 최소 적의 수
    private int currentEnemyCount;
    private int currentKilledEnemyCount;
    public int CurrentEnemyCount // 현재 소환되어 있는 적의 수
    {
        get
        {
            return currentEnemyCount;
        }
        set
        {
            currentEnemyCount = value;
            UIManager.Instance.SetCurrentEnemyCountTMP(currentEnemyCount);
        }
    }
    public int CurrentKilledEnemyCount  // 현재 스테이지에서의 적 처치수
    {
        get
        {
            return currentKilledEnemyCount;
        }
        set
        {
            currentKilledEnemyCount = value;
            UIManager.Instance.SetKilledEnemyCountTMP(currentKilledEnemyCount);
        }
    }


    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


    void Start()
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

    void InitCountAndGoalEnemyCountUI()
    {
        CurrentEnemyCount = 0;
        CurrentKilledEnemyCount = 0;
        UIManager.Instance.SetGoalEnemyCountTMP(CurrentEnemyCount);
    }


    IEnumerator ClearStage()
    {
        if (currentStage == numOfStages)
        {
            GameManager.Instance.EndGame(true);
            yield break;
        }
        else
        {
            // 스테이지 클리어 후 대기중 Pause탭을 켜도,
            // 코루틴이 안멈춰서 퍼즈탭이 남아있는 상태에서 다음 스테이지가 재개되는 문제
            currentStage++;
            ObjectPoolManager.Instance.ReturnAllActiveObjectsToPool();
            GameManager.Instance.PauseGame();
            UIManager.Instance.SetActiveClearStageTextGO(true);
            yield return new WaitForSecondsRealtime(delayBeforeNextStage);
            UIManager.Instance.SetActiveClearStageTextGO(false);
            GameManager.Instance.ResumeGame();
            InitCountAndGoalEnemyCountUI();
        }
        yield break;
    }

    void FailStage()
    {
        // GameManager의 EndGame을 호출
        GameManager.Instance.EndGame(false);
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
}

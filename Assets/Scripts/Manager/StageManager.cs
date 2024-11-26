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
    private int enemyCountToFail = 5; // 게임 오버가 되는 최소 적의 수
    public int CurrentEnemyCount { get; set; } = 0; // 현재 소환되어 있는 적의 수
    public int CurrentKilledEnemyCount { get; set; } = 0; // 현재 스테이지에서의 적 처치수
    public bool IsCleared { get; private set; } = false;


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
            goalEnemyCount[i] = i * 1;
        }
        InitCount();
    }

    void Update()
    {
    }

    void InitCount()
    {
        CurrentEnemyCount = 0;
        CurrentKilledEnemyCount = 0;
    }

    void FailStage()
    {
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

    IEnumerator ClearStage()
    {
        IsCleared = true;
        if (currentStage == numOfStages)
        {
            GameManager.Instance.EndGame(true);
            yield break;
        }
        else
        {
            currentStage++;
            InitCount();
            Time.timeScale = 0f;
            yield return new WaitForSecondsRealtime(delayBeforeNextStage);
            IsCleared = false;
            Time.timeScale = 1f;
        }
        yield break;
    }
}

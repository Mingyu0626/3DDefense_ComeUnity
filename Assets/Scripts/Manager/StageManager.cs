using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; } // 외부에서 StageManager에 접근하기 위한 인스턴스
    private int[] goalEnemyCount; // 스테이지 별 목표 적 처치수를 저장하는 배열 
    private int numOfStages = 4; // 전체 스테이지 수 
    private int currentStage = 1; // 현재 스테이지  
    private float delayBeforeNextStage = 3f;
    public int CurrentEnemyCount { get; set; } = 0; // 현재 소환되어 있는 적의 수
    public int CurrentKilledEnemyCount { get; set; } = 0; // 현재 스테이지에서의 적 처치수


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
        UIManager.Instance.SetGoalEnemyCountTMP(goalEnemyCount[currentStage]);
        Init();
    }

    void Update()
    {
        // 현재 살아있는 몬스터 수가 50마리를 넘으면, FailStage 호출
        if (50 < CurrentEnemyCount)
        {
            FailStage();
        }
    }

    void Init()
    {
        CurrentEnemyCount = 0;
        CurrentKilledEnemyCount = 0;
        UIManager.Instance.SetCurrentEnemyCountTMP(CurrentEnemyCount);
        UIManager.Instance.SetKilledEnemyCountTMP(CurrentKilledEnemyCount);
        UIManager.Instance.SetGoalEnemyCountTMP(goalEnemyCount[currentStage]);
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
            currentStage++;
            ObjectPoolManager.Instance.ReturnAllActiveObjectsToPool();
            Init();
            // 여기서 일시정지를 하지말고 UI를 띄워주자.
            // 일시정지를 할꺼면 PlayerInputAction도 일시적으로 비활성화 시켜줘야 한다.
            Time.timeScale = 0f;
            yield return new WaitForSecondsRealtime(delayBeforeNextStage);
            Time.timeScale = 1f;
        }
        Debug.Log("현재 스테이지 : " + currentStage);
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
}

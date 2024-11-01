using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; } // 외부에서 StageManager에 접근하기 위한 인스턴스
    private int[] goalEnemyCount; // 스테이지 별 목표 적 처치수를 저장하는 배열 
    private int numOfStages = 4; // 전체 스테이지 수 
    private int currentStage = 1; // 현재 스테이지  
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
        for (int i = 0; i < numOfStages; i++)
        {
            goalEnemyCount[i] = (i + 1) * 20;
        }
    }

    void Update()
    {
        // 현재 스테이지의 목표 처치수와 현재 적 처치수를 비교
        // 목표에 도달하면, ClearStage 호출
        // 추후 Coroutine 학습후 효율적인 로직으로 개선 예정
        if (goalEnemyCount[currentStage] <= CurrentKilledEnemyCount)
        {
            ClearStage();
        }
    }

    void ClearStage()
    {
        // 현재 스테이지가 마지막 스테이지라면,
        // GameManager의 isWin 변수를 true로 변경
        // GameManager의 EndGame을 호출
        if (currentStage == numOfStages)
        {
            GameManager.Instance.EndGame(true);
        }

        currentStage++;
    }

    void FailStage()
    {
        // GameManager의 EndGame을 호출
        GameManager.Instance.EndGame(false);
    }
}

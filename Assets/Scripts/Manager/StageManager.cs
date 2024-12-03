using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; } // �ܺο��� StageManager�� �����ϱ� ���� �ν��Ͻ�
    private int[] goalEnemyCount; // �������� �� ��ǥ �� óġ���� �����ϴ� �迭 
    private int numOfStages = 4; // ��ü �������� �� 
    private int currentStage = 1; // ���� ��������  
    private float delayBeforeNextStage = 3f; // �������� Ŭ���� �� ���� �������� ���� �������� ������
    private int enemyCountToFail = 5; // ���� ������ �Ǵ� �ּ� ���� ��
    private int currentEnemyCount = 0;
    private int currentKilledEnemyCount = 0;
    public int CurrentEnemyCount 
    {
        get { return currentEnemyCount; }
        set
        {
            currentEnemyCount = value;
            UIManager.Instance.SetCurrentEnemyCountTMP(currentEnemyCount);
        }
    } // ���� ��ȯ�Ǿ� �ִ� ���� ��
    public int CurrentKilledEnemyCount 
    {
        get { return currentKilledEnemyCount; }
        set
        {
            currentKilledEnemyCount = value;
            UIManager.Instance.SetKilledEnemyCountTMP(currentKilledEnemyCount);
        }
    } // ���� �������������� �� óġ��
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
        // goalEnemyCount �迭 ��ü �������� ����ŭ ���� �Ҵ�
        // �������� �� ��ǥ �� óġ ���� ����(���ϴ� ������ Ŀ���͸�����)
        // ����) 1���������� 20����, 2���������� 50����, 3���������� 100���� ...
        goalEnemyCount = new int[numOfStages + 1];
        for (int i = 1; i <= numOfStages; i++)
        {
            goalEnemyCount[i] = i * 3;
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
        UIManager.Instance.SetGoalEnemyCountTMP(goalEnemyCount[currentStage]);
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
            Time.timeScale = 0f;
            yield return new WaitForSecondsRealtime(delayBeforeNextStage);
            IsCleared = false;
            InitCount();
            Time.timeScale = 1f;
        }
        yield break;
    }
}

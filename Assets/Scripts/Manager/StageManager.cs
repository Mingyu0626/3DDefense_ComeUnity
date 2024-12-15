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
    private int enemyCountToFail = 50; // ���� ������ �Ǵ� �ּ� ���� ��
    private int currentEnemyCount;
    private int currentKilledEnemyCount;
    public int CurrentEnemyCount // ���� ��ȯ�Ǿ� �ִ� ���� ��
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
    public int CurrentKilledEnemyCount  // ���� �������������� �� óġ��
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
        // goalEnemyCount �迭 ��ü �������� ����ŭ ���� �Ҵ�
        // �������� �� ��ǥ �� óġ ���� ����(���ϴ� ������ Ŀ���͸�����)
        // ����) 1���������� 20����, 2���������� 50����, 3���������� 100���� ...
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
            // �������� Ŭ���� �� ����� Pause���� �ѵ�,
            // �ڷ�ƾ�� �ȸ��缭 �������� �����ִ� ���¿��� ���� ���������� �簳�Ǵ� ����
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
        // GameManager�� EndGame�� ȣ��
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; } // �ܺο��� StageManager�� �����ϱ� ���� �ν��Ͻ�
    private int[] goalEnemyCount; // �������� �� ��ǥ �� óġ���� �����ϴ� �迭 
    private int numOfStages = 4; // ��ü �������� �� 
    private int currentStage = 1; // ���� ��������  
    private float delayBeforeNextStage = 3f;
    public int CurrentEnemyCount { get; set; } = 0; // ���� ��ȯ�Ǿ� �ִ� ���� ��
    public int CurrentKilledEnemyCount { get; set; } = 0; // ���� �������������� �� óġ��


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
        UIManager.Instance.SetGoalEnemyCountTMP(goalEnemyCount[currentStage]);
        Init();
    }

    void Update()
    {
        // ���� ����ִ� ���� ���� 50������ ������, FailStage ȣ��
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
            // ���⼭ �Ͻ������� �������� UI�� �������.
            // �Ͻ������� �Ҳ��� PlayerInputAction�� �Ͻ������� ��Ȱ��ȭ ������� �Ѵ�.
            Time.timeScale = 0f;
            yield return new WaitForSecondsRealtime(delayBeforeNextStage);
            Time.timeScale = 1f;
        }
        Debug.Log("���� �������� : " + currentStage);
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
}

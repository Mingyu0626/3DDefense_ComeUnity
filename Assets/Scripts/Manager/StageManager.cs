using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    private int numOfStages = 4; // ��ü �������� �� 
    private int currentStage = 1; // ���� ��������  
 
    private int currentEnemyCount; // ���� ��ȯ�Ǿ� �ִ� ���� ��
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
    private int currentKilledEnemyCount; // ���� �������������� �� óġ��
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
    private int[] goalEnemyCount; // �������� �� ��ǥ �� óġ���� �����ϴ� �迭 
    private int enemyCountToFail = 50; // ���� ������ �Ǵ� �ּ� ���� ��
    private float delayBeforeNextStage = 3f; // �������� Ŭ���� �� ���� �������� ���� �������� ������
    private bool waitingNextStage;     // �������� Ŭ���� �� ���� ���������� ��ٸ��� ������ ���θ� ��Ÿ���� ����
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

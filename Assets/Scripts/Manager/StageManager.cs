using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; } // �ܺο��� StageManager�� �����ϱ� ���� �ν��Ͻ�
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
            UIManager.Instance.SetCurrentEnemyCountTMP(currentEnemyCount);
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
            UIManager.Instance.SetKilledEnemyCountTMP(currentKilledEnemyCount);
        }
    }
    private int[] goalEnemyCount; // �������� �� ��ǥ �� óġ���� �����ϴ� �迭 
    private int enemyCountToFail = 50; // ���� ������ �Ǵ� �ּ� ���� ��
    private float delayBeforeNextStage = 3f; // �������� Ŭ���� �� ���� �������� ���� �������� ������
    private bool waitingNextStage;
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
            UIManager.Instance.SetActiveClearStageTextGO(value);
            if (GameManager.Instance.Action.Player.enabled)
            {
                GameManager.Instance.Action.Player.Disable();
            }
            else
            {
                GameManager.Instance.Action.Player.Enable();
            }
        }
    } 
    // �������� Ŭ���� �� ���� ���������� ��ٸ��� ������ ���θ� ��Ÿ���� ����
    [SerializeField] private List<Spawner> stageSpawnerList = new List<Spawner>();

    private void Awake()
    {   
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
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
        UIManager.Instance.SetGoalEnemyCountTMP(CurrentEnemyCount);
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
        // GameManager�� EndGame�� ȣ��
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    [SerializeField] private StageDataModel stageDataModel;

    private float delayBeforeNextStage = 3f; // �������� Ŭ���� �� ���� �������� ���� �������� ������

    protected override void Awake()
    {
        InitStageDataOnStageChanged(stageDataModel.CurrentStage);
    }
    private void Start()
    {
        SetActiveSpawnersOnStage(true);
    }

    private void InitStageDataOnStageChanged(int currentStage)
    {
        stageDataModel.AliveEnemyCount = 0;
        stageDataModel.KilledEnemyCount = 0;
        stageDataModel.KilledEnemyCountToClear = stageDataModel.KilledEnemyCountToClearArray[currentStage];
    }

    public void OnEnemySpawned()
    {
        stageDataModel.AliveEnemyCount++;
        if (CheckStageFailCondition())
        {
            StageFail();
        }
    }
    public void OnEnemyKilled()
    {
        stageDataModel.KilledEnemyCount++;
        stageDataModel.AliveEnemyCount--;
        if (CheckStageClearCondition())
        {
            StartCoroutine(StageClear());
        }
    }
    public bool CheckStageClearCondition()
    {
        return stageDataModel.KilledEnemyCountToClear <= stageDataModel.KilledEnemyCount;
    }
    public bool CheckStageFailCondition()
    {
        return stageDataModel.AliveEnemyCountToFail <= stageDataModel.AliveEnemyCount;
    }
    private IEnumerator StageClear()
    {
        if (stageDataModel.CurrentStage == stageDataModel.NumOfStages)
        {
            GameManager.Instance.EndGame(true);
            yield break;
        }
        else
        {
            stageDataModel.CurrentStage++;
            SetActiveSpawnersOnStage(false);
            ObjectPoolManager.Instance.ReturnAllActiveObjectsToPool();
            InitStageDataOnStageChanged(stageDataModel.CurrentStage);
            stageDataModel.IsWaitingNextStage = true; 
            yield return new WaitForSeconds(delayBeforeNextStage);
            stageDataModel.IsWaitingNextStage = false;
            SetActiveSpawnersOnStage(true);
        }
        yield break;
    }
    private void StageFail()
    {
        GameManager.Instance.EndGame(false);
    }

    // ��� �Űܾߵɵ�?
    [SerializeField] private List<Spawner> stageSpawnerList = new List<Spawner>();
    private void SetActiveSpawnersOnStage(bool val = true)
    {
        for (int i = 0; i < stageSpawnerList.Count; i++)
        {
            if (stageSpawnerList[i].ActivateStageNumber <= stageDataModel.CurrentStage)
            {
                stageSpawnerList[i].gameObject.SetActive(val);
            }
        }
    }
}

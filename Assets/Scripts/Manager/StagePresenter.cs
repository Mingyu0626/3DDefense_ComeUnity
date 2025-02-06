using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagePresenter : Singleton<StagePresenter>
{
    [SerializeField] private StageDataModel stageDataModel;
    [SerializeField] private UIStageView stageView;


    private float delayBeforeNextStage = 3f; // 스테이지 클리어 후 다음 스테이지 시작 전까지의 딜레이

    private void Start()
    {
        stageDataModel.CurrentStageChanged += stageView.SetTextCurrentStage;
        stageDataModel.CurrentStageChanged += InitStageDataOnStageChanged;
        stageDataModel.AliveEnemyCountChanged += stageView.SetTextAliveEnemyCount;
        stageDataModel.KilledEnemyCountChanged += stageView.SetTextKilledEnemyCount;
        stageDataModel.KilledEnemyCountToClearChanged += stageView.SetTextGoalEnemyCount;
        stageDataModel.IsWaitingNextStageChanged += stageView.StageClear;

        ActivateSpawners();
        InitStageDataOnStageChanged(stageDataModel.CurrentStage);
    }

    private void InitStageDataOnStageChanged(int currentStage)
    {
        stageDataModel.AliveEnemyCount = 0;
        stageDataModel.KilledEnemyCount = 0;
        stageDataModel.KilledEnemyCountToClear = stageDataModel.KilledEnemyCountToClearArray[currentStage];
        stageView.SetTextCurrentStage(currentStage);
    }
    public void OnEnemySpawned()
    {
        stageDataModel.AliveEnemyCount++;
        CheckStageFailCondition();
    }
    public void OnEnemyKilled()
    {
        stageDataModel.KilledEnemyCount++;
        stageDataModel.AliveEnemyCount--;
        CheckStageClearCondition();
    }
    public void CheckStageClearCondition()
    {
        if (stageDataModel.KilledEnemyCountToClear <= stageDataModel.KilledEnemyCount)
        {
            StartCoroutine(StageClear());
        }
    }
    public void CheckStageFailCondition()
    {
        if (stageDataModel.AliveEnemyCountToFail <= stageDataModel.AliveEnemyCount)
        {
            StageFail();
        }
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
            StartCoroutine(stageView.CountDown());
            stageDataModel.CurrentStage++;
            ObjectPoolManager.Instance.ReturnAllActiveObjectsToPool();
            stageDataModel.IsWaitingNextStage = true;
            yield return new WaitForSeconds(delayBeforeNextStage);
            stageDataModel.IsWaitingNextStage = false;
        }
        yield break;
    }
    private void StageFail()
    {
        GameManager.Instance.EndGame(false);
    }


    // 얘는 옮겨야될듯?
    [SerializeField] private List<Spawner> stageSpawnerList = new List<Spawner>();
    private void ActivateSpawners(bool val = true)
    {
        for (int i = 0; i < stageSpawnerList.Count; i++)
        {
            if (stageSpawnerList[i].ActivateStageNum <= stageDataModel.CurrentStage)
            {
                stageSpawnerList[i].gameObject.SetActive(val);
            }
        }
    }
}

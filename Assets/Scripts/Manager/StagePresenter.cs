using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagePresenter : MonoBehaviour
{
    [SerializeField] private StageDataModel stageDataModel;
    [SerializeField] private UIStageView stageView;

    private void Start()
    {
        stageDataModel.CurrentStageChanged += stageView.SetTextCurrentStage;
        stageDataModel.AliveEnemyCountChanged += stageView.SetTextAliveEnemyCount;
        stageDataModel.KilledEnemyCountChanged += stageView.SetTextKilledEnemyCount;
        stageDataModel.KilledEnemyCountToClearChanged += stageView.SetTextGoalEnemyCount;
        stageDataModel.IsWaitingNextStageChanged += stageView.StageClear;
    }
}

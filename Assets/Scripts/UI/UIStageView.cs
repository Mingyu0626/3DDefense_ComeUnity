using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIStageView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentStageTMP;
    [SerializeField] private TextMeshProUGUI aliveEnemyCountTMP;
    [SerializeField] private TextMeshProUGUI killedEnemyCountTMP;
    [SerializeField] private TextMeshProUGUI goalEnemyCountTMP;

    [SerializeField] private RectTransform clearStageRT;
    [SerializeField] private RectTransform getReadyForTheNextStageRT;
    [SerializeField] private GameObject countDownGO;

    public void SetTextCurrentStage(int stageNumber)
    {
        if (currentStageTMP != null)
        {
            currentStageTMP.SetText(stageNumber.ToString());
        }
    }
    public void SetTextAliveEnemyCount(int count)
    {
        if (aliveEnemyCountTMP != null)
        {
            aliveEnemyCountTMP.SetText(count.ToString());
        }
    }
    public void SetTextKilledEnemyCount(int count)
    {
        if (killedEnemyCountTMP != null)
        {
            killedEnemyCountTMP.SetText(count.ToString());
        }
    }
    public void SetTextGoalEnemyCount(int count)
    {
        if (goalEnemyCountTMP != null)
        { 
            goalEnemyCountTMP.SetText(count.ToString());
        }
    }

    // ¿ä ¹Ø¿¡ ¾Öµéµµ ÄÆ..?

    public void StageClear(bool isWaiting)
    {
        if (clearStageRT != null && getReadyForTheNextStageRT != null)
        {
            if (isWaiting)
            {
                UIManager.Instance.AnimationManager.SlideIn(ref clearStageRT, ref getReadyForTheNextStageRT, 0.3f, 1600f);
            }
            else
            {
                UIManager.Instance.AnimationManager.SlideOut(ref clearStageRT, ref getReadyForTheNextStageRT, 0.3f, 1600f);
            }
        }
    }

    public IEnumerator CountDown()
    {
        if (countDownGO != null)
        {
            countDownGO.gameObject.SetActive(true);
            TextMeshProUGUI countDownTMP = countDownGO.GetComponent<TextMeshProUGUI>();
            countDownTMP.SetText(3.ToString());
            UIManager.Instance.AnimationManager.FadeIn(ref countDownTMP, 0.5f);

            int countInt = 3;
            while (countInt != 0)
            {
                countDownTMP.SetText(countInt.ToString());
                yield return new WaitForSeconds(1f);
                countInt--;
            }
            countDownTMP.SetText("Go!");
            yield return new WaitForSecondsRealtime(1f);
            UIManager.Instance.AnimationManager.FadeOut(ref countDownTMP, 1f,
                () => countDownGO.gameObject.SetActive(false));

        }
        yield return null;
    }
}

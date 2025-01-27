using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGameScene : MonoBehaviour
{
    public static UIGameScene Instance { get; private set; }
    [SerializeField]
    private TextMeshProUGUI currentEnemyCountTMP;
    [SerializeField]
    private TextMeshProUGUI killedEnemyCountTMP;
    [SerializeField]
    private TextMeshProUGUI goalEnemyCountTMP;
    [SerializeField]
    private Slider playerHpBar;
    [SerializeField]
    private Slider basementHpBar;
    [SerializeField]
    private GameObject clearStageTextGO;
    [SerializeField]
    private RectTransform clearStageRT;
    [SerializeField]
    private RectTransform getReadyForTheNextStageRT;
    [SerializeField]
    private GameObject countDownGO;

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
        playerHpBar.maxValue = Player.Instance.MaxHP;
        SetPlayerHPSlider(Convert.ToInt32(playerHpBar.maxValue));
        basementHpBar.maxValue = Basement.Instance.MaxHP;
        SetBasementHPSlider(Convert.ToInt32(basementHpBar.maxValue));
    }
    public void SetCurrentEnemyCountTMP(int val)
    {
        if (currentEnemyCountTMP != null)
        {
            currentEnemyCountTMP.SetText(val.ToString());
        }
    }
    public void SetKilledEnemyCountTMP(int val)
    {
        if (killedEnemyCountTMP != null)
        {
            killedEnemyCountTMP.SetText(val.ToString());
        }
    }
    public void SetGoalEnemyCountTMP(int val)
    {
        if (goalEnemyCountTMP != null)
        { 
        goalEnemyCountTMP.SetText(val.ToString());
        }
    }
    public void SetPlayerHPSlider(int val)
    {
        if (playerHpBar != null)
        {
            playerHpBar.value = val;
        }
    }
    public void SetBasementHPSlider(int val)
    {
        if(basementHpBar != null)
        {
        basementHpBar.value = val;
        }
    }
    public void ClearStageAnimation(bool startWaiting)
    {
        if (clearStageRT == null || getReadyForTheNextStageRT == null)
        {
            Debug.Log("애니메이션이 동작할 객체가 Null");
        }

        if (startWaiting)
        {
            UIManager.Instance.AnimationManager.AnimationSlideIn(ref clearStageRT, ref getReadyForTheNextStageRT, 0.3f);
        }
        else
        {
            UIManager.Instance.AnimationManager.AnimationSlideOut(ref clearStageRT, ref getReadyForTheNextStageRT, 0.5f);
        }
    }
    private IEnumerator CountDown()
    {
        if (countDownGO != null)
        {
            countDownGO.gameObject.SetActive(true);
            TextMeshProUGUI countDownTMP = countDownGO.GetComponent<TextMeshProUGUI>();
            UIManager.Instance.AnimationManager.AnimationFadeIn(ref countDownTMP, 0.1f);

            int countInt = 3;
            while (countInt != 0)
            {
                countDownTMP.SetText(countInt.ToString());
                yield return new WaitForSecondsRealtime(1f);
                countInt--;
            }
            countDownTMP.SetText("Go!!");
            // Slide 또는 Fade를 통해 안보이게하기
            // Slide의 경우 오리지널 포지션 임시객체에 저장해둘 것

        }
        yield return null;
    }
}

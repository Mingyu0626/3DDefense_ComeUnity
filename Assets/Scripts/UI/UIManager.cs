using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField]
    private TextMeshProUGUI currentEnemyCountTMP;
    [SerializeField]
    private TextMeshProUGUI killedEnemyCountTMP;
    [SerializeField]
    private TextMeshProUGUI goalEnemyCountTMP;
    [SerializeField]
    private Slider hpBar;

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
        hpBar.maxValue = PlayerInfo.Instance.MaxHP;
        SetPlayerHPSlider(Convert.ToInt32(hpBar.maxValue));
    }

    public void SetCurrentEnemyCountTMP(int val)
    {
        currentEnemyCountTMP.SetText(val.ToString());
    }

    public void SetKilledEnemyCountTMP(int val)
    {
        killedEnemyCountTMP.SetText(val.ToString());
    }

    public void SetGoalEnemyCountTMP(int val)
    {
        goalEnemyCountTMP.SetText(val.ToString());
    }

    public void SetPlayerHPSlider(int val)
    {
        hpBar.value = val;
    }
}

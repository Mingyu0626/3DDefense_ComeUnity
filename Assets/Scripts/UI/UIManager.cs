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
    private Slider playerHpBar;
    [SerializeField]
    private Slider basementHpBar;

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
}

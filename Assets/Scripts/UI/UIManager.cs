using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField]
    private TextMeshProUGUI currentEnemyCountTMP;
    [SerializeField]
    private TextMeshProUGUI killedEnemyCountTMP;
    [SerializeField]
    private TextMeshProUGUI goalEnemyCountTMP;

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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private int[] GoalEnemies = new int[] { 20, 30, 50, 100 };
    private int MaxStage = 4;
    int currentNumOfEnemies;
    void Start()
    {
        
    }

    void Update()
    {
        
    }



    void ClearStage()
    {
        GameManager.Instance.stageNum++;
        if (MaxStage == GameManager.Instance.stageNum)
        {
            GameManager.Instance.EndGame(true);
        }
    }

    void FailStage()
    {
        GameManager.Instance.EndGame(false);
    }


}

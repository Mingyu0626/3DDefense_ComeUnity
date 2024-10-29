using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private int[] goalEnemies = new int[] { 20, 30, 50, 100 };
    private int numOfStages = 3;
    private int currentStageLevel = 1;
    private int currentNumOfEnemies;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }



    void ClearStage()
    {
        currentStageLevel++;
        if (currentStageLevel == numOfStages)
        {
            GameManager.instance.EndGame(true);
        }
    }

    void FailStage()
    {
        GameManager.instance.EndGame(false);
    }
}

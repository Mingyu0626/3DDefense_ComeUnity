using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private int[] goalEnemies = new int[] { 20, 30, 50, 100 };
    private int numOfStages = 3;
    private int currentStage = 1;
    private int currentNumOfEnemies;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }



    void ClearStage()
    {
        currentStage++;
        if (currentStage == numOfStages)
        {
            GameManager.Instance.EndGame(true);
        }
    }

    void FailStage()
    {
        GameManager.Instance.EndGame(false);
    }

    int getCurrentNumOfEnemies() { return currentNumOfEnemies; }
    void setCurrentNumOfEnemies(int val) { currentNumOfEnemies = val; }


}

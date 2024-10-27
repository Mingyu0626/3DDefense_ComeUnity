using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private int[] goalEnemies = new int[] { 20, 30, 50, 100 };
    private int maxStage = 4;
    private int currentNumOfEnemies;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }



    void ClearStage()
    {
        GameManager.Instance.stageNum++;
        if (maxStage == GameManager.Instance.stageNum)
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

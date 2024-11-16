using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManagerExercise : MonoBehaviour
{
    public static StageManagerExercise Instance { get; private set; } // �ܺο��� StageManager�� �����ϱ� ���� �ν��Ͻ�
    private int[] goalEnemyCount; // �������� �� ��ǥ �� óġ���� �����ϴ� �迭 
    private int numOfStages = 4; // ��ü �������� �� 
    private int currentStage = 1; // ���� ��������  
    public int CurrentEnemyCount { get; set; } = 0; // ���� ��ȯ�Ǿ� �ִ� ���� ��
    public int CurrentKilledEnemyCount { get; set; } = 0; // ���� �������������� �� óġ��


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
        // goalEnemyCount �迭 ��ü �������� ����ŭ ���� �Ҵ�
        // �������� �� ��ǥ �� óġ ���� ����(���ϴ� ������ Ŀ���͸�����)
        // ����) 1���������� 20����, 2���������� 50����, 3���������� 100���� ...
        goalEnemyCount = new int[numOfStages + 1];
        for (int i = 1; i <= numOfStages; i++)
        {
            goalEnemyCount[i] = (i + 1) * 20;
        }
    }

    void Update()
    {
        if (goalEnemyCount[currentStage] <= CurrentKilledEnemyCount)
        {
            ClearStage();
        }
    }

    void ClearStage()
    {
        // ���� ���������� ������ �����������,
        // GameManager�� isWin ������ true�� ����
        // GameManager�� EndGame�� ȣ��
        Debug.Log("Clearstage() ȣ��");
        if (currentStage == numOfStages)
        {
            Debug.Log("���� Ŭ����");
            GameManager.Instance.EndGame(true);
        }
        currentStage++;
        CurrentEnemyCount = 0;
        CurrentKilledEnemyCount = 0;
    }

    void FailStage()
    {
        // GameManager�� EndGame�� ȣ��
        GameManager.Instance.EndGame(false);
    }
}

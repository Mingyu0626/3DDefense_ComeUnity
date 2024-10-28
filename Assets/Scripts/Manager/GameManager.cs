using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int numOfStages { get; set; } = 0;
    public bool isWin { get; set; } = false;



    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {
        Invoke("StartGame", 2f);
    }

    void Update()
    {
        
    }

    public void StartGame()
    {
        LoadSceneWithName("GameScene");
    }

    public void EndGame(bool isWin)
    {
        this.isWin = isWin;
        Debug.Log("Game is Over");
        LoadSceneWithName("GameEndScene");
    }

    public void LoadSceneWithName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

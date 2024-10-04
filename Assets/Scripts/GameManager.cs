using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance { get; private set; }
    public int stageNum { get; set; } = 0;
    public bool isWin { get; set; } = false;



    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {
        // TODO
        // 
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

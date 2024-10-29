using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    private bool isWin = false;



    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
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

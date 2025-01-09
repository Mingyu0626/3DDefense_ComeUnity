using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public bool IsWin { get; private set; } = false;
    public InputActions Action { get; private set; }
    private GameObject settingsPanel;

    public void EndGame(bool isWin)
    {
        IsWin = isWin;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        LoadSceneWithName(SceneNames.GameEndScene);
    }
    public void LoadSceneWithName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

public static class SceneNames
{
    public const string LobbyScene = "LobbyScene";
    public const string GameScene = "GameScene";
    public const string GameEndScene = "GameEndScene";
}


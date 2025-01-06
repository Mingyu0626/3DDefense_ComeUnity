using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool IsWin { get; private set; } = false;
    public InputActions Action { get; private set; }
    private GameObject settingsPanel;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        Action = new InputActions();
        DontDestroyOnLoad(gameObject);
    }

    public void EndGame(bool isWin)
    {
        IsWin = isWin;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        LoadSceneWithName("GameEndScene");
    }
    public void LoadSceneWithName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public bool IsWin { get; private set; } = false;
    public InputActions Action { get; private set; }

    private List<ISceneObserver> observerList = new List<ISceneObserver>();

    protected override void Awake()
    {
        base.Awake();
        if (!isDestroyed)
        {
            if (Action == null)
            {
                Action = new InputActions();
                SceneManager.sceneLoaded += NotifySceneChanged;
            }
        }
    }

    public void AddObserver(ISceneObserver observer)
    {
        if (!observerList.Contains(observer))
        {
            observerList.Add(observer);
        }
    }
    public void RemoveObserver(ISceneObserver observer)
    {
        if (observerList.Contains(observer))
        {
            observerList.Remove(observer);
        }
    }
    public void NotifySceneChanged(Scene scene, LoadSceneMode mode)
    {
        foreach (var observer in observerList)
        {
            observer.OnSceneChanged(scene.name);
        }
    }

    public void EndGame(bool isWin)
    {
        IsWin = isWin;
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


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public bool IsWin { get; private set; } = false;
    private List<ISceneObserver> observerList = new List<ISceneObserver>();

    protected override void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded += NotifyCompleteSceneChange;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        observerList.Clear();
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
    public void NotifyStartSceneChange(Scene scene, LoadSceneMode mode)
    {
        foreach (var observer in observerList)
        {
            observer.OnSceneClosed(scene.name);
        }
    }
    public void NotifyCompleteSceneChange(Scene scene, LoadSceneMode mode)
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
        UIManager.Instance.OnSceneClosed(() => SceneManager.LoadScene(sceneName));
    }
}

public static class SceneNames
{
    public const string LoadingScene = "LoadingScene";
    public const string LobbyScene = "LobbyScene";
    public const string GameScene = "GameScene";
    public const string GameEndScene = "GameEndScene";
}


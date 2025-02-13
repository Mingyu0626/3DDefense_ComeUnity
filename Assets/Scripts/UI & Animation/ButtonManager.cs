using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class ButtonManager : Singleton<ButtonManager>, ISceneObserver
{
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        GameManager.Instance.AddObserver(this);
    }
    private void OnDestroy()
    {
        GameManager.Instance.RemoveObserver(this);
    }
    public void OnSceneChanged(string sceneName)
    {
        AddButtonOnClickEvent(sceneName);
    }
    private void AddButtonOnClickEvent(string sceneName)
    {
        switch (sceneName)
        {
            case SceneNames.LobbyScene:
                Button startBtn = GameObject.Find("Button_Start").GetComponent<Button>();
                if (startBtn != null)
                {
                    AddListenerOnButton(startBtn, SceneNames.GameScene);
                }
                Button settingsBtn = GameObject.Find("Button_Settings").GetComponent<Button>();
                if (settingsBtn != null)
                {
                    AddListenerOnButton(settingsBtn, UIManager.Instance.OpenSettingsPanel, true);
                }
                break;
            case SceneNames.GameEndScene:
                Button retryBtn = GameObject.Find("Button_Retry").GetComponent<Button>();
                if (retryBtn != null)
                {
                    AddListenerOnButton(retryBtn, SceneNames.GameScene);
                }

                Button goToLobbyBtn = GameObject.Find("Button_GoToLobby").GetComponent<Button>();
                if (goToLobbyBtn is not null)
                {
                    AddListenerOnButton(goToLobbyBtn, SceneNames.LobbyScene);
                }
                break;
            default:
                break;
        }
    }
    private void AddListenerOnButton(Button btn, string sceneName)
    {
        btn.onClick.RemoveListener(() => GameManager.Instance.LoadSceneWithName(sceneName));
        btn.onClick.AddListener(() => GameManager.Instance.LoadSceneWithName(sceneName));
    }
    private void AddListenerOnButton(Button btn, UnityAction listener)
    {
        btn.onClick.RemoveListener(() => listener());
        btn.onClick.AddListener(() => listener());
    }
    private void AddListenerOnButton<T>(Button btn, UnityAction<T> listener, T param)
    {
        btn.onClick.RemoveListener(() => listener(param));
        btn.onClick.AddListener(() => listener(param));
    }
    private void AddListenerOnButton<T1, T2>(Button btn, UnityAction<T1, T2> listener, T1 param1, T2 param2)
    {
        btn.onClick.RemoveListener(() => listener(param1, param2));
        btn.onClick.AddListener(() => listener(param1, param2));
    }
    private void RemoveListenerOnButton(Button btn, string sceneName)
    {
        btn.onClick.RemoveListener(() => GameManager.Instance.LoadSceneWithName(sceneName));
    }
}

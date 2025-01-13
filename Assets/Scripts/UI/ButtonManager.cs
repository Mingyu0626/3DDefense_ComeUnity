using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ButtonManager : Singleton<ButtonManager>
{
    protected override void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!isDestroyed)
        {
            AddButtonOnClickEvent();
        }
    }

    private void AddButtonOnClickEvent()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName.Equals(SceneNames.LobbyScene))
        {
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
        }

        if (currentSceneName.Equals(SceneNames.GameEndScene))
        {
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
        }
    }
    private void AddListenerOnButton(Button btn, string sceneName)
    {
        btn.onClick.AddListener(() => GameManager.Instance.LoadSceneWithName(sceneName));
    }
    private void AddListenerOnButton(Button btn, UnityAction listener)
    {
        btn.onClick.AddListener(() => listener());
    }
    private void AddListenerOnButton<T>(Button btn, UnityAction<T> listener, T param)
    {
        btn.onClick.AddListener(() => listener(param));
    }
    private void AddListenerOnButton<T1, T2>(Button btn, UnityAction<T1, T2> listener, T1 param1, T2 param2)
    {
        btn.onClick.AddListener(() => listener(param1, param2));
    }
}

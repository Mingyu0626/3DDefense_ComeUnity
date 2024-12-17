using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ButtonManager : MonoBehaviour
{
    public enum SceneName
    {
        LobbyScene,
        GameScene,
        GameEndScene
    }
    public static ButtonManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(gameObject);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AddButtonOnClickEvent();
    }

    private void AddButtonOnClickEvent()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName.Equals(SceneName.LobbyScene.ToString()))
        {
            Button startBtn = GameObject.Find("Button_Start").GetComponent<Button>();
            if (startBtn != null)
            {
                AddListenerOnButton(startBtn, SceneName.GameScene.ToString());
            }
            Button settingsBtn = GameObject.Find("Button_Settings").GetComponent<Button>();
            if (settingsBtn != null)
            {
                AddListenerOnButton(settingsBtn, UIManager.Instance.OpenSettingsPanel, true);
            }
        }

        if (currentSceneName.Equals(SceneName.GameEndScene.ToString()))
        {
            Button retryBtn = GameObject.Find("Button_Retry").GetComponent<Button>();
            if (retryBtn != null)
            {
                AddListenerOnButton(retryBtn, SceneName.GameScene.ToString());
            }

            Button goToLobbyBtn = GameObject.Find("Button_GoToLobby").GetComponent<Button>();
            if (goToLobbyBtn is not null)
            {
                AddListenerOnButton(goToLobbyBtn, SceneName.LobbyScene.ToString());
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
}

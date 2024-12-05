using UnityEngine;
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
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
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
            Button startBtn = GameObject.Find("ButtonStart").GetComponent<Button>();
            if (startBtn != null)
            {
                AddListenerOnButton(startBtn, SceneName.GameScene.ToString());
            }

            Button settingsBtn = GameObject.Find("ButtonSettings").GetComponent<Button>();
            if (settingsBtn != null)
            {

            }
        }

        if (currentSceneName.Equals(SceneName.GameEndScene.ToString()))
        {
            Button retryBtn = GameObject.Find("ButtonRetry").GetComponent<Button>();
            if (retryBtn != null)
            {
                AddListenerOnButton(retryBtn, SceneName.GameScene.ToString());
            }

            Button goToLobbyBtn = GameObject.Find("ButtonGoToLobby").GetComponent<Button>();
            if (goToLobbyBtn != null)
            {
                AddListenerOnButton(goToLobbyBtn, SceneName.LobbyScene.ToString());
            }
        }
    }
    private void AddListenerOnButton(Button btn, string sceneName)
    {
        btn.onClick.AddListener(() => GameManager.Instance.LoadSceneWithName(sceneName));
    }
}

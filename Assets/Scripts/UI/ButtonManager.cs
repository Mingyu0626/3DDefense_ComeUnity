using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{

    private void Awake()
    {
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
        if (currentSceneName.Equals("LobbyScene"))
        {
            Button startBtn = GameObject.Find("ButtonStart").GetComponent<Button>();
            if (startBtn != null)
            {
                AddListenerOnButton(startBtn, "GameScene");
            }

            Button settingsBtn = GameObject.Find("ButtonSettings").GetComponent<Button>();
            if (settingsBtn != null)
            {

            }
        }

        if (currentSceneName.Equals("GameEndScene"))
        {
            Button retryBtn = GameObject.Find("ButtonRetry").GetComponent<Button>();
            if (retryBtn != null)
            {
                AddListenerOnButton(retryBtn, "GameScene");
            }

            Button goToLobbyBtn = GameObject.Find("ButtonGoToLobby").GetComponent<Button>();
            if (goToLobbyBtn != null)
            {
                AddListenerOnButton(goToLobbyBtn, "LobbyScene");
            }
        }
    }
    private void AddListenerOnButton(Button btn, string sceneName)
    {
        btn.onClick.AddListener(() => GameManager.Instance.LoadSceneWithName(sceneName));
    }

    void Start()
    {
    }


}

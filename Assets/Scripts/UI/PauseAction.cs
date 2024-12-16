using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PauseAction : MonoBehaviour
{
    InputActions action;
    InputAction pauseAction;

    [SerializeField] private GameObject PausedTabGO;

    private void Awake()
    {
        action = GameManager.Instance.Action;
        action.UI.Enable();

        pauseAction = action.UI.Pause;
        pauseAction.performed += OnPaused;
    }

    private void OnDestroy()
    {
        action.UI.Disable();
    }

    private void OnPaused(InputAction.CallbackContext context)
    {
        if (PausedTabGO is not null)
        {
            PausedTabGO.SetActive(!PausedTabGO.activeSelf);
            if (PausedTabGO.activeSelf)
            {
                GameManager.Instance.PauseGame();
            }
            else
            {
                GameManager.Instance.ResumeGame();
            }
        }
    }

}

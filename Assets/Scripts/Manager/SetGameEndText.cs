using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGameEndText : MonoBehaviour
{
    [SerializeField]
    private GameObject gameClearTextGO;
    [SerializeField]
    private GameObject gameOverTextGO;
    void Start()
    {
        if (gameClearTextGO != null && gameOverTextGO != null)
        {
            if (GameManager.Instance.IsWin)
            {
                gameClearTextGO.SetActive(true);
                gameOverTextGO.SetActive(false);
            }
            else
            {
                gameClearTextGO.SetActive(false);
                gameOverTextGO.SetActive(true);
            }
        }
    }
}

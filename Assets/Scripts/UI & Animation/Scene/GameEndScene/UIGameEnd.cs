using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameEndScene : MonoBehaviour
{
    [SerializeField]
    private GameObject winText;
    [SerializeField]
    private GameObject loseText;


    private void Awake()
    {
        EnableAppropriateText();
    }

    private void EnableAppropriateText()
    {
        if (GameManager.Instance.IsWin)
        {
            if (winText != null)
            {
                winText.SetActive(true);
            }
        }
        else
        {
            if (loseText != null)
            {
                loseText.SetActive(true);
            }
        }
    }

}

using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI introTMP;

    private void Start()
    {
        FadeManager fadeManager = UIManager.Instance.FadeManager;
        StartCoroutine(fadeManager.FadeIn(introTMP, 1f, () => 
            StartCoroutine(fadeManager.FadeOut(introTMP, 1f, () => 
            GameManager.Instance.LoadSceneWithName(SceneNames.LobbyScene))))); 
    }
}


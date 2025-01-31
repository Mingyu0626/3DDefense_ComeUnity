using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Intro : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI introTMP;

    private void Start()
    {
        UIManager.Instance.AnimationManager.FadeIn(ref introTMP, 2f,
            () => UIManager.Instance.AnimationManager.FadeOut(ref introTMP, 2f,
            () => GameManager.Instance.LoadSceneWithName(SceneNames.LobbyScene)));
    }
}


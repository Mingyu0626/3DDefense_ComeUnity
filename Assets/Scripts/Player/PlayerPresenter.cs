using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerDataModel), typeof(UIPlayerView))]
public class PlayerPresenter : MonoBehaviour
{
    private PlayerDataModel playerDataModel;
    private UIPlayerView playerView;

    private void Awake()
    {
        playerDataModel = GetComponent<PlayerDataModel>();
        playerView = GetComponent<UIPlayerView>();
    }
    private void Start()
    {
        playerDataModel.PlayerHP = playerDataModel.PlayerHPMax;
        playerDataModel.PlayerHPChanged += playerView.SetSliderPlayerHP;
        playerView.InitSliderPlayerHP(playerDataModel.PlayerHPMax);
    }

    public void OnPlayerDamaged(int damage)
    {
        playerDataModel.PlayerHP -= damage;
    }
    public int GetPlayerHP() { return playerDataModel.PlayerHP; }
}

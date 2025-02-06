using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPresenter : MonoBehaviour
{
    [SerializeField] private PlayerDataModel playerDataModel;
    [SerializeField] private UIPlayerView playerView;

    private void Start()
    {
        playerDataModel.PlayerHP = playerDataModel.PlayerHPMax;
        playerDataModel.PlayerHPChanged += playerView.SetSliderPlayerHP;
        playerView.SetSliderMaxValue(playerDataModel.PlayerHPMax);
    }

    public void OnPlayerDamaged(int damage)
    {
        playerDataModel.PlayerHP -= damage;
    }
    public int GetPlayerHP() { return playerDataModel.PlayerHP; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerPresenter))]
public class Player : MonoBehaviour
{
    // 오로지 Enemy의 플레이어 추적만을 위한 싱글톤인데...
    // Enemy에서 다른 방식으로 플레이어의 Transform을 실시간으로 접근할 수 있다면?
    public static Player Instance { get; private set; }
    private PlayerPresenter playerPresenter;

    private void Awake()
    {
        playerPresenter = GetComponent<PlayerPresenter>();
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyAttack"))
        {
            EnemyBullet enemyBullet = other.GetComponent<EnemyBullet>();
            if (enemyBullet != null)
            {
                ApplyDamage(enemyBullet.GetDamage());
                if (CheckDeath())
                {
                    GameManager.Instance.LoseGame();
                }
            }
        }
    }
    private void ApplyDamage(int damage)
    {
        playerPresenter.OnPlayerDamaged(damage);
    }
    private bool CheckDeath()
    {
        return playerPresenter.GetPlayerHP() <= 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerPresenter))]
public class Player : MonoBehaviour
{
    // ������ Enemy�� �÷��̾� �������� ���� �̱����ε�...
    // Enemy���� �ٸ� ������� �÷��̾��� Transform�� �ǽð����� ������ �� �ִٸ�?
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

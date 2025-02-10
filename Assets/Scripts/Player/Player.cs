using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerPresenter))]
public class Player : MonoBehaviour
{
    // 오로지 Enemy의 TracePlayer만을 위한 코드인데, 개선 방안이 분명 존재할것임
    // Enemy에서 다른 방식으로 공격 대상의 Transform을 실시간으로 변경할 수 있으면
    // 싱글톤으로 할 필요도 없어짐
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
            EnemyBullet EnemyAttack = other.GetComponent<EnemyBullet>();
            if (EnemyAttack != null)
            {
                ApplyDamage(EnemyAttack.GetDamage());
            }
            if (playerPresenter.GetPlayerHP() <= 0)
            {
                GameManager.Instance.EndGame(false);
            }
        }
    }
    private void ApplyDamage(int damage)
    {
        playerPresenter.OnPlayerDamaged(damage);
    }
}

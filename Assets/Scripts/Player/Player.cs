using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerPresenter))]
public class Player : MonoBehaviour
{
    // ������ Enemy�� TracePlayer���� ���� �ڵ��ε�, ���� ����� �и� �����Ұ���
    // Enemy���� �ٸ� ������� ���� ����� Transform�� �ǽð����� ������ �� ������
    // �̱������� �� �ʿ䵵 ������
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

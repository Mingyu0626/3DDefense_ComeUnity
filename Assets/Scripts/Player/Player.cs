using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // ������ Enemy�� TracePlayer���� ���� �ڵ��ε�, ���� ����� �и� �����Ұ���
    // Enemy���� �ٸ� ������� ���� ����� Transform�� �ǽð����� ������ �� ������
    // �̱������� �� �ʿ䵵 ������
    public static Player Instance { get; private set; }
    public Transform PlayerTransform { get; private set; }
    private PlayerPresenter presenter;

    private void Awake()
    {
        presenter = GetComponent<PlayerPresenter>();
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        PlayerTransform = transform;
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
            if (presenter.GetPlayerHP() <= 0)
            {
                GameManager.Instance.EndGame(false);
            }
        }
    }
    private void ApplyDamage(int damage)
    {
        presenter.OnPlayerDamaged(damage);
    }
}

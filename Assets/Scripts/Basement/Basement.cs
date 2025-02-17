using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basement : MonoBehaviour
{
    // ������ Enemy�� TracePlayer���� ���� �ڵ��ε�, ���� ����� �и� �����Ұ���
    // Enemy���� �ٸ� ������� ���� ����� Transform�� �ǽð����� ������ �� ������
    // �̱������� �� �ʿ䵵 ������
    public static Basement Instance { get; private set; }
    public Transform BasementTransform { get; private set; }
    private BasementPresenter presenter;
    private void Awake()
    {
        presenter = GetComponent<BasementPresenter>();
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
        BasementTransform = transform;
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
            if (presenter.GetBasementHP() <= 0)
            {
                GameManager.Instance.LoseGame();
            }
        }
    }
    private void ApplyDamage(int damage)
    {
        presenter.OnBasementDamaged(damage);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HPComponent : MonoBehaviour
{

    [SerializeField]
    public int MaxHP { get; private set; } = 100;
    protected int curHP;

    protected virtual void Awake()
    {
        curHP = MaxHP;
    }
    protected virtual void ApplyDamage(int damage)
    {
        curHP -= damage;
        curHP = Mathf.Clamp(curHP, 0, MaxHP);
    }

    public bool IsHPZero()
    {
        return curHP == 0;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyAttack"))
        {
            EnemyBullet EnemyAttack = other.GetComponent<EnemyBullet>();
            if (EnemyAttack != null)
            {
                ApplyDamage(EnemyAttack.GetDamage());
            }
            if (IsHPZero())
            {
                // �÷��̾� ��� or ���� �ı��� ���� ���� ����
                GameManager.Instance.EndGame(false);
            }
        }
    }
}
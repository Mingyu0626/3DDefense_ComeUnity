using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HPComponent : MonoBehaviour
{

    [SerializeField]
    private int maxHP = 100;
    private int curHP;

    protected virtual void Awake()
    {
        curHP = maxHP;
    }
    public void ApplyDamage(int damage)
    {
        curHP -= damage;
    }

    public bool IsHPNoMoreThanZero()
    {
        return curHP <= 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyAttack"))
        {
            Bullet EnemyAttack = other.GetComponent<Bullet>();
            ApplyDamage(EnemyAttack.GetDamage());
            if (IsHPNoMoreThanZero())
            {
                // 플레이어 사망 or 기지 파괴에 의한 게임 종료
                GameManager.Instance.EndGame(false);
            }
        }
    }
}

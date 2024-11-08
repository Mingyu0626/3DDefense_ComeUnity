using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HPComponent : MonoBehaviour
{

    [SerializeField]
    public int MaxHP { get; private set; } = 100;
    private int curHP;

    protected virtual void Awake()
    {
        curHP = MaxHP;
    }
    public void ApplyDamage(int damage)
    {
        curHP -= damage;
        curHP = Mathf.Clamp(curHP, 0, MaxHP);
        UIManager.Instance.SetPlayerHPSlider(curHP);
    }

    public bool IsHPZero()
    {
        return curHP == 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyAttack"))
        {
            Bullet EnemyAttack = other.GetComponent<Bullet>();
            ApplyDamage(EnemyAttack.GetDamage());
            if (IsHPZero())
            {
                // 플레이어 사망 or 기지 파괴에 의한 게임 종료
                GameManager.Instance.EndGame(false);
            }
        }
    }
}

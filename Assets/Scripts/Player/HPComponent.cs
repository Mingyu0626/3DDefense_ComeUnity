using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPComponent : MonoBehaviour // Player, Basement의 부모 클래스
{
    [SerializeField]
    public int MaxHP { get; private set; } = 100; // 최대 체력
    protected int curHP; // 현재 체력, protected로 하는 이유는, 추후 자식 클래스에서 curHP를 기반으로 UI를 변경하기 때문

    protected virtual void Awake()
    {
        curHP = MaxHP;
    }
    protected virtual void ApplyDamage(int damage) // 데미지를 적용하는 함수
    {
        curHP -= damage;
        curHP = Mathf.Clamp(curHP, 0, MaxHP); 
        // 현재 curHP의 값을 0 ~ MaxHP의 값으로 보간
        // curHP가 음수로 떨어졌을 때, curHP를 0으로 바꿔주는 역할
    }

    public bool IsHPZero() // curHP가 0인지 확인하는 함수
    {
        return curHP == 0;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyAttack")) // 충돌 감지된 대상이 "EnemyAttack"이라는 태그를 가진 게임 오브젝트일 때
        {
            Bullet EnemyAttack = other.GetComponent<Bullet>(); 
            if (EnemyAttack != null)
            {
                ApplyDamage(EnemyAttack.Damage); // 적의 공격 오브젝트(Bullet)
            }
            if (IsHPZero()) // 플레이어 또는, 기지의 HP가 0이면
            {
                // 플레이어 사망 or 기지 파괴에 의한 게임 종료
                GameManager.Instance.EndGame(false);
            }
        }
    }
}

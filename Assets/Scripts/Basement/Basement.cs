using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basement : MonoBehaviour
{
    // 오로지 Enemy의 TracePlayer만을 위한 코드인데, 개선 방안이 분명 존재할것임
    // Enemy에서 다른 방식으로 공격 대상의 Transform을 실시간으로 변경할 수 있으면
    // 싱글톤으로 할 필요도 없어짐
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

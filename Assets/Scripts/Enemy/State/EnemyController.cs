using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyControlState
{
    public class EnemyController : PoolAble
    {
        public EnemyState tracePlayerState, attackPlayerState, goBasementState, attackBasementState;
        public EnemyStateContext enemyStateContext;

        private int maxHp = 1;
        private int hp;
        private int damage;
        public float Speed { get; private set; } = 5f;

        private float playerDetectableDistance = 40f;
        private float playerAttackableDistance = 20f;
        private float basementAttackableDistance = 10f;

        [SerializeField]
        public GameObject attackPoint;

        void OnEnable()
        {
            hp = maxHp;
            enemyStateContext.ChangeState(tracePlayerState);
        }

        void Start()
        {
            enemyStateContext = new EnemyStateContext(this);
            tracePlayerState = new EnemyTracePlayerState();
            attackPlayerState = new EnemyAttackBasementState();
            goBasementState = new EnemyGoBasementState();
            attackBasementState = new EnemyAttackBasementState();
        }

        protected virtual void OnDisable()
        {
            // 뭔가 디자인 패턴 사용해서 개선될거같은데..
            // 여기보다 더 Fit한 처리 부분이 있을거 같다
            StageManager.Instance.CurrentKilledEnemyCount++;
            StageManager.Instance.CurrentEnemyCount--;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("PlayerBullet"))
            {
                Bullet playerBullet = other.GetComponent<Bullet>();
                ApplyDamage(playerBullet.GetDamage());
            }
        }

        private void CreateDeathReactionGO()
        {
            GameObject enemyDeathReactionGO = ObjectPoolManager.Instance.GetGameObject("EnemyDeathReaction");
            if (enemyDeathReactionGO != null)
            {
                enemyDeathReactionGO.transform.position = transform.position;
            }
        }

        private void ApplyDamage(int damage)
        {
            hp -= damage;
            if (hp <= 0)
            {
                ReleaseObject();
                CreateDeathReactionGO();
                StageManager.Instance.CheckClearCondition();
            }
        }

        public bool CanDetectPlayer()
        { 
            return Vector3.Distance(Player.Instance.PlayerTransform.position, transform.position)
                <= playerDetectableDistance;
        }

        public bool CanAttackPlayer()
        {
            return Vector3.Distance(Player.Instance.PlayerTransform.position, transform.position)
                <= playerAttackableDistance;
        }

        public bool CanAttackBasement()
        {
            return Vector3.Distance(Basement.Instance.BasementTransform.position, transform.position)
                <= basementAttackableDistance;
        }

        public void StartStateCoroutine(IEnumerator coroutine)
        {
            StartCoroutine(coroutine); 
        }

        public void StopStateCoroutine(IEnumerator coroutine)
        {
            StopCoroutine(coroutine);
        }
    }
}

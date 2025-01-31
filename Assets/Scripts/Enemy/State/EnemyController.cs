using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyControlState
{
    public class EnemyController : PoolAble
    {
        public EnemyState tracePlayerState, attackPlayerState, goBasementState, attackBasementState;
        public EnemyStateContext enemyStateContext;

        public int MaxHP { get; protected set; }
        public int HP { get; protected set; }
        public int Damage { get; protected set; }
        public float Speed { get; protected set; }
        public float RotationSpeed { get; protected set; }  

        private float playerDetectableDistance = 50f;
        private float playerAttackableDistance = 20f;
        private float basementAttackableDistance = 20f;

        [SerializeField]
        public GameObject attackPoint;

        protected virtual void OnEnable()
        {
            HP = MaxHP;
        }

        void Start()
        {
            enemyStateContext = new EnemyStateContext(this);
            tracePlayerState = new EnemyTracePlayerState();
            attackPlayerState = new EnemyAttackPlayerState();
            goBasementState = new EnemyGoBasementState();
            attackBasementState = new EnemyAttackBasementState();
            enemyStateContext.ChangeState(goBasementState);
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

        protected virtual void Update()
        {
            if (enemyStateContext.currentState != null)
            {
                enemyStateContext.currentState.Update();
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
            HP -= damage;
            if (HP <= 0)
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

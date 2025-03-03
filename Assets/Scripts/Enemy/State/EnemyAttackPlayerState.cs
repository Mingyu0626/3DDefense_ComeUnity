using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyControlState
{
    public class EnemyAttackPlayerState : IEnemyState
    {
        private EnemyController enemyController;
        private float attackPlayerInterval = 3f;

        public void Enter(EnemyController controller)
        {
            if (!enemyController)
            {
                enemyController = controller;
            }
            // Debug.Log("EnemyAttackBasementState ¡¯¿‘");
            enemyController.StartStateCoroutine(AttackPlayer());
        }

        public void Update()
        {
            if (!enemyController.CanAttackPlayer())
            {
                enemyController.enemyStateContext.ChangeState(enemyController.tracePlayerState);
            }
        }

        public void Exit()
        {
            enemyController.StopStateCoroutine(AttackPlayer());
        }

        IEnumerator AttackPlayer()
        {
            while (true)
            {

                GameObject enemyBulletGO = ObjectPoolManager.Instance.GetObject("EnemyBullet");
                if (enemyBulletGO != null && enemyController.AttackPoint != null)
                {
                    enemyBulletGO.transform.position = enemyController.AttackPoint.transform.position;
                    enemyBulletGO.transform.rotation = enemyController.AttackPoint.transform.rotation;
                    yield return new WaitForSeconds(attackPlayerInterval);
                }
                yield return null;
            }
        }
    }
}

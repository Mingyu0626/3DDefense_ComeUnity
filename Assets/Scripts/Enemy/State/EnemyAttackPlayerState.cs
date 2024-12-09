using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyControlState
{
    public class EnemyAttackPlayerState : EnemyState
    {
        private EnemyController enemyController;
        private float attackPlayerInterval = 3f;

        public void Enter(EnemyController controller)
        {
            if (!enemyController)
            {
                enemyController = controller;
            }
            Debug.Log("EnemyAttackBasementState ¡¯¿‘");
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

                GameObject enemyBulletGO = ObjectPoolManager.Instance.GetGameObject("EnemyBullet");
                if (enemyBulletGO != null && enemyController.attackPoint != null)
                {
                    enemyBulletGO.transform.position = enemyController.attackPoint.transform.position;
                    enemyBulletGO.transform.rotation = enemyController.attackPoint.transform.rotation;
                    yield return new WaitForSeconds(attackPlayerInterval);
                }
                yield return null;
            }
        }
    }
}

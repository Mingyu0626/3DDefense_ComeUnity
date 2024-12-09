using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EnemyControlState
{
    public class EnemyTracePlayerState : EnemyState
    {
        private EnemyController enemyController;
        public void Enter(EnemyController controller)
        {
            if (!enemyController)
            {
                enemyController = controller;
            }
            Debug.Log("EnemyTracePlayerState ¡¯¿‘");
        }

        public void Update()
        {
            TracePlayer();
            if (!enemyController.CanDetectPlayer())
            {
                enemyController.enemyStateContext.ChangeState(enemyController.goBasementState);
            }

            if (enemyController.CanAttackPlayer())
            {
                enemyController.enemyStateContext.ChangeState(enemyController.attackPlayerState);
            }
        }

        public void Exit()
        {
        }

        private void TracePlayer()
        {
            Transform playerTransform = Player.Instance.PlayerTransform;
            enemyController.transform.LookAt(playerTransform);
            enemyController.transform.position = Vector3.MoveTowards
                (enemyController.transform.position, 
                playerTransform.position, 
                enemyController.Speed * Time.deltaTime);
        }
    }
}

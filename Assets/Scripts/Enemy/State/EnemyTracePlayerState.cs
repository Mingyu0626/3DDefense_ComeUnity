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
            // Debug.Log("EnemyTracePlayerState ¡¯¿‘");
        }

        public void Update()
        {
            TracePlayer();
            if (!enemyController.CanDetectPlayer())
            {
                enemyController.enemyStateContext.ChangeState(enemyController.goBasementState);
            }

            else if (enemyController.CanAttackPlayer())
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
            Transform enemyTransform = enemyController.transform;

            enemyTransform.rotation = Quaternion.Slerp
                (enemyTransform.rotation,
                Quaternion.LookRotation(playerTransform.position - enemyTransform.position),
                enemyController.enemyData.RotationSpeed * Time.deltaTime);

            enemyTransform.position = Vector3.MoveTowards
                (enemyTransform.position, playerTransform.position, 
                enemyController.enemyData.MoveSpeed * Time.deltaTime);
        }
    }
}

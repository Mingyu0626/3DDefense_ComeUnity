using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyControlState
{
    public class EnemyGoBasementState : EnemyState
    {
        private EnemyController enemyController;
        private float basementAttackableDistance = 10f;
        public void Enter(EnemyController controller)
        {
            if (!enemyController)
            {
                enemyController = controller;
            }
            Debug.Log("EnemyGoBasementState ¡¯¿‘");
        }

        public void Update()
        {
            GoBasement();
            if (enemyController.CanDetectPlayer())
            {
                enemyController.enemyStateContext.ChangeState(enemyController.tracePlayerState);
            }

            if (enemyController.CanAttackBasement())
            {
                enemyController.enemyStateContext.ChangeState(enemyController.attackBasementState);
            }
        }

        public void Exit()
        {

        }

        private void GoBasement()
        {
            Transform basementTransform = Basement.Instance.BasementTransform;
            enemyController.transform.LookAt(basementTransform);
            enemyController.transform.position = Vector3.MoveTowards
                (enemyController.transform.position,
                basementTransform.position,
                enemyController.Speed * Time.deltaTime);
        }
    }
}


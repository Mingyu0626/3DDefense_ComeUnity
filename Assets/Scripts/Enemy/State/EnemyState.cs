using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyControlState
{
    public interface EnemyState
    {
        public void Enter(EnemyController controller);
        public void Update();
        public void Exit();
    }

    public class EnemyStateContext
    {
        public EnemyState currentState { get; private set; }
        private EnemyController enemyController;

        public EnemyStateContext(EnemyController controller)
        {
            enemyController = controller;
        }

        public void ChangeState()
        {
            currentState = new EnemyGoBasementState();
            currentState.Enter(enemyController);
        }

        public void ChangeState(EnemyState nextState)
        {
            if (currentState == null)
            {
                currentState = nextState;
                currentState.Enter(enemyController);
            }
            else
            {
                currentState.Exit();
                currentState = nextState;
                currentState.Enter(enemyController);
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyControlState
{
    public enum EnemyName
    {
        Slime,
        TurtleShell
    }

    public struct EnemyData
    {
        private string name;

        private int maxHp;
        private int curHp;
        private int attackDamage;
        private float moveSpeed;
        private float rotationSpeed;

        private float playerDetactableDistance;
        private float playerAttackableDistance;
        private float basementAttackableDistance;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int MaxHP
        {
            get { return maxHp; }
            set { maxHp = value; }
        }
        public int CurHP
        {
            get { return curHp; }
            set { curHp = value; }
        }
        public int AttackDamage
        {
            get { return attackDamage; }
            set { attackDamage = value; }
        }
        public float MoveSpeed
        {
            get { return moveSpeed; }
            set { moveSpeed = value; }
        }
        public float RotationSpeed
        {
            get { return rotationSpeed; }
            set { rotationSpeed = value; }
        }
        public float PlayerDetectableDistance
        {
            get { return playerDetactableDistance; }
            set { playerDetactableDistance = value; }
        }
        public float PlayerAttackableDistance
        {
            get { return playerAttackableDistance; }
            set { playerAttackableDistance = value; }
        }
        public float BasementAttackableDistance
        {
            get { return basementAttackableDistance; }
            set { basementAttackableDistance = value; }
        }

        public EnemyData(string name, int maxHp, int curHp, int attackDamage, float moveSpeed, float rotationSpeed,
            float playerDetactableDistance, float playerAttackableDistance, float basementAttackableDistance)
        {
            this.name = name;
            this.maxHp = maxHp;
            this.curHp = curHp;
            this.attackDamage = attackDamage;
            this.moveSpeed = moveSpeed;
            this.rotationSpeed = rotationSpeed;
            this.playerDetactableDistance = playerDetactableDistance;
            this.playerAttackableDistance = playerAttackableDistance;
            this.basementAttackableDistance = basementAttackableDistance;
        }
    }

    public static class EnemyDataDictionary
    {
        private static readonly Dictionary<EnemyName, EnemyData> enemyDataDict = new Dictionary<EnemyName, EnemyData>
    {
        { EnemyName.Slime, new EnemyData(nameof(EnemyName.Slime), 1, 1, 1, 8f, 4f, 50f, 20f, 20f) },
        { EnemyName.TurtleShell, new EnemyData(nameof(EnemyName.TurtleShell), 3, 3, 2, 5f, 2f, 50f, 25f, 25f) }
    };

        public static EnemyData GetEnemyData(EnemyName type)
        {
            return enemyDataDict.TryGetValue(type, out var data) ? data : default;
        }
    }
}
using GMTK2021.Gameplay.Enemy.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GMTK2021.Gameplay.Enemy
{
    public class BuggyBehavior : EnemyBehavior
    {
        [SerializeField]
        private float distanceToPlayer;

        public Vector3? TargetPosition { get; private set; }

        protected override void Initialize()
        {
            AddState<BuggyIdleState>();
            AddState<BuggyMoveState>();
        }

        public bool CheckForPlayer()
        {
            Vector3 playerLocation = GameManager.Player.transform.position;
            if (Physics.Raycast(transform.position, playerLocation - transform.position, out RaycastHit hitInfo, distanceToPlayer))
            {
                if (hitInfo.transform.CompareTag("Player"))
                {
                    TargetPosition = hitInfo.point;
                    return true;
                }
            }
            if (NavMeshAgent.isStopped)
                TargetPosition = null;
            return false;
        }
    }
}

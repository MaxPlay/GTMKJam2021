using GMTK2021.Gameplay.Enemy.Base;
using System;
using UnityEngine;

namespace GMTK2021.Gameplay.Enemy
{
    public class BuggyMoveState : EnemyState
    {
        float playerCheckTimer;

        public override void Enter()
        {
            var owner = GetOwner<BuggyBehavior>();
            if (owner.TargetPosition == null)
            {
                SetState<BuggyIdleState>();
                return;
            }

            owner.NavMeshAgent.SetDestination(owner.TargetPosition.Value);
        }

        public override void Exit()
        {

        }

        public override void Update()
        {
            if (playerCheckTimer <= 0)
            {
                var owner = GetOwner<BuggyBehavior>();
                if (owner.CheckForPlayer())
                {
                    SetState<BuggyIdleState>();
                }
                playerCheckTimer = 0.5f;
            }
            playerCheckTimer -= Time.deltaTime;
        }
    }
}
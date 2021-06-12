using GMTK2021.Gameplay.Enemy.Base;
using System;
using UnityEngine;

namespace GMTK2021.Gameplay.Enemy
{
    public class GunnerMoveState : EnemyState
    {
        float playerCheckTimer;

        public override void Enter()
        {
            var owner = GetOwner<GunnerBehaviour>();
            if (owner.TargetPosition == null)
            {
                SetState<GunnerIdleState>();
                return;
            }

            owner.NavMeshAgent.SetDestination(owner.TargetPosition.Value);
        }

        public override void Exit()
        {

        }

        public override void Update()
        {
            var owner = GetOwner<GunnerBehaviour>();
            if (playerCheckTimer <= 0)
            {
                switch(owner.CheckForPlayer())
                {
                    case 0: SetState<GunnerIdleState>();
                        break;
                    case 1: Enter();
                        break;
                }
                playerCheckTimer = 0.5f;
            }
            if(owner.CheckForPlayer() == 2)
            {
                SetState<GunnerShotState>();
            }
            playerCheckTimer -= Time.deltaTime;
        }
    }
}
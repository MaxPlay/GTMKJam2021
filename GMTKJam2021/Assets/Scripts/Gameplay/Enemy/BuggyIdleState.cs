using GMTK2021.Gameplay.Enemy.Base;
using UnityEngine;

namespace GMTK2021.Gameplay.Enemy
{
    public class BuggyIdleState : EnemyState
    {
        float playerCheckTimer;

        public override void Enter()
        {

        }

        public override void Exit()
        {

        }

        public override void Update()
        {
            if (playerCheckTimer <= 0)
            {
                if (GetOwner<BuggyBehavior>().CheckForPlayer())
                {
                    SetState<BuggyMoveState>();
                }
                playerCheckTimer = 0.5f;
            }

            playerCheckTimer -= Time.deltaTime;
        }
    }
}
using GMTK2021.Gameplay.Enemy.Base;
using UnityEngine;

namespace GMTK2021.Gameplay.Enemy
{
    public class GunnerIdleState : EnemyState
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
                switch (GetOwner<GunnerBehaviour>().CheckForPlayer())
                {
                    case 1:
                    case 2:
                        SetState<GunnerMoveState>();
                        break;
                }
                playerCheckTimer = 0.5f;
            }

            playerCheckTimer -= Time.deltaTime;
        }
    }
}
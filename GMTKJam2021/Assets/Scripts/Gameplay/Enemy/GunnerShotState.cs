using GMTK2021.Gameplay.Enemy.Base;
using UnityEngine;

namespace GMTK2021.Gameplay.Enemy
{
    public class GunnerShotState : EnemyState
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

            owner.NavMeshAgent.SetDestination(Transform.position);
            owner.NavMeshAgent.velocity = Vector3.zero;

            owner.Animator.SetTrigger("Shoot");

            ProjectileBehaviour newProjectile = GameObject.Instantiate(owner.projectile, owner.ShootPosition.position, new Quaternion(), null);
            newProjectile.Velocity = owner.GameManager.Player.transform.position - owner.transform.position;
            newProjectile.parent = owner.gameObject;

            owner.shootParticles.Play();

            Debug.Log("Shoot!");
        }

        public override void Exit()
        {

        }

        public override void Update()
        {
            if (playerCheckTimer <= 0)
            {
                switch(GetOwner<GunnerBehaviour>().CheckForPlayer())
                {
                    case 0:
                        SetState<GunnerIdleState>();
                        break;
                    case 1:
                    case 2:
                        SetState<GunnerMoveState>();
                        break;
                }
                playerCheckTimer = 1.5f;
            }

            playerCheckTimer -= Time.deltaTime;
        }
    }
}
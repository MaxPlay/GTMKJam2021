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

        [SerializeField]
        private float attackCooldown = 5;

        [SerializeField]
        private float attackRange = 1;

        [SerializeField]
        private int damagePower = 5;

        public Vector3? TargetPosition { get; private set; }

        float attackCooldownChecker;

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

        public override void Update()
        {
            base.Update();
            Animator.SetFloat("MoveSpeed", NavMeshAgent.velocity.magnitude);
            attackCooldownChecker -= Time.deltaTime;
            if(attackCooldownChecker < 0 && (GameManager.Player.transform.position - transform.position).sqrMagnitude < Mathf.Pow(attackRange, 2))
            {
                attackCooldownChecker = attackCooldown;
                GameManager.Player.Damage(damagePower);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}

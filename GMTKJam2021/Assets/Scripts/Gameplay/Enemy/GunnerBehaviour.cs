using GMTK2021.Gameplay.Enemy;
using GMTK2021.Gameplay.Enemy.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerBehaviour : EnemyBehavior
{
    [SerializeField]
    private float distanceToPlayer;

    [SerializeField]
    private float shootDistance;

    [SerializeField]
    Transform shootPosition;
    public Transform ShootPosition { get => shootPosition; }

    public ProjectileBehaviour projectile;

    public ParticleSystem shootParticles;

    public Vector3? TargetPosition { get; private set; }

    protected override void Initialize()
    {
        AddState<GunnerIdleState>();
        AddState<GunnerMoveState>();
        AddState<GunnerShotState>();
    }

    public int CheckForPlayer()
    {
        if (GameManager.Player)
        {
            Vector3 playerLocation = GameManager.Player.transform.position;
            if (Physics.Raycast(transform.position, playerLocation - transform.position, out RaycastHit hitInfo, distanceToPlayer))
            {
                if (hitInfo.transform.CompareTag("Player"))
                {
                    TargetPosition = hitInfo.point;
                    if ((transform.position - playerLocation).sqrMagnitude < Mathf.Pow(shootDistance, 2) &&
                        Vector3.Dot(transform.forward, (playerLocation - transform.position).normalized) > 0.9f)
                        return 2;
                    return 1;
                }
            }
            if (NavMeshAgent.isStopped)
                TargetPosition = null;
            return 0;
        }
        return 0;
    }

    public override void Update()
    {
        base.Update();
        Animator.SetFloat("MoveSpeed", NavMeshAgent.velocity.magnitude);
    }
}

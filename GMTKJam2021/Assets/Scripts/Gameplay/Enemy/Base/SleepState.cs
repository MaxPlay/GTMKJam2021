namespace GMTK2021.Gameplay.Enemy.Base
{
    public class SleepState : EnemyState
    {
        public override void Enter()
        {
            OwningBehavior.NavMeshAgent.isStopped = true;
        }

        public override void Exit() { }
        public override void Update() { }
    }
}

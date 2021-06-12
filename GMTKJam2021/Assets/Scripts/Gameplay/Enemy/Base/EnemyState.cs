using UnityEngine;

namespace GMTK2021.Gameplay.Enemy.Base
{
    public abstract class EnemyState
    {
        public EnemyBehavior OwningBehavior { get; set; }

        public Transform Transform => OwningBehavior.transform;

        public GameObject GameObject => OwningBehavior.gameObject;

        public T GetComponent<T>() where T : Component => OwningBehavior.GetComponent<T>();
        public T GetComponentInChildren<T>() where T : Component => OwningBehavior.GetComponentInChildren<T>();

        public T GetOwner<T>() where T : EnemyBehavior => OwningBehavior as T;

        public void SetState<T>() where T : EnemyState => OwningBehavior.SetState<T>();

        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
    }
}
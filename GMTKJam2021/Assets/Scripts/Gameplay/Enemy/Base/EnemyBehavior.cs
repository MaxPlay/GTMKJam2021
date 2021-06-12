using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GMTK2021.Gameplay.Enemy.Base
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class EnemyBehavior : MonoBehaviour
    {
        private Dictionary<Type, EnemyState> states = new Dictionary<Type, EnemyState>();

        private EnemyState currentState = null;

        public GameManager GameManager { get; set; }

        public NavMeshAgent NavMeshAgent { get; private set; }

        protected virtual void Start()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>();
            // This is for objects that are placed in the scene. Make sure that "GameManager" is the first in the scene, so this is no overhead
            if (GameManager == null)
                GameManager = FindObjectOfType<GameManager>();
            Initialize();
        }

        protected abstract void Initialize();

        public void AddState<T>() where T : EnemyState, new()
        {
            if (!states.ContainsKey(typeof(T)))
            {
                EnemyState enemyState = new T
                {
                    OwningBehavior = this
                };
                states.Add(typeof(T), enemyState);
                if (currentState == null)
                    SetState<T>();
            }
        }

        public void SetState<T>() where T : EnemyState
        {
            if (states.TryGetValue(typeof(T), out EnemyState enemyState) && currentState != enemyState)
            {
                currentState?.Exit();
                currentState = enemyState;
                currentState.Enter();
            }
        }

        public void Update()
        {
            currentState?.Update();
        }
    }
}

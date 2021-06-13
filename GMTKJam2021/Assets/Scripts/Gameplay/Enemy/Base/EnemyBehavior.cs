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

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private ParticleSystem burningEffect;

        [SerializeField]
        private GameObject explosionPrefab;

        public Animator Animator { get => animator; }

        [SerializeField]
        bool debugCurrentState = false;

        [SerializeField]
        float fireExtinguishCooldown;

        [SerializeField]
        float inFireDamageCooldown;

        [SerializeField]
        float onFireDamageCooldown;

        bool isInFire;
        bool isOnFire;
        float onFireTimer;
        float onFireDamageTimer;
        float inFireDamageTimer;

        [SerializeField]
        int maxHealth;

        int currentHealth;

        protected virtual void Start()
        {
            currentHealth = maxHealth;
            StopParticles();
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

        private void OnParticleTrigger()
        {
            Debug.Log("FIRE!");
        }

        public virtual void Update()
        {
            if (debugCurrentState)
                Debug.Log(gameObject.name + "'s State is: " + currentState.GetType().ToString());
            currentState?.Update();
        }

        private void FixedUpdate()
        {
            if (isOnFire && !isInFire && onFireTimer + fireExtinguishCooldown > Time.time)
            {
                if(onFireDamageTimer + onFireDamageCooldown < Time.time)
                {
                    onFireDamageTimer = Time.time;
                    currentHealth--;
                }
            }
            else if (isOnFire && !isInFire && onFireTimer + fireExtinguishCooldown <= Time.time)
            {
                isOnFire = false;
                StopParticles();
            }
            else if (isInFire)
            {
                onFireDamageTimer = Time.time;
                onFireTimer = Time.time;
                if (inFireDamageTimer + inFireDamageCooldown < Time.time)
                {
                    inFireDamageTimer = Time.time;
                    currentHealth--;
                }
            }
            CheckHealth();
            isInFire = false;
        }

        private void CheckHealth()
        {
            if (currentHealth <= 0)
            {
                if(explosionPrefab)
                    Instantiate(explosionPrefab, transform.position, transform.rotation, null);
                Destroy(gameObject);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if(other.CompareTag("Fire"))
            {
                isInFire = true;
                isOnFire = true;
                StartParticles();
            }
        }

        private void StartParticles()
        {
            var emission = burningEffect.emission;
            emission.enabled = true;
        }

        private void StopParticles()
        {
            var emission = burningEffect.emission;
            emission.enabled = false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GMTK2021.Gameplay
{
    public class FlamethrowerBehavior : MonoBehaviour
    {
        [SerializeField]
        private float fuelConsumptionPerSecond;

        [SerializeField]
        Transform lookatTarget;

        [SerializeField]
        ProjectileBehaviour damageTriggerPrefab;

        [SerializeField]
        float spawnTriggerCooldown = 0.5f;
        float spawnTriggerTimer = -9999;

        [SerializeField]
        float fireSpeed;

        private float fuelTimer;
        private bool activeLastFrame;

        [SerializeField]
        private ParticleSystem ParticleSystem;

        private void Start()
        {
            StopParticles();
        }

        private void StopParticles()
        {
            var emission = ParticleSystem.emission;
            emission.enabled = false;
        }

        public int Activate()
        {
            activeLastFrame = true;
            if (fuelTimer <= 0)
            {
                fuelTimer += fuelConsumptionPerSecond;
                return 1;
            }
            return 0;
        }

        private void Update()
        {
            if (activeLastFrame && !ParticleSystem.emission.enabled)
                StartParticles();
            if (!activeLastFrame && ParticleSystem.emission.enabled)
                StopParticles();

            if (ParticleSystem.emission.enabled && Time.time > spawnTriggerTimer + spawnTriggerCooldown)
            {
                spawnTriggerTimer = Time.time;
                ProjectileBehaviour newProjectile = Instantiate(damageTriggerPrefab, transform.position, transform.rotation, null);
                newProjectile.Velocity = (lookatTarget.position - transform.position).normalized * fireSpeed;
            }

            activeLastFrame = false;

            if (fuelTimer > 0)
                fuelTimer -= Time.deltaTime;

            ParticleSystem.transform.LookAt(lookatTarget.position);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(lookatTarget.position, 0.2f);
        }

        private void StartParticles()
        {
            var emission = ParticleSystem.emission;
            emission.enabled = true;
        }
    }
}

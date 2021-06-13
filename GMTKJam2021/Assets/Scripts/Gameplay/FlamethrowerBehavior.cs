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

            activeLastFrame = false;

            if (fuelTimer > 0)
                fuelTimer -= Time.deltaTime;

            ParticleSystem.transform.LookAt(lookatTarget.position);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
        }

        private void StartParticles()
        {
            var emission = ParticleSystem.emission;
            emission.enabled = true;
        }
    }
}

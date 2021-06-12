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
        private ParticleSystem flameThrowerParticlesPrefab;

        [SerializeField]
        private float fuelConsumptionPerSecond;

        private float fuelTimer;
        private bool activeLastFrame;

        private ParticleSystem currentParticleSystem;

        private void Start()
        {
            currentParticleSystem = Instantiate(flameThrowerParticlesPrefab, transform.position, transform.rotation, transform);
            StopParticles();
        }

        private void StopParticles()
        {
            var emission = currentParticleSystem.emission;
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
            if (activeLastFrame && !currentParticleSystem.emission.enabled)
                StartParticles();
            if (!activeLastFrame && currentParticleSystem.emission.enabled)
                StopParticles();

            activeLastFrame = false;

            if (fuelTimer > 0)
                fuelTimer -= Time.deltaTime;
        }

        private void StartParticles()
        {
            var emission = currentParticleSystem.emission;
            emission.enabled = true;
        }
    }
}

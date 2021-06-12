using System;
using UnityEngine;

namespace GMTK2021.Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerBehavior : MonoBehaviour
    {
        public GameManager GameManager { get; set; }

        private JoinedValues healthFuelValues;
        private Vector2 currentSpeed;

        private new Rigidbody rigidbody;
        private Transform meshRoot;

        private FlamethrowerBehavior flamethrower;

        [SerializeField]
        private Animator animator;

        public int Health => healthFuelValues.A;

        public int Fuel => healthFuelValues.B;

        public void Fire()
        {
            if (Fuel > 0)
            {
                healthFuelValues.AddToB(-flamethrower.Activate());
            }
        }

        public void Damage(int damage)
        {
            healthFuelValues.AddToA(-damage);
            HealthCheck();
        }

        private void HealthCheck()
        {
            if (Health <= 0)
                Debug.Log("Dieded!");
        }

        public void Heal(int amount)
        {
            healthFuelValues.AddToA(amount);
        }

        public void Move(Vector2 speed)
        {
            currentSpeed += speed;
        }

        internal void ShiftToHealth()
        {
            healthFuelValues.ShiftToA(1);
        }

        internal void ShiftToFuel()
        {
            healthFuelValues.ShiftToB(1);
            HealthCheck();
        }

        private void FixedUpdate()
        {
            rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, transform.right * currentSpeed.x + transform.forward * currentSpeed.y, 0.5f);
            currentSpeed = Vector2.zero;
            animator.SetFloat("MoveSpeed", rigidbody.velocity.magnitude);
            animator.SetBool("Backwards", Vector3.Dot(rigidbody.velocity, meshRoot.rotation.eulerAngles) < 0);
        }

        public void LookAt(Vector3 location)
        {
            Vector3 myPosition = meshRoot.position;
            myPosition.y = 0;
            Vector3 theirPosition = location;
            theirPosition.y = 0;
            meshRoot.rotation = Quaternion.LookRotation(theirPosition - myPosition, Vector3.up);
        }

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            meshRoot = transform.GetChild(0);
            healthFuelValues = new JoinedValues(200, 50, 50);
            flamethrower = GetComponentInChildren<FlamethrowerBehavior>();
            Debug.Assert(flamethrower);
        }
    }
}
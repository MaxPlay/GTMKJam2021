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

        public int Health => healthFuelValues.A;

        public int Fuel => healthFuelValues.B;

        public void Fire()
        {
            Debug.DrawRay(transform.position, transform.forward * 5, Color.red);
        }

        public void Damage(int damage)
        {
            healthFuelValues.AddToA(-damage);
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

        private void FixedUpdate()
        {
            rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, transform.right * currentSpeed.x + transform.forward * currentSpeed.y, 0.5f);
            currentSpeed = Vector2.zero;
        }

        public void LookAt(Vector3 location)
        {
            // TODO(ms): Implement better look at logic for that, the unity one doesn't do it
        }

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            meshRoot = transform.GetChild(0);
        }
    }
}
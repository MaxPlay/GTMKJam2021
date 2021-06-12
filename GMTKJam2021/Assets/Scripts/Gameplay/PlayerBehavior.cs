﻿using UnityEngine;

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

        [SerializeField]
        private Transform stomachTransform;

        [SerializeField]
        private float speed = 1;

        Vector3 theirPosition = Vector3.zero;

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

        public void Move(Vector2 moveSpeed)
        {
            currentSpeed += moveSpeed * speed;
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

            Vector3 myPosition = meshRoot.position;
            if (rigidbody.velocity.magnitude < 0.1f)
            {
                meshRoot.transform.rotation = Quaternion.LookRotation(theirPosition - myPosition, Vector3.up);
                stomachTransform.rotation = Quaternion.LookRotation(myPosition - theirPosition, Vector3.up);
                animator.SetBool("Backwards", false);
            }
            else
            {
                Quaternion targetWalkRotation;
                Quaternion targetLookRotation;
                Vector3 stomachDirection = myPosition - theirPosition;
                stomachDirection.y = 0;
                Vector3 meshRootDirection = rigidbody.velocity;
                meshRootDirection.y = 0;
                animator.SetBool("Backwards", Vector3.Dot(meshRootDirection, -stomachDirection) < 0);
                if (Vector3.Dot(meshRootDirection, -stomachDirection) < 0)
                {
                    targetWalkRotation = Quaternion.LookRotation(-rigidbody.velocity, Vector3.up);
                    targetLookRotation = Quaternion.LookRotation(myPosition - theirPosition, Vector3.up);
                }
                else
                {
                    targetWalkRotation = Quaternion.LookRotation(rigidbody.velocity, Vector3.up);
                    targetLookRotation = Quaternion.LookRotation(myPosition - theirPosition, Vector3.up);
                }
                meshRoot.transform.rotation = Quaternion.Lerp(targetWalkRotation, meshRoot.transform.rotation, 0.05f);
                stomachTransform.rotation = targetLookRotation;
            }
            currentSpeed = Vector2.zero;
            animator.SetFloat("MoveSpeed", rigidbody.velocity.magnitude);
        }

        public void LookAt(Vector3 location)
        {
            Vector3 myPosition = meshRoot.position;
            myPosition.y = 0;
            Vector3 theirPosition = location;
            theirPosition.y = 0;
            this.theirPosition = theirPosition;
        }

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            meshRoot = transform.GetChild(0);
            healthFuelValues = new JoinedValues(200, 50, 50);
            flamethrower = GetComponentInChildren<FlamethrowerBehavior>();
            Debug.Assert(flamethrower);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(stomachTransform.position, stomachTransform.position - stomachTransform.forward);
            if (meshRoot)
                Gizmos.DrawLine(meshRoot.position, meshRoot.position + meshRoot.forward);
        }
    }
}
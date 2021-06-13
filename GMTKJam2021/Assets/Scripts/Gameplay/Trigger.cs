using GMTK2021.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace GMTK2021.Gameplay
{
    public class Trigger : MonoBehaviour
    {
        [SerializeField]
        [TagSelector]
        private List<string> triggeredByTags;

        private HashSet<GameObject> gameObjectsInTrigger = new HashSet<GameObject>();

        [SerializeField]
        private bool triggerOnce;

        [SerializeField]
        private bool isEnabled = true;

        public bool Enabled
        {
            get => isEnabled;
            set => isEnabled = value;
        }

        public UnityEvent OnEnter;
        public UnityEvent OnExit;
        public UnityEvent OnExitAll;

        [SerializeField]
        private Color triggerColor = Color.blue;

        private void OnTriggerEnter(Collider other)
        {
            if (triggeredByTags.Count == 0 || triggeredByTags.Any(other.CompareTag))
            {
                if (triggerOnce && isEnabled)
                {
                    Collider[] colliders = GetComponents<Collider>();
                    for (int i = 0; i < colliders.Length; i++)
                    {
                        colliders[i].enabled = false;
                    }
                    TriggerEnter();
                    TriggerExit();
                    return;
                }

                if (!gameObjectsInTrigger.Contains(other.gameObject))
                {
                    gameObjectsInTrigger.Add(other.gameObject);
                    TriggerEnter();
                }
            }
        }

        private void TriggerEnter()
        {
            if (isEnabled)
            {
                Debug.Log($"Trigger Enter: {gameObject.name}", gameObject);
                OnEnter.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (triggerOnce)
                return;

            if (triggeredByTags.Count == 0 || triggeredByTags.Any(other.CompareTag))
            {
                if (gameObjectsInTrigger.Contains(other.gameObject))
                {
                    gameObjectsInTrigger.Remove(other.gameObject);
                }
                TriggerExit();

                if (gameObjectsInTrigger.Count == 0)
                    TriggerExitAll();
            }
        }

        private void TriggerExit()
        {
            if (isEnabled)
            {
                Debug.Log($"Trigger Exit: {gameObject.name}", gameObject);
                OnExit.Invoke();
            }
        }

        private void TriggerExitAll()
        {
            if (isEnabled)
            {
                Debug.Log($"Trigger Exit All: {gameObject.name}", gameObject);
                OnExitAll.Invoke();
            }
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = triggerColor;
            var boxCollider = GetComponent<BoxCollider>();
            if (boxCollider != null)
                Gizmos.DrawWireCube(boxCollider.center + transform.position, boxCollider.size);

            var sphereCollider = GetComponent<SphereCollider>();
            if (sphereCollider != null)
                Gizmos.DrawWireSphere(sphereCollider.center + transform.position, sphereCollider.radius);

            Gizmos.DrawIcon(transform.position, "Trigger.png", true, triggerColor);
        }
    }
}

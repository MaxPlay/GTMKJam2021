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

        public UnityEvent OnEnter;
        public UnityEvent OnExit;

        private void OnTriggerEnter(Collider other)
        {
            if (triggeredByTags.Count == 0 || triggeredByTags.Any(other.CompareTag))
            {
                if (!gameObjectsInTrigger.Contains(other.gameObject))
                {
                    gameObjectsInTrigger.Add(other.gameObject);
                    OnEnter.Invoke();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (triggeredByTags.Count == 0 || triggeredByTags.Any(other.CompareTag))
            {
                if (gameObjectsInTrigger.Contains(other.gameObject))
                {
                    gameObjectsInTrigger.Remove(other.gameObject);
                    OnExit.Invoke();
                }
            }
        }
    }
}

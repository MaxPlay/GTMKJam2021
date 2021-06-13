using UnityEngine;

namespace GMTK2021.Gameplay
{
    [RequireComponent(typeof(Animator))]
    public class Door : MonoBehaviour
    {
        private Animator animator;

        [SerializeField]
        private bool isOpen;

        public bool IsOpen
        {
            get => isOpen;
            set
            {
                isOpen = value;
                TriggerAnimation();
            }
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
            TriggerAnimation();
        }

        private void TriggerAnimation() => animator.SetBool("Open", isOpen);
    }
}
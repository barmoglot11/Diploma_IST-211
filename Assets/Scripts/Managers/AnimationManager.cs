using System;
using UnityEngine;

namespace MANAGER
{
    public class AnimationManager : MonoBehaviour
    {
        public static AnimationManager Instance;

        public Animator animator;
        
        [Header("Переменные с анимациями")]
        public bool IsWalking;
        public bool IsRunning;
        public bool IsSniping;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            animator.SetBool("IsWalking", IsWalking);
            animator.SetBool("IsRunning", IsRunning);
            animator.SetBool("IsSniping", IsSniping);
        }
    }
}
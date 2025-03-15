using System;
using UnityEngine;

namespace INVESTIGATION
{
    public class InvestigationManager : MonoBehaviour
    {
        [SerializeField]
        private InputReader input;
        public static InvestigationManager Instance;

        public bool Investigating = false;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Initialize();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Initialize()
        {
            Instance = this;
            input.InvestigationEvent += InvestChange;
        }

        private void InvestChange() => Investigating = !Investigating;
    }
}
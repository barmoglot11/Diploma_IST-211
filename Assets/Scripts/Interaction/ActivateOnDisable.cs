using System;
using UnityEngine;
using UnityEngine.Events;

namespace Interaction
{
    public class ActivateOnDisable : MonoBehaviour
    {
        public UnityEvent OnDeactivate;
        private void OnDisable()
        { 
            OnDeactivate?.Invoke();
        }
    }
}
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Interaction
{
    public class ActivateOnEnable : MonoBehaviour
    {
        public UnityEvent OnActivate;
        private void OnEnable()
        {
            OnActivate?.Invoke();
        }
    }
}
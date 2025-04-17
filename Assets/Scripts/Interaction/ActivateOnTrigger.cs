using System;
using UnityEngine;
using UnityEngine.Events;

namespace Interaction
{
    public class ActivateOnTrigger : MonoBehaviour
    {
        public UnityEvent onActivate;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            onActivate?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
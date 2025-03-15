using System;
using UnityEngine;
using UnityEngine.Events;

namespace Interaction
{
    [RequireComponent(typeof(Collider))]
    public class DialogueInvoker : MonoBehaviour
    {
        public UnityEvent OnInteract;
        private void OnTriggerEnter(Collider other)
        {
            OnInteract?.Invoke();
        }
    }
}
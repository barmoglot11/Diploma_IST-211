using System;
using CHARACTER;
using INVESTIGATION;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Interaction
{
    [RequireComponent(typeof(BoxCollider))]
    public class InvestigationDialogueTrigger : MonoBehaviour
    {
        public UnityEvent OnTrigger;
        private bool Investigating => InvestigationManager.Instance.IsInvestigating;

        private void OnTriggerStay(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            if (!Investigating) return;
            OnTrigger?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
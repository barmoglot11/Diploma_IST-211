using System;
using UnityEngine;

namespace INVESTIGATION
{
    [RequireComponent(typeof(Collider))]
    public class InvestigationZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            InvestigationManager.Instance.isInvestigationAvailable = true;
        }

        private void OnTriggerExit(Collider other)
        {
            InvestigationManager.Instance.isInvestigationAvailable = false;
        }
    }
}
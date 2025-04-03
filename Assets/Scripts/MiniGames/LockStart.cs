using System;
using UnityEngine;
using UnityEngine.Events;

namespace LOCKPICKING
{
    public class LockStart : MonoBehaviour
    {
        public UnityEvent UnlockEvent;
        
        public void StartMinigame()
        {
            LockpickingManager.Instance.Picking();
            LockpickingManager.Instance.Lock.Unlocked = UnlockEvent;
        }
    }
}
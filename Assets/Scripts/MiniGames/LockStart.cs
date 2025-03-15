using System;
using UnityEngine;
using UnityEngine.Events;

namespace LOCKPICKING
{
    public class LockStart : MonoBehaviour
    {
        public GameObject canvas;
        
        
        public void StartMinigame()
        {
            canvas.SetActive(true);
            LockpickingManager.Instance.Picking();
            LockpickingManager.Instance.SetUnlockEvent(UnlockedLock);
        }

        public void UnlockedLock()
        {
            canvas.SetActive(false);
            InputManager.Instance.EnableCharMoveAndCamera();
            
        }
    }
}
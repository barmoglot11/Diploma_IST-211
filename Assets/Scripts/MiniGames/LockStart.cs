using UnityEngine;
using UnityEngine.Events;

namespace LOCKPICKING
{
    public class LockStart : MonoBehaviour
    {
        [Header("Lock Events")]
        [SerializeField] private UnityEvent _onUnlock;
        
        public void StartMinigame()
        {
            if (!ValidateDependencies())
            {
                Debug.LogError("LockStart: Required dependencies are missing!");
                return;
            }

            InputManager.Instance.ChangeInputStatus(InputStatus.Lock);
            
            var lockpickingManager = LockpickingManager.Instance;
            lockpickingManager.StartLockpicking();
            lockpickingManager.Lock.OnUnlocked = _onUnlock;
        }

        private bool ValidateDependencies()
        {
            if (InputManager.Instance == null)
            {
                Debug.LogError("InputManager instance is not available");
                return false;
            }

            if (LockpickingManager.Instance == null)
            {
                Debug.LogError("LockpickingManager instance is not available");
                return false;
            }

            if (LockpickingManager.Instance.Lock == null)
            {
                Debug.LogError("Lock reference in LockpickingManager is not set");
                return false;
            }

            return true;
        }
    }
}
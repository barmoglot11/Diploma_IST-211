using UnityEngine;
using UnityEngine.Events;

namespace LOCKPICKING
{
    public class LockpickingManager : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Camera _lockpickingCamera;
        [SerializeField] private Lock _lock;
        [SerializeField] private Pick _pick;

        public LockDifficulty Difficulty { get; set; }
        
        public Lock Lock => _lock;
        public static LockpickingManager Instance { get; private set; }

        private void Awake()
        {
            InitializeSingleton();
        }

        private void InitializeSingleton()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void SetDifficulty(LockDifficulty diff) => Difficulty = diff;
        
        public void SetUnlockEvent(UnityAction action)
        {
            if (_lock != null)
            {
                _lock.OnUnlocked.AddListener(action);
            }
            else
            {
                Debug.LogError("Lock reference is not set in LockpickingManager", this);
            }
        }
        
        public void StartLockpicking()
        {
            if (!ValidateDependencies())
            {
                Debug.LogError("Cannot start lockpicking - dependencies are not properly set", this);
                return;
            }

            if(!InventoryManager.Instance.HasItem("Отмычка")) return;
            
            _lock.SetLockDiff(Difficulty);
            
            _pick.Initialize(_lockpickingCamera, _lock);
            _lock.Initialize(_pick);
        }

        private bool ValidateDependencies()
        {
            bool isValid = true;

            if (_lockpickingCamera == null)
            {
                Debug.LogError("Lockpicking camera is not assigned", this);
                isValid = false;
            }

            if (_lock == null)
            {
                Debug.LogError("Lock is not assigned", this);
                isValid = false;
            }

            if (_pick == null)
            {
                Debug.LogError("Pick is not assigned", this);
                isValid = false;
            }

            return isValid;
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }
    }
}
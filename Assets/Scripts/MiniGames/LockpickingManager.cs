using System;
using UnityEngine;
using UnityEngine.Events;

namespace LOCKPICKING
{
    public class LockpickingManager : MonoBehaviour
    {
        [SerializeField]
        private Camera _camera;
        [SerializeField]
        private Lock _lock;
        [SerializeField]
        private Pick _pick;

        public static LockpickingManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SetUnlockEvent(UnityAction action)
        {
            _lock.Unlocked.AddListener(action);
        }
        
        public void Picking()
        {
            _pick.Init(_camera, _lock);
            _lock.Init(_pick);
        }
    }
}
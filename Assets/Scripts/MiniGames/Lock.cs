using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LOCKPICKING
{
    public class Lock : MonoBehaviour
    {
        [SerializeField] private Transform _inner;
        [SerializeField] private Transform _pickAnchor;
        [SerializeField] private float _rotationSpeed = 10;
        [SerializeField] private float _maxRotationAngle = 90;
    
        [SerializeField, Range(1, 25)] 
        private float _lockRange = 10;
    
        private float _unlockAngle;
        private Vector2 _unlockRange;
        private Camera _camera;
        private Pick _pick;
    
        public float InnerRotation => _inner.eulerAngles.z;
        public Vector3 PickAnchorPosition => _pickAnchor.position;
        public float MaxRotationAngle => _maxRotationAngle;
    
        public event Action Unlocked;
    
        private void OnEnable()
        {
            Unlocked += GenerateUnlockAngle;
        }
    
        private void OnDisable()
        {
            Unlocked -= GenerateUnlockAngle;
        }
    
        public void Init(Pick pick)
        {
            _pick = pick;
            GenerateUnlockAngle();
        }
    
        private void GenerateUnlockAngle()
        {
            _unlockAngle = Random.Range(-_maxRotationAngle + _lockRange, _maxRotationAngle - _lockRange);
            _unlockRange = new Vector2(_unlockAngle - _lockRange, _unlockAngle + _lockRange);
        }
        
        private void Update()
        {
            if (_pick == null)
                return;
    
            var rotationCoefficient = _pick.IsMoving ? 0 : 1;
    
            var percentage = Mathf.Round(100 - Mathf.Abs((_pick.Angle - _unlockAngle) / 100 * 100));
            var maxRotation = (percentage / 100) * _maxRotationAngle;
            var lockRotation = maxRotation * rotationCoefficient;
    
            var lockLerp = Mathf.LerpAngle(_inner.eulerAngles.z, lockRotation, Time.deltaTime * _rotationSpeed);
            _inner.eulerAngles = new Vector3(0, 0, lockLerp);
    
            if (lockLerp >= maxRotation -1)
            {
                if (_pick.Angle < _unlockRange.y && _pick.Angle > _unlockRange.x)
                {
                    Debug.Log("Unlocked!");
                    Unlocked?.Invoke();
                }
            }
        }
    }
}

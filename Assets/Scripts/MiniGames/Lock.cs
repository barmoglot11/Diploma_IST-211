using UnityEngine;
using UnityEngine.Events;

public enum LockDifficulty
{
    Easy = 25,
    Normal = 20,
    Hard = 15,
    Expert = 10
    
}

namespace LOCKPICKING
{
    [RequireComponent(typeof(Transform))]
    public class Lock : MonoBehaviour
    {
        [Header("Lock Settings")]
        [SerializeField] private Transform _innerLock;
        [SerializeField] private Transform _pickAnchor;
        [SerializeField, Min(0.1f)] private float _rotationSpeed = 10f;
        [SerializeField, Range(1f, 180f)] private float _maxRotationAngle = 90f;
        [SerializeField, Range(1f, 25f)] private float _lockRange;
        public LockDifficulty Difficulty { get; private set; }
        private float _unlockAngle;
        private Vector2 _unlockRange;
        private Pick _currentPick;
        
        public float NormalizedInnerRotation => _innerLock.localEulerAngles.z / _maxRotationAngle;
        public Vector3 PickAnchorWorldPosition => _pickAnchor.position;
        public float CurrentMaxRotationAngle => _maxRotationAngle;
        public bool IsPickEngaged => _currentPick != null;

        public UnityEvent OnUnlocked;

        public void SetLockDiff(LockDifficulty diff) => Difficulty = diff;
        
        public void Initialize(Pick pick)
        {
            _currentPick = pick;
            _lockRange = (int)Difficulty;
            GenerateUnlockParameters();
        }

        private void GenerateUnlockParameters()
        {
            float safeRange = _maxRotationAngle - _lockRange;
            _unlockAngle = Random.Range(-safeRange, safeRange);
            _unlockRange = new Vector2(
                _unlockAngle - _lockRange, 
                _unlockAngle + _lockRange
            );
        }

        private void Update()
        {
            if (!IsPickEngaged) return;

            UpdateLockRotation();
            CheckUnlockCondition();
        }

        private void UpdateLockRotation()
        {
            float rotationCoefficient = _currentPick.IsMoving ? 0f : 1f;
            float unlockProgress = CalculateUnlockProgress();
            float targetRotation = CalculateTargetRotation(unlockProgress, rotationCoefficient);
            
            ApplyRotation(targetRotation);
        }

        private float CalculateUnlockProgress()
        {
            float angleDifference = Mathf.Abs(_currentPick.Angle - _unlockAngle);
            return 1f - Mathf.Clamp01(angleDifference / 100f);
        }

        private float CalculateTargetRotation(float progress, float coefficient)
        {
            float rotationLimit = progress * _maxRotationAngle;
            return rotationLimit * coefficient;
        }

        private void ApplyRotation(float targetRotation)
        {
            float currentAngle = _innerLock.localEulerAngles.z;
            float smoothedRotation = Mathf.LerpAngle(
                currentAngle, 
                targetRotation, 
                Time.deltaTime * _rotationSpeed
            );
            
            _innerLock.localEulerAngles = new Vector3(0f, 0f, smoothedRotation);
        }

        private void CheckUnlockCondition()
        {
            float currentAngle = _innerLock.localEulerAngles.z;
            float targetLimit = CalculateTargetRotation(1f, 1f) - 1f;

            if (currentAngle >= targetLimit && IsPickInUnlockRange())
            {
                Debug.Log("Lock unlocked successfully!");
                OnUnlocked?.Invoke();
            }
        }

        private bool IsPickInUnlockRange()
        {
            return _currentPick.Angle > _unlockRange.x && 
                   _currentPick.Angle < _unlockRange.y;
        }

        private void OnValidate()
        {
            _lockRange = Mathf.Min(_lockRange, _maxRotationAngle * 0.9f);
        }
    }
}
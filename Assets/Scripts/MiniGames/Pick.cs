using UnityEngine;

namespace LOCKPICKING
{
    [RequireComponent(typeof(AudioSource))]
    public class Pick : MonoBehaviour
    {
        [Header("Pick Settings")]
        [SerializeField] private Vector3 _restPosition;
        
        private Camera _mainCamera;
        private Lock _currentLock;
        private float _currentAngle;
        private bool _isMoving = true;
        private AudioSource _audioSource;

        public bool IsMoving => _isMoving;
        public float Angle => _currentAngle;

        public bool IsInitialized = false;
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void Initialize(Camera camera, Lock lockTarget)
        {
            _mainCamera = camera;
            _currentLock = lockTarget;
            transform.position = _restPosition;
            
            if (_currentLock != null)
            {
            }
            
            IsInitialized = true;
        }

        private void Update()
        {
            if (!IsInitialized) return;
            UpdatePosition();
            
            if (_isMoving)
            {
                HandlePickMovement();
                PlayPickSound();
            }

            HandleMovementToggle();
        }

        private void UpdatePosition()
        {
            if (_currentLock != null)
            {
                transform.position = _currentLock.PickAnchorWorldPosition;
            }
        }

        private void HandlePickMovement()
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            
            _currentAngle += (-mouseX * 20f - mouseY * 15f) * Time.deltaTime;
            _currentAngle = Mathf.Clamp(_currentAngle, -_currentLock.CurrentMaxRotationAngle, _currentLock.CurrentMaxRotationAngle);

            Quaternion targetRotation = Quaternion.AngleAxis(_currentAngle, Vector3.forward);
            transform.localRotation = targetRotation;
        }

        private void PlayPickSound()
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
        }

        private void HandleMovementToggle()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                _isMoving = false;
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                _isMoving = true;
            }
        }

        private void ResetPickMovement()
        {
            _isMoving = true;
        }
    }
}
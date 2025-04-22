using UnityEngine;

namespace Interaction
{
    [RequireComponent(typeof(Animator), typeof(AudioSource))]
    [DisallowMultipleComponent]
    public class DoorOpener : MonoBehaviour
    {
        [Header("Animation Settings")]
        [SerializeField] private string _openTrigger = "OpenDoor";
        [SerializeField] private string _closeTrigger = "CloseDoor";
        
        [Header("Audio Settings")]
        [SerializeField] private AudioClip _doorSound;
        [SerializeField] [Range(0, 1)] private float _volume = 1f;
        
        [Header("State")]
        [SerializeField] private bool _isOpen = false;
        [SerializeField] private bool _isLocked = false;
        
        [Header("Debug")]
        [SerializeField] private bool _debugLog = true;

        private Animator _cachedAnimator;
        private AudioSource _cachedAudioSource;
        private int _openTriggerHash;
        private int _closeTriggerHash;

        private void Awake()
        {
            CacheComponents();
            HashParameters();
            ValidateParameters();
        }

        private void CacheComponents()
        {
            _cachedAnimator = GetComponent<Animator>();
            _cachedAudioSource = GetComponent<AudioSource>();
            _cachedAudioSource.playOnAwake = false;
            _cachedAudioSource.volume = _volume;
        }

        private void HashParameters()
        {
            _openTriggerHash = Animator.StringToHash(_openTrigger);
            _closeTriggerHash = Animator.StringToHash(_closeTrigger);
        }

        private void ValidateParameters()
        {
            if (_cachedAnimator == null) return;

            bool hasOpenTrigger = false;
            bool hasCloseTrigger = false;

            foreach (var param in _cachedAnimator.parameters)
            {
                if (param.nameHash == _openTriggerHash) hasOpenTrigger = true;
                if (param.nameHash == _closeTriggerHash) hasCloseTrigger = true;
            }

            if (!hasOpenTrigger || !hasCloseTrigger)
            {
                Debug.LogError($"Missing animation triggers on {name}. " +
                    $"Required: {_openTrigger} and {_closeTrigger}", this);
            }
        }

        public void ToggleDoor()
        {
            if (_isLocked)
            {
                if (_debugLog) Debug.Log($"[Door] {name} is locked", this);
                return;
            }

            if (_isOpen) CloseDoor();
            else OpenDoor();
        }

        private void OpenDoor()
        {
            _cachedAnimator.SetTrigger(_openTriggerHash);
            PlaySound();
            _isOpen = true;
            if (_debugLog) Debug.Log($"[Door] {name} opened", this);
        }

        private void CloseDoor()
        {
            _cachedAnimator.SetTrigger(_closeTriggerHash);
            PlaySound();
            _isOpen = false;
            if (_debugLog) Debug.Log($"[Door] {name} closed", this);
        }

        public void Lock() 
        {
            _isLocked = true;
            if (_debugLog) Debug.Log($"[Door] {name} locked", this);
        }

        public void Unlock() 
        {
            _isLocked = false;
            if (_debugLog) Debug.Log($"[Door] {name} unlocked", this);
        }

        private void PlaySound()
        {
            if (_doorSound == null || _cachedAudioSource == null) return;
            _cachedAudioSource.PlayOneShot(_doorSound, _volume);
        }

        #if UNITY_EDITOR
        private void OnValidate()
        {
            if (_doorSound == null)
                Debug.LogWarning($"No sound assigned on {name}", this);
        }
        #endif
    }
}
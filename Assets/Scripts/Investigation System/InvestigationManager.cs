using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace INVESTIGATION
{
    public class InvestigationManager : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private AudioClip _investigationSound;
        [SerializeField] private ChangeSpriteOnLAlt EyeController;
        [SerializeField] private InteractiveObject Dialogue;
        [Header("Audio Settings")]
        [SerializeField] [Range(0f, 1f)] private float _maxVolume = 1f;
        [SerializeField] private float _fadeDuration = 0.5f;

        [Header("Debug")] 
        public bool isInvestigationAvailable = false;
        [SerializeField] private bool _debugLogs = true;

        public bool IsInvestigating { get; private set; } = false;
        public static InvestigationManager Instance { get; private set; }

        private AudioSource _audioSource;
        private Coroutine _fadeCoroutine;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            Initialize();
        }

        private void Initialize()
        {
            SetupAudioSource();
            RegisterInputEvents();
            
        }

        private void SetupAudioSource()
        {
            _audioSource = GetComponent<AudioSource>();
            if (_audioSource == null)
            {
                _audioSource = gameObject.AddComponent<AudioSource>();
            }

            _audioSource.clip = _investigationSound;
            _audioSource.loop = true;
            _audioSource.volume = _maxVolume;
        }

        private void RegisterInputEvents()
        {
            if (_inputReader != null)
            {
                _inputReader.InvestigationEvent += ToggleInvestigation;
            }
            else
            {
                Debug.LogError("InputReader reference is missing!", this);
            }
        }

        private void Update()
        {
            switch (IsInvestigating)
            {
                case true when EyeController.isDefaultSprite && EyeController.IsEndedChanging:
                case false when !EyeController.isDefaultSprite && EyeController.IsEndedChanging:
                    EyeController.ChangeSprite(IsInvestigating);
                    break;
            }
        }

        private void OnDestroy()
        {
            UnregisterInputEvents();
            
            if (Instance == this)
            {
                Instance = null;
            }
        }

        private void UnregisterInputEvents()
        {
            if (_inputReader != null)
            {
                _inputReader.InvestigationEvent -= ToggleInvestigation;
            }
        }

        public void ToggleInvestigation()
        {
            if(!isInvestigationAvailable) return;
            IsInvestigating = !IsInvestigating;
            UpdateInvestigationState();
            EyeController.StartChangeSprite();
            if (_debugLogs)
            {
                Debug.Log($"[Investigation] Mode {(IsInvestigating ? "activated" : "deactivated")}");
            }
        }

        private void UpdateInvestigationState()
        {
            if (IsInvestigating)
            {
                StartInvestigation();
            }
            else
            {
                StopInvestigation();
            }
        }

        private void StartInvestigation()
        {
            if (_audioSource.isPlaying) return;
            
            PostProcessingManager.Instance.SetProfileByIndexSmooth(1);
            
            if (_fadeCoroutine != null)
            {
                StopCoroutine(_fadeCoroutine);
            }
            _fadeCoroutine = StartCoroutine(FadeInSound());
        }

        private void StopInvestigation()
        {
            if (!_audioSource.isPlaying) return;
            
            PostProcessingManager.Instance.SetProfileByIndexSmooth(0);
            
            if (_fadeCoroutine != null)
            {
                StopCoroutine(_fadeCoroutine);
            }
            _fadeCoroutine = StartCoroutine(FadeOutSound());
        }

        private IEnumerator FadeInSound()
        {
            _audioSource.volume = 0f;
            _audioSource.Play();

            float elapsedTime = 0f;

            while (elapsedTime < _fadeDuration)
            {
                _audioSource.volume = Mathf.Lerp(0f, _maxVolume, elapsedTime / _fadeDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _audioSource.volume = _maxVolume;
            if(Dialogue != null)
                if(Dialogue.gameObject.activeSelf)
                    Dialogue?.Interact();
            _fadeCoroutine = null;
        }
        
        private IEnumerator FadeOutSound()
        {
            float startVolume = _audioSource.volume;
            float elapsedTime = 0f;

            while (elapsedTime < _fadeDuration)
            {
                _audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsedTime / _fadeDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _audioSource.Stop();
            _audioSource.volume = _maxVolume;
            _fadeCoroutine = null;
        }
    }
}
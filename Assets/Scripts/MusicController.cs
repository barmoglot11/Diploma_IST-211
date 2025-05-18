using System;
using System.Collections;
using UnityEngine;

namespace AUDIO
{
    public class MusicController : MonoBehaviour
    {
        public AudioSource audioSource1;
        public AudioSource audioSource2;
        public float fadeDuration = 2f;

        private AudioSource _currentSource;
        private AudioSource _otherSource;

        private void Awake()
        {
            OnValidate();
            Initialize();
        }

        private void Initialize()
        {
            _currentSource = audioSource1;
            _otherSource = audioSource2;
            
            audioSource1.loop = true;
            audioSource2.loop = true;
            
            audioSource1.Play();
        }

        public void CrossFadeTo(AudioClip newClip)
        {
            StartCoroutine(CrossFade(newClip));
        }

        private IEnumerator CrossFade(AudioClip newClip)
        {
            _otherSource.clip = newClip;
            _otherSource.volume = 0f;
            _otherSource.Play();
    
            float timer = 0f;
    
            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;
                float ratio = timer / fadeDuration;
        
                _currentSource.volume = Mathf.Lerp(1f, 0f, ratio);
                _otherSource.volume = Mathf.Lerp(0f, 1f, ratio);
        
                yield return null;
            }
    
            // Меняем источники местами
            (_currentSource, _otherSource) = (_otherSource, _currentSource);

            _otherSource.Stop();
        }

        private void OnValidate()
        {
            if(audioSource1 == null) Debug.LogError("AudioSource1  не назначен");
            if(audioSource2 == null) Debug.LogError("AudioSource2  не назначен");
        }
    }
}
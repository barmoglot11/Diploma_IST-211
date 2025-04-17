using System;
using System.Collections;
using UnityEngine;

namespace INVESTIGATION
{
    public class InvestigationManager : MonoBehaviour
    {
        [SerializeField]
        private InputReader input;
        public static InvestigationManager Instance;

        public bool Investigating = false;
        AudioSource source;
        public AudioClip audioClip;
        private void Awake()
        {
            if (Instance == null)
            {
                Initialize();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Initialize()
        {
            Instance = this;
            input.InvestigationEvent += InvestChange;
            source = GetComponent<AudioSource>();
            if (source == null)
            {
                source = gameObject.AddComponent<AudioSource>();
            }
            source.clip = audioClip;
            source.loop = true;
        }

        private void InvestChange() => Investigating = !Investigating;

        public void Update()
        {
            if (Investigating)
            {
                if(!source.isPlaying)
                    source.Play();
            }
            else
            {
                if (source.isPlaying)
                {
                    StartCoroutine(StopSound());
                }
            }
        }

        public IEnumerator StopSound()
        {
            if(source.volume > 0.1f)
                yield return source.volume - 0.2f;
            
            source.Stop();
            source.volume = 1f;
        }
    }
}
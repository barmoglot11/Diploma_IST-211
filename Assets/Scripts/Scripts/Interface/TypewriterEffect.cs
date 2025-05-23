using System;
using DIALOGUE;
using UnityEngine;
using UnityEngine.Audio;

public class TypewriterEffect : MonoBehaviour
{
    [Header("Settings")]
    public AudioClip typeSound; 
    public AudioMixer Mixer; 
    public GameObject canvasObject;
    private AudioSource audioSource;
    public float speed = 1.3f;
    public float differenceForPitch = 0.2f;

    private void Awake()
    {
        audioSource = canvasObject.gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = canvasObject.gameObject.AddComponent<AudioSource>();
            audioSource.clip = typeSound;
            audioSource.outputAudioMixerGroup = Mixer.FindMatchingGroups("UI")[0];
            audioSource.playOnAwake = false;
            audioSource.loop = true;
        }
    }

    private void Update()
    {
        if (DialogueSystem.instance.IsRunningDialogue && audioSource.gameObject.activeInHierarchy)
        {
            PlaySound();
        }
        else
        {
            StopSound();
        }
    }

    public void PlaySound()
    {
        audioSource.pitch = UnityEngine.Random.Range(speed - differenceForPitch, speed + differenceForPitch);
        if(audioSource.isPlaying) return;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void StopSound()
    {
        audioSource.loop = false;
    }
}
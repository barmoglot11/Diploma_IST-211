using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSoundEffects : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [Header("Звуковые эффекты кнопки")]
    [Tooltip("Звук при наведении на кнопку")]
    public AudioClip hoverSound; // Звук при наведении

    [Tooltip("Звук при нажатии на кнопку")]
    public AudioClip clickSound; // Звук при нажатии

    private AudioSource audioSource; // Компонент для воспроизведения звуков

    void Start()
    {
        // Получение компонента AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // Добавление AudioSource, если его нет
        }

        // Настройка AudioSource
        audioSource.playOnAwake = false; // Не воспроизводить звук при загрузке
        audioSource.loop = false; // Не зацикливать звук
        audioSource.outputAudioMixerGroup = Resources.Load<AudioMixer>("Audio").FindMatchingGroups("UI")[0];
    }

    // Метод для обработки наведения указателя на кнопку
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSound != null)
        {
            // Воспроизведение звука при наведении
            audioSource.PlayOneShot(hoverSound);
        }
    }

    // Метод для обработки нажатия на кнопку
    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickSound != null)
        {
            // Воспроизведение звука при нажатии
            audioSource.PlayOneShot(clickSound);
        }
    }
}
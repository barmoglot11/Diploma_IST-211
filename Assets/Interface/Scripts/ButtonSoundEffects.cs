using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSoundEffects : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [Header("Звуковые эффекты")]
    [Tooltip("Звук при наведении курсора")]
    public AudioClip hoverSound; // Звук при наведении

    [Tooltip("Звук при нажатии на кнопку")]
    public AudioClip clickSound; // Звук при нажатии

    private AudioSource audioSource; // Компонент для воспроизведения звуков

    void Start()
    {
        // Получаем или добавляем компонент AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Настраиваем AudioSource
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    // При наведении курсора
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSound != null)
        {
            // Воспроизводим звук при наведении
            audioSource.PlayOneShot(hoverSound);
        }
    }

    // При нажатии на кнопку
    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickSound != null)
        {
            // Воспроизводим звук при нажатии
            audioSource.PlayOneShot(clickSound);
        }
    }
}
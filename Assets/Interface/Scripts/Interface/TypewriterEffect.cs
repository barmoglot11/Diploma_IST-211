using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TypewriterEffect : MonoBehaviour
{
    [Header("Settings")]
    public float delayBetweenLetters = 0.1f; // Задержка между появлением букв
    public string fullText = "Hello, World!"; // Полный текст, который нужно отобразить
    public AudioClip typeSound; // Аудиодорожка, которая будет проигрываться

    private TMP_Text textComponent; // Компонент TextMeshPro
    private string currentText = ""; // Текущий текст, который отображается
    private AudioSource audioSource; // Компонент AudioSource для проигрывания звука

    private void Awake()
    {
        // Получаем компонент TextMeshPro
        textComponent = GetComponent<TMP_Text>();
        if (textComponent == null)
        {
            Debug.LogError("TextMeshPro component is missing!");
            return;
        }

        // Получаем или добавляем компонент AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Очищаем текст при старте
        textComponent.text = "";
    }

    private void Start()
    {
        // Запускаем анимацию появления текста
        StartCoroutine(ShowText());
    }

    // Корутина для анимации появления текста
    private IEnumerator ShowText()
    {
        // Проигрываем аудиодорожку, если она назначена
        if (typeSound != null && audioSource != null)
        {
            audioSource.clip = typeSound;
            audioSource.Play(); // Запускаем аудиодорожку
        }

        // Постепенно отображаем текст
        for (int i = 0; i < fullText.Length; i++)
        {
            // Добавляем по одной букве
            currentText += fullText[i];
            textComponent.text = currentText;

            // Ждем указанное время перед добавлением следующей буквы
            yield return new WaitForSeconds(delayBetweenLetters);
        }

        // Останавливаем аудиодорожку, если она еще играет (опционально)
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    // Метод для запуска анимации вручную (например, из другого скрипта)
    public void StartTypewriter(string newText)
    {
        fullText = newText;
        currentText = "";
        textComponent.text = "";
        StartCoroutine(ShowText());
    }
}
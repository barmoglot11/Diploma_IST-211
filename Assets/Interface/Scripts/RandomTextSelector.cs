using UnityEngine;
using TMPro; // Не забудьте подключить пространство имен для TextMeshPro

public class RandomTextSelector : MonoBehaviour
{
    [Header("Настройки")]
    [Tooltip("Массив текстов для выбора")]
    public string[] texts; // Массив текстов, которые можно задать в инспекторе

    [Tooltip("Компонент TextMeshPro для отображения текста")]
    public TextMeshProUGUI textComponent; // Ссылка на компонент TMP

    void Start()
    {
        // Проверяем, есть ли компонент TMP
        if (textComponent == null)
        {
            Debug.LogError("Компонент TextMeshProUGUI не назначен!");
            return;
        }

        // Проверяем, есть ли тексты в массиве
        if (texts == null || texts.Length == 0)
        {
            Debug.LogError("Массив текстов пуст!");
            return;
        }

        // Выбираем случайный текст и отображаем его
        SelectRandomText();
    }

    // Метод для выбора случайного текста
    public void SelectRandomText()
    {
        // Выбираем случайный индекс из массива
        int randomIndex = Random.Range(0, texts.Length);

        // Устанавливаем текст в компонент TMP
        textComponent.text = texts[randomIndex];
    }
}
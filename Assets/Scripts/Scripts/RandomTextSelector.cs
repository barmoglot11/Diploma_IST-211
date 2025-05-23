using UnityEngine;
using TMPro; // Подключаем пространство имен для работы с TextMeshPro

public class RandomTextSelector : MonoBehaviour
{
    [Header("Свойства")]
    [Tooltip("Массив текстов для отображения")]
    public string[] texts; // Массив текстов, из которых будет выбран один

    [Tooltip("Компонент TextMeshPro для отображения текста")]
    public TextMeshProUGUI textComponent; // Компонент для отображения TMP

    void Start()
    {
        // Проверяем, установлен ли компонент TMP
        if (textComponent == null)
        {
            Debug.LogError("Компонент TextMeshProUGUI не установлен!");
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
        // Генерируем случайный индекс
        int randomIndex = Random.Range(0, texts.Length);

        // Устанавливаем выбранный текст в компонент TMP
        textComponent.text = texts[randomIndex];
    }
}
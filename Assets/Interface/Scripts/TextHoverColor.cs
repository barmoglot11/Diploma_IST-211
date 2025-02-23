using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TextHoverColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI textComponent; // Компонент TextMeshProUGUI
    private Color normalColor = Color.white; // Обычный цвет текста (белый)
    private Color hoverColor = Color.black; // Цвет текста при наведении (черный)

    void Start()
    {
        // Получаем компонент TextMeshProUGUI
        textComponent = GetComponent<TextMeshProUGUI>();

        // Устанавливаем начальный цвет текста
        textComponent.color = normalColor;
    }

    // При наведении курсора
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Меняем цвет текста на черный
        textComponent.color = hoverColor;
    }

    // При уходе курсора
    public void OnPointerExit(PointerEventData eventData)
    {
        // Возвращаем цвет текста к белому
        textComponent.color = normalColor;
    }
}
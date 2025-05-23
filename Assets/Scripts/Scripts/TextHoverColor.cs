using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TextHoverColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI textComponent; // Компонент TextMeshProUGUI
    private Color normalColor = Color.white; // Цвет текста в нормальном состоянии (белый)
    private Color hoverColor = Color.black; // Цвет текста при наведении (черный)

    void Start()
    {
        // Получаем компонент TextMeshProUGUI
        textComponent = GetComponent<TextMeshProUGUI>();

        // Устанавливаем начальный цвет текста
        textComponent.color = normalColor;
    }

    // Метод вызывается при наведении указателя
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Изменяем цвет текста на цвет при наведении
        textComponent.color = hoverColor;
    }

    // Метод вызывается при уходе указателя
    public void OnPointerExit(PointerEventData eventData)
    {
        // Возвращаем цвет текста в нормальное состояние
        textComponent.color = normalColor;
    }
}
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class QuestButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Settings")]
    public Button button; // Ссылка на компонент Button
    public Sprite normalSprite; // Обычный спрайт кнопки
    public Sprite hoveredSprite; // Спрайт при наведении
    public Sprite clickedSprite; // Спрайт при нажатии
    public float fadeSpeed = 1f; // Скорость анимации изменения непрозрачности

    [Header("Other Buttons")]
    public QuestButton[] otherButtons; // Массив других кнопок в списке

    private bool isClicked = false; // Флаг, указывающий, была ли кнопка нажата

    private void Start()
    {
        // Устанавливаем начальный спрайт
        if (button != null && button.image != null)
        {
            button.image.sprite = normalSprite;
        }

        // Назначаем обработчик события нажатия
        if (button != null)
        {
            button.onClick.AddListener(OnPointerClick);
        }
    }

    // Обработчик события наведения курсора
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isClicked)
        {
            // Плавное изменение спрайта при наведении
            StartCoroutine(FadeTo(hoveredSprite, fadeSpeed));
        }
    }

    // Обработчик события ухода курсора
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isClicked)
        {
            // Плавное возвращение к обычному спрайту при уходе курсора
            StartCoroutine(FadeTo(normalSprite, fadeSpeed));
        }
    }

    // Обработчик события нажатия на кнопку
    public void OnPointerClick()
    {
        if (!isClicked)
        {
            // Плавное изменение спрайта при нажатии
            StartCoroutine(FadeTo(clickedSprite, fadeSpeed));
            isClicked = true;

            // Возвращаем остальные кнопки в исходное состояние
            foreach (var otherButton in otherButtons)
            {
                if (otherButton != this)
                {
                    otherButton.ResetButton();
                }
            }
        }
    }

    // Сброс кнопки в исходное состояние
    public void ResetButton()
    {
        isClicked = false;
        StartCoroutine(FadeTo(normalSprite, fadeSpeed));
    }

    // Плавное изменение спрайта и непрозрачности
    private IEnumerator FadeTo(Sprite newSprite, float speed)
    {
        if (button == null || button.image == null) yield break;

        float elapsed = 0f;
        Color startColor = button.image.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        // Меняем спрайт
        button.image.sprite = newSprite;

        // Плавно изменяем непрозрачность
        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * speed;
            button.image.color = Color.Lerp(startColor, endColor, elapsed);
            yield return null;
        }

        button.image.color = endColor;
    }
}
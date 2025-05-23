using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class QuestButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Settings")]
    public Button button; // Ссылка на кнопку UI
    public Sprite normalSprite; // Спрайт в обычном состоянии
    public Sprite hoveredSprite; // Спрайт при наведении
    public Sprite clickedSprite; // Спрайт при нажатии
    public float fadeSpeed = 1f; // Скорость плавного перехода между спрайтами

    [Header("Other Buttons")]
    public QuestButton[] otherButtons; // Массив других кнопок для сброса состояния

    private bool isClicked = false; // Флаг, указывающий, нажата ли кнопка

    private void Start()
    {
        // Инициализация спрайта кнопки
        if (button != null && button.image != null)
        {
            button.image.sprite = normalSprite;
        }

        // Подписка на событие нажатия кнопки
        if (button != null)
        {
            button.onClick.AddListener(OnPointerClick);
        }
    }

    // Обработчик события наведения курсора на кнопку
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isClicked)
        {
            // Плавный переход к спрайту при наведении
            StartCoroutine(FadeTo(hoveredSprite, fadeSpeed));
        }
    }

    // Обработчик события выхода курсора с кнопки
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isClicked)
        {
            // Плавный переход к обычному спрайту
            StartCoroutine(FadeTo(normalSprite, fadeSpeed));
        }
    }

    // Обработчик события нажатия кнопки
    public void OnPointerClick()
    {
        if (!isClicked)
        {
            // Плавный переход к спрайту при нажатии
            StartCoroutine(FadeTo(clickedSprite, fadeSpeed));
            isClicked = true;

            if (otherButtons.Length <= 0) return;
            
            // Сброс состояния других кнопок
            foreach (var otherButton in otherButtons)
            {
                if (otherButton != this && otherButton != null)
                {
                    otherButton.ResetButton();
                }
            }
        }
    }

    // Сброс состояния кнопки
    public void ResetButton()
    {
        isClicked = false;
        StartCoroutine(FadeTo(normalSprite, fadeSpeed));
    }

    // Корутин для плавного перехода между спрайтами
    private IEnumerator FadeTo(Sprite newSprite, float speed)
    {
        if (button == null || button.image == null) yield break;

        float elapsed = 0f;
        Color startColor = button.image.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        // Установка нового спрайта
        button.image.sprite = newSprite;

        // Плавный переход цвета
        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * speed;
            button.image.color = Color.Lerp(startColor, endColor, elapsed);
            yield return null;
        }

        button.image.color = endColor; // Установка конечного цвета
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class GradientButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image buttonImage;

    [Header("Настройки спрайта")]
    [Tooltip("Спрайт, который будет отображаться при наведении")]
    public Sprite hoverSprite; // Спрайт для наведения

    [Header("Настройки анимации")]
    [Tooltip("Длительность анимации в секундах")]
    public float fadeDuration = 0.5f; // Длительность плавного перехода

    [Tooltip("Масштаб кнопки при наведении")]
    public Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1.1f); // Увеличение на 10%

    private Coroutine fadeCoroutine; // Для управления корутиной
    private Vector3 normalScale; // Обычный масштаб кнопки

    void Start()
    {
        // Получаем компонент Image кнопки
        buttonImage = GetComponent<Image>();

        // Устанавливаем прозрачный цвет по умолчанию
        buttonImage.color = new Color(1, 1, 1, 0); // Прозрачный цвет
        buttonImage.sprite = null; // Убираем спрайт по умолчанию

        // Сохраняем обычный масштаб кнопки
        normalScale = transform.localScale;
    }

    // При наведении курсора
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSprite != null)
        {
            // Останавливаем предыдущую корутину, если она есть
            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);

            // Запускаем плавное появление спрайта и увеличение масштаба
            fadeCoroutine = StartCoroutine(FadeAndScale(hoverSprite, 1f, hoverScale)); // 1f = непрозрачность
        }
    }

    // При уходе курсора
    public void OnPointerExit(PointerEventData eventData)
    {
        // Останавливаем предыдущую корутину, если она есть
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        // Запускаем плавное исчезновение спрайта и возврат к обычному масштабу
        fadeCoroutine = StartCoroutine(FadeAndScale(null, 0f, normalScale)); // 0f = прозрачность
    }

    // Корутина для плавного изменения спрайта, прозрачности и масштаба
    private IEnumerator FadeAndScale(Sprite targetSprite, float targetAlpha, Vector3 targetScale)
    {
        float elapsedTime = 0f;
        Color startColor = buttonImage.color;
        Sprite startSprite = buttonImage.sprite;
        Vector3 startScale = transform.localScale;

        while (elapsedTime < fadeDuration)
        {
            // Плавно изменяем прозрачность
            float newAlpha = Mathf.Lerp(startColor.a, targetAlpha, elapsedTime / fadeDuration);
            buttonImage.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);

            // Плавно меняем спрайт
            if (targetSprite != null)
            {
                buttonImage.sprite = targetSprite;
            }

            // Плавно изменяем масштаб
            transform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / fadeDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Убедимся, что конечные значения установлены точно
        buttonImage.color = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
        buttonImage.sprite = targetSprite;
        transform.localScale = targetScale;
    }
}
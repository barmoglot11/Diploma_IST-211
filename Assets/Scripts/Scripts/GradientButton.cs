using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class GradientButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image buttonImage;

    [Header("Свойства кнопки")]
    [Tooltip("Спрайт, который будет отображаться при наведении")]
    public Sprite hoverSprite; // Спрайт при наведении

    [Header("Свойства затухания")]
    [Tooltip("Продолжительность затухания спрайта")]
    public float fadeDuration = 0.5f; // Продолжительность затухания

    [Tooltip("Масштаб кнопки при наведении")]
    public Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1.1f); // Увеличение на 10%

    private Coroutine fadeCoroutine; // Ссылка на корутину затухания
    private Vector3 normalScale; // Нормальный масштаб кнопки

    void Start()
    {
        // Получаем компонент Image кнопки
        buttonImage = GetComponent<Image>();

        // Устанавливаем начальный цвет кнопки
        buttonImage.color = new Color(1, 1, 1, 0); // Прозрачный цвет
        buttonImage.sprite = null; // Убираем спрайт

        // Сохраняем нормальный масштаб кнопки
        normalScale = transform.localScale;
    }

    // При наведении указателя на кнопку
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSprite != null)
        {
            // Останавливаем корутину, если она уже запущена
            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);

            // Запускаем корутину для затухания и изменения масштаба
            fadeCoroutine = StartCoroutine(FadeAndScale(hoverSprite, 1f, hoverScale)); // 1f = непрозрачный
        }
    }

    // При уходе указателя с кнопки
    public void OnPointerExit(PointerEventData eventData)
    {
        // Останавливаем корутину, если она уже запущена
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        // Запускаем корутину для возвращения к нормальному состоянию
        fadeCoroutine = StartCoroutine(FadeAndScale(null, 0f, normalScale)); // 0f = прозрачный
    }

    // Корутину для плавного изменения спрайта и масштаба
    private IEnumerator FadeAndScale(Sprite targetSprite, float targetAlpha, Vector3 targetScale)
    {
        float elapsedTime = 0f;
        Color startColor = buttonImage.color;
        Sprite startSprite = buttonImage.sprite;
        Vector3 startScale = transform.localScale;

        while (elapsedTime < fadeDuration)
        {
            // Вычисляем новый альфа-канал
            float newAlpha = Mathf.Lerp(startColor.a, targetAlpha, elapsedTime / fadeDuration);
            buttonImage.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);

            // Устанавливаем спрайт, если он не равен null
            if (targetSprite != null)
            {
                buttonImage.sprite = targetSprite;
            }

            // Вычисляем новый масштаб
            transform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / fadeDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Устанавливаем окончательные значения
        buttonImage.color = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
        buttonImage.sprite = targetSprite;
        transform.localScale = targetScale;
    }
}

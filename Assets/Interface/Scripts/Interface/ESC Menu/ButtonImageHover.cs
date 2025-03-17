using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonImageHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Settings")]
    public Image targetImage; // Изображение внутри кнопки
    public Sprite normalSprite; // Обычный спрайт
    public Sprite hoveredSprite; // Спрайт при наведении
    public float fadeSpeed = 1f; // Скорость анимации

    private RectTransform buttonRect; // RectTransform кнопки
    private bool isHovered = false; // Флаг наведения курсора
    private Canvas canvas; // Canvas, к которому принадлежит кнопка
    private Camera canvasCamera; // Камера, используемая для рендеринга Canvas

    private void Start()
    {
        // Получаем RectTransform кнопки
        buttonRect = GetComponent<RectTransform>();

        // Получаем Canvas и камеру, используемую для рендеринга
        canvas = GetComponentInParent<Canvas>();
        if (canvas != null)
        {
            canvasCamera = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;
        }

        // Устанавливаем начальный спрайт
        if (targetImage != null)
        {
            targetImage.sprite = normalSprite;
            targetImage.color = new Color(1, 1, 1, 1); // Полная непрозрачность
        }
    }

    private void Update()
    {
        // Если курсор наведён, проверяем, находится ли он в пределах кнопки с учётом отступов
        if (isHovered)
        {
            Vector2 localCursor;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(buttonRect, Input.mousePosition, canvasCamera, out localCursor);

            // Проверяем, находится ли курсор в пределах RectTransform кнопки
            if (buttonRect.rect.Contains(localCursor))
            {
                // Плавно меняем спрайт на hoveredSprite
                StartCoroutine(FadeTo(hoveredSprite, fadeSpeed));
            }
            else
            {
                // Плавно возвращаемся к normalSprite
                StartCoroutine(FadeTo(normalSprite, fadeSpeed));
            }
        }
    }

    // Обработчик события наведения курсора
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
    }

    // Обработчик события ухода курсора
    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        StartCoroutine(FadeTo(normalSprite, fadeSpeed));
    }

    // Плавное изменение спрайта и непрозрачности
    private IEnumerator FadeTo(Sprite newSprite, float speed)
    {
        if (targetImage == null) yield break;

        float elapsed = 0f;
        Color startColor = targetImage.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        // Меняем спрайт
        targetImage.sprite = newSprite;

        // Плавно изменяем непрозрачность
        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * speed;
            targetImage.color = Color.Lerp(startColor, endColor, elapsed);
            yield return null;
        }

        targetImage.color = endColor;
    }
}
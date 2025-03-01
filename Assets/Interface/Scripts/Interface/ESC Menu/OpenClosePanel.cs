using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OpenClosePanel : MonoBehaviour
{
    [Header("Settings")]
    public CanvasGroup panel; // Панель, которая будет открываться и закрываться
    public Button openButton; // Кнопка для открытия панели
    public Button closeButton; // Кнопка для закрытия панели
    public float fadeSpeed = 1f; // Скорость анимации

    private void Start()
    {
        // Проверяем, назначены ли панель и кнопки
        if (panel == null || openButton == null || closeButton == null)
        {
            Debug.LogError("Panel or buttons are not assigned in the inspector!");
            return;
        }

        // Изначально скрываем панель
        panel.alpha = 0;
        panel.gameObject.SetActive(false);

        // Назначаем обработчики событий для кнопок
        openButton.onClick.AddListener(OpenPanel);
        closeButton.onClick.AddListener(ClosePanel);
    }

    // Открыть панель
    private void OpenPanel()
    {
        StopAllCoroutines(); // Останавливаем все анимации
        StartCoroutine(FadeIn());
    }

    // Закрыть панель
    private void ClosePanel()
    {
        StopAllCoroutines(); // Останавливаем все анимации
        StartCoroutine(FadeOut());
    }

    // Анимация открытия (увеличение непрозрачности)
    private IEnumerator FadeIn()
    {
        panel.gameObject.SetActive(true);

        float elapsed = 0f;
        float startAlpha = panel.alpha;
        float endAlpha = 1f; // Полная непрозрачность

        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * fadeSpeed;
            panel.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed);
            yield return null;
        }

        panel.alpha = endAlpha;
    }

    // Анимация закрытия (уменьшение непрозрачности)
    private IEnumerator FadeOut()
    {
        float elapsed = 0f;
        float startAlpha = panel.alpha;
        float endAlpha = 0f; // Полная прозрачность

        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * fadeSpeed;
            panel.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed);
            yield return null;
        }

        panel.alpha = endAlpha;
        panel.gameObject.SetActive(false);
    }
}
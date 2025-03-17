using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonPanelManager : MonoBehaviour
{
    [Header("Settings")]
    public Button[] buttons; // Массив кнопок
    public CanvasGroup[] panels; // Массив панелей (используем CanvasGroup вместо Image)
    public float fadeSpeed = 5f; // Скорость анимации

    private void Start()
    {
        // Назначаем обработчики событий для каждой кнопки
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Локальная переменная для замыкания
            buttons[i].onClick.AddListener(() => OnButtonClick(index));
        }

        // Изначально скрываем все панели
        // foreach (var panel in panels)
        // {
            // if (panel != null)
            // {
                // panel.gameObject.SetActive(false);
                // panel.alpha = 0; // Полная прозрачность
           // }
        // }
    }

    // Обработчик нажатия на кнопку
    private void OnButtonClick(int buttonIndex)
    {
        // Запускаем процесс закрытия всех панелей, затем открытия выбранной
        StartCoroutine(CloseAllPanelsThenOpen(buttonIndex));
    }

    // Закрыть все панели, затем открыть выбранную
    private IEnumerator CloseAllPanelsThenOpen(int panelIndexToOpen)
    {
        // Закрываем все панели
        foreach (var panel in panels)
        {
            if (panel != null && panel.gameObject.activeSelf)
            {
                yield return StartCoroutine(FadeOut(panel)); // Ждём завершения анимации исчезновения
            }
        }

        // Открываем выбранную панель
        if (panelIndexToOpen >= 0 && panelIndexToOpen < panels.Length && panels[panelIndexToOpen] != null)
        {
            yield return StartCoroutine(FadeIn(panels[panelIndexToOpen])); // Ждём завершения анимации появления
        }
    }

    // Анимация появления (увеличение непрозрачности)
    private IEnumerator FadeIn(CanvasGroup panel)
    {
        if (panel == null) yield break;

        panel.gameObject.SetActive(true);
        float elapsed = 0f;
        float startAlpha = 0; // Начинаем с прозрачности 0
        float endAlpha = 1; // Полная непрозрачность

        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * fadeSpeed;
            panel.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed);
            yield return null;
        }

        panel.alpha = endAlpha;
    }

    // Анимация исчезновения (уменьшение непрозрачности)
    private IEnumerator FadeOut(CanvasGroup panel)
    {
        if (panel == null) yield break;

        float elapsed = 0f;
        float startAlpha = panel.alpha;
        float endAlpha = 0; // Полная прозрачность

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
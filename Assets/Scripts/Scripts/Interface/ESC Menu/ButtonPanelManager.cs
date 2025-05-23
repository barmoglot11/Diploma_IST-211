using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonPanelManager : MonoBehaviour
{
    [Header("Settings")]
    public Button[] buttons; // Массив кнопок
    public CanvasGroup[] panels; // Массив панелей (CanvasGroup с компонентом Image)
    public float fadeSpeed = 5f; // Скорость затухания

    private void Start()
    {
        // Подписка на события нажатия кнопок
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Сохранение индекса для замыкания
            buttons[i].onClick.AddListener(() => OnButtonClick(index));
        }

        // Инициализация панелей
        // foreach (var panel in panels)
        // {
        //     if (panel != null)
        //     {
        //         panel.gameObject.SetActive(false);
        //         panel.alpha = 0; // Установка прозрачности на 0
        //     }
        // }
    }

    // Обработка нажатия на кнопку
    private void OnButtonClick(int buttonIndex)
    {
        // Закрытие всех панелей и открытие выбранной
        StartCoroutine(CloseAllPanelsThenOpen(buttonIndex));
    }

    // Закрытие панелей, затем открытие выбранной
    private IEnumerator CloseAllPanelsThenOpen(int panelIndexToOpen)
    {
        // Закрытие всех активных панелей
        foreach (var panel in panels)
        {
            if (panel != null && panel.gameObject.activeSelf)
            {
                yield return StartCoroutine(FadeOut(panel)); // Затухание панели
            }
        }

        // Открытие выбранной панели
        if (panelIndexToOpen >= 0 && panelIndexToOpen < panels.Length && panels[panelIndexToOpen] != null)
        {
            yield return StartCoroutine(FadeIn(panels[panelIndexToOpen])); // Появление панели
        }
    }

    // Появление панели (затухание в)
    private IEnumerator FadeIn(CanvasGroup panel)
    {
        if (panel == null) yield break;

        panel.gameObject.SetActive(true);
        float elapsed = 0f;
        float startAlpha = 0; // Начальная прозрачность 0
        float endAlpha = 1; // Конечная прозрачность

        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * fadeSpeed;
            panel.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed);
            yield return null;
        }

        panel.alpha = endAlpha;
    }

    // Затухание панели (затухание из)
    private IEnumerator FadeOut(CanvasGroup panel)
    {
        if (panel == null) yield break;

        float elapsed = 0f;
        float startAlpha = panel.alpha;
        float endAlpha = 0; // Конечная прозрачность

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

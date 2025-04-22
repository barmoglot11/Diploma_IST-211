using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HidePanelOnClick : MonoBehaviour
{
    [Header("Settings")]
    public Button button; // Кнопка, которая скрывает панель
    public CanvasGroup panelCanvasGroup; // CanvasGroup, который будет управлять прозрачностью панели
    public float fadeSpeed = 1f; // Скорость анимации

    private void Start()
    {
        // Проверяем, назначены ли кнопка и CanvasGroup
        if (button == null || panelCanvasGroup == null)
        {
            Debug.LogError("Button or CanvasGroup is not assigned in the inspector!");
            return;
        }

        // Назначаем обработчик события нажатия на кнопку
        button.onClick.AddListener(OnButtonClick);
    }

    // Обработчик нажатия на кнопку
    private void OnButtonClick()
    {
        StartCoroutine(FadeOutPanel());
    }

    // Анимация скрытия панели
    private IEnumerator FadeOutPanel()
    {
        float elapsed = 0f;
        float startAlpha = panelCanvasGroup.alpha;
        float endAlpha = 0f; // Полная прозрачность

        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * fadeSpeed;
            panelCanvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed);
            yield return null;
        }

        panelCanvasGroup.alpha = endAlpha;

        // Отключаем взаимодействие с панелью
        panelCanvasGroup.interactable = false;
        panelCanvasGroup.blocksRaycasts = false;

        // Отключаем объект панели (опционально)
        panelCanvasGroup.gameObject.SetActive(false);
    }
}
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowPanel : MonoBehaviour
{
    [Header("Settings")]
    public Button button; // Кнопка, которая активирует панель
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

        // Изначально скрываем панель
        panelCanvasGroup.alpha = 0;
        panelCanvasGroup.interactable = false;
        panelCanvasGroup.blocksRaycasts = false;

        // Назначаем обработчик события нажатия на кнопку
        button.onClick.AddListener(OnButtonClick);
    }

    // Обработчик нажатия на кнопку
    private void OnButtonClick()
    {
        StartCoroutine(FadeInPanel());
    }

    // Анимация появления панели
    private IEnumerator FadeInPanel()
    {
        // Включаем объект панели (опционально)
        panelCanvasGroup.gameObject.SetActive(true);
        panelCanvasGroup.interactable = true;
        panelCanvasGroup.blocksRaycasts = true;

        float elapsed = 0f;
        float startAlpha = panelCanvasGroup.alpha;
        float endAlpha = 1f; // Полная непрозрачность

        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * fadeSpeed;
            panelCanvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed);
            yield return null;
        }

        panelCanvasGroup.alpha = endAlpha;
    }
}
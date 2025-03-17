using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowPanelOnClick : MonoBehaviour
{
    [Header("Настройки панели")]
    [Tooltip("Панель, которая будет появляться")]
    public GameObject panel; // Панель, которую нужно показать

    [Header("Настройки анимации")]
    [Tooltip("Длительность анимации в секундах")]
    public float fadeDuration = 1f; // Длительность плавного появления

    private CanvasGroup panelCanvasGroup; // Компонент для управления прозрачностью
    private Button button; // Компонент кнопки

    void Start()
    {
        // Получаем компонент кнопки
        button = GetComponent<Button>();

        // Назначаем метод обработки нажатия
        button.onClick.AddListener(OnButtonClick);

        // Получаем или добавляем CanvasGroup к панели
        if (panel != null)
        {
            panelCanvasGroup = panel.GetComponent<CanvasGroup>();
            if (panelCanvasGroup == null)
            {
                panelCanvasGroup = panel.AddComponent<CanvasGroup>();
            }

            // Скрываем панель в начале
            panelCanvasGroup.alpha = 0f;
            panelCanvasGroup.interactable = false;
            panelCanvasGroup.blocksRaycasts = false;
            panel.SetActive(false); // Отключаем панель
        }
    }

    // Обработчик нажатия на кнопку
    private void OnButtonClick()
    {
        if (panel != null)
        {
            // Включаем панель и запускаем анимацию
            panel.SetActive(true);
            StartCoroutine(FadeIn());
        }
    }

    // Корутина для плавного появления панели
    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        // Включаем взаимодействие с панелью
        panelCanvasGroup.interactable = true;
        panelCanvasGroup.blocksRaycasts = true;

        while (elapsedTime < fadeDuration)
        {
            // Плавно изменяем прозрачность
            float newAlpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            panelCanvasGroup.alpha = newAlpha;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Убедимся, что конечная прозрачность установлена точно
        panelCanvasGroup.alpha = 1f;
    }
}
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowPanelOnClick : MonoBehaviour
{
    [Header("Свойства панели")]
    [Tooltip("Панель, которая будет отображаться")]
    public GameObject panel; // Панель, которая будет отображаться

    [Header("Свойства затухания")]
    [Tooltip("Продолжительность затухания панели")]
    public float fadeDuration = 1f; // Продолжительность затухания

    private CanvasGroup panelCanvasGroup; // Ссылка на компонент CanvasGroup панели
    private Button button; // Ссылка на компонент Button

    void Start()
    {
        // Получаем компонент Button
        button = GetComponent<Button>();

        // Добавляем слушатель для обработки клика по кнопке
        button.onClick.AddListener(OnButtonClick);

        // Проверяем наличие CanvasGroup на панели
        if (panel != null)
        {
            panelCanvasGroup = panel.GetComponent<CanvasGroup>();
            if (panelCanvasGroup == null)
            {
                panelCanvasGroup = panel.AddComponent<CanvasGroup>();
            }

            // Устанавливаем начальные значения для CanvasGroup
            panelCanvasGroup.alpha = 0f;
            panelCanvasGroup.interactable = false;
            panelCanvasGroup.blocksRaycasts = false;
            panel.SetActive(false); // Скрываем панель
        }
    }

    // Метод, вызываемый при клике на кнопку
    private void OnButtonClick()
    {
        if (panel != null)
        {
            // Активируем панель и запускаем корутину для затухания
            panel.SetActive(true);
            StartCoroutine(FadeIn());
        }
    }

    // Корутину для плавного появления панели
    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        // Разрешаем взаимодействие с панелью
        panelCanvasGroup.interactable = true;
        panelCanvasGroup.blocksRaycasts = true;

        while (elapsedTime < fadeDuration)
        {
            // Вычисляем новый альфа-канал
            float newAlpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            panelCanvasGroup.alpha = newAlpha;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Устанавливаем окончательное значение альфа-канала
        panelCanvasGroup.alpha = 1f;
    }
}

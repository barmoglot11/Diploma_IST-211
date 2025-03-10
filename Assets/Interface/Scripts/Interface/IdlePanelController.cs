using UnityEngine;
using UnityEngine.UI;

public class IdlePanelController : MonoBehaviour
{
    public CanvasGroup panelCanvasGroup; // Ссылка на CanvasGroup панели
    public float fadeDuration = 1.0f; // Длительность плавного появления/исчезновения
    public float idleTimeToShow = 5.0f; // Время бездействия для появления панели

    private float idleTimer = 0f; // Таймер бездействия
    private bool isPanelVisible = false; // Флаг, указывающий, видна ли панель
    private bool isFading = false; // Флаг, указывающий, идет ли процесс изменения прозрачности

    void Update()
    {
        // Проверяем, было ли какое-либо действие пользователя
        if (Input.anyKeyDown || Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0 || Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            idleTimer = 0f; // Сбрасываем таймер бездействия

            // Если панель видна, начинаем скрывать её
            if (isPanelVisible && !isFading)
            {
                StartCoroutine(FadePanel(0f)); // Плавно скрываем панель
            }
        }
        else
        {
            // Увеличиваем таймер бездействия
            idleTimer += Time.deltaTime;

            // Если время бездействия превысило порог и панель не видна, начинаем показывать её
            if (idleTimer >= idleTimeToShow && !isPanelVisible && !isFading)
            {
                StartCoroutine(FadePanel(1f)); // Плавно показываем панель
            }
        }
    }

    private System.Collections.IEnumerator FadePanel(float targetAlpha)
    {
        isFading = true;

        float startAlpha = panelCanvasGroup.alpha;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            panelCanvasGroup.alpha = alpha; // Плавно изменяем прозрачность CanvasGroup
            yield return null;
        }

        // Устанавливаем конечное значение прозрачности
        panelCanvasGroup.alpha = targetAlpha;

        // Обновляем флаги
        isPanelVisible = (targetAlpha == 1f);
        isFading = false;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TogglePanelOnE : MonoBehaviour
{
    public CanvasGroup canvas1; // Первый CanvasGroup
    public CanvasGroup canvas2; // Второй CanvasGroup
    public float fadeDuration = 1f; // Длительность плавного перехода
    public MonoBehaviour cameraScript; // Скрипт, управляющий камерой (например, контроллер движения камеры)

    private bool isCanvas1Visible = false;
    private bool isFading = false; // Флаг для отслеживания выполнения корутин

    void Update()
    {
        if (Input.GetKey(KeyCode.E) && !isFading) // Проверяем, не выполняется ли уже анимация
        {
            ToggleCanvases();
        }
    }

    void ToggleCanvases()
    {
        isCanvas1Visible = !isCanvas1Visible;

        if (isCanvas1Visible)
        {
            StartCoroutine(FadeCanvas(canvas1, 1f, true)); // Плавное появление canvas1
            StartCoroutine(FadeCanvas(canvas2, 0f, false)); // Плавное исчезновение canvas2
            Cursor.visible = true; // Включаем курсор
            Cursor.lockState = CursorLockMode.None; // Разблокируем курсор

            // Отключаем скрипт, управляющий камерой
            if (cameraScript != null)
            {
                cameraScript.enabled = false; // Отключаем скрипт
            }
        }
    }

    private System.Collections.IEnumerator FadeCanvas(CanvasGroup canvasGroup, float targetAlpha, bool activateAfterFade)
    {
        isFading = true; // Устанавливаем флаг, что анимация началась

        // Если targetAlpha > 0, активируем объект перед началом анимации
        if (targetAlpha > 0)
        {
            canvasGroup.gameObject.SetActive(true);
        }

        float startAlpha = canvasGroup.alpha;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;

        // Если targetAlpha == 0, деактивируем объект после завершения анимации
        if (targetAlpha == 0)
        {
            canvasGroup.gameObject.SetActive(false);
        }

        // Активируем или деактивируем объект в зависимости от параметра activateAfterFade
        if (activateAfterFade)
        {
            canvasGroup.gameObject.SetActive(true);
        }
        else
        {
            canvasGroup.gameObject.SetActive(false);
        }

        isFading = false; // Сбрасываем флаг, что анимация завершена
    }
}

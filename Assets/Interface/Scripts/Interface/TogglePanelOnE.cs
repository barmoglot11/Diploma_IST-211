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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleCanvases();
        }
    }

    void ToggleCanvases()
    {
        isCanvas1Visible = !isCanvas1Visible;

        if (isCanvas1Visible)
        {
            StartCoroutine(FadeCanvas(canvas1, 1f)); // Плавное появление canvas1
            StartCoroutine(FadeCanvas(canvas2, 0f)); // Плавное исчезновение canvas2
            Cursor.visible = true; // Включаем курсор
            Cursor.lockState = CursorLockMode.None; // Разблокируем курсор

            // Отключаем скрипт, управляющий камерой
            if (cameraScript != null)
            {
                cameraScript.enabled = false; // Отключаем скрипт
            }
        }
        else
        {
            StartCoroutine(FadeCanvas(canvas1, 0f)); // Плавное исчезновение canvas1
            StartCoroutine(FadeCanvas(canvas2, 1f)); // Плавное появление canvas2
            Cursor.visible = false; // Выключаем курсор
            Cursor.lockState = CursorLockMode.Locked; // Блокируем курсор

            // Включаем скрипт, управляющий камерой
            if (cameraScript != null)
            {
                cameraScript.enabled = true; // Включаем скрипт
            }
        }
    }

    private System.Collections.IEnumerator FadeCanvas(CanvasGroup canvasGroup, float targetAlpha)
    {
        float startAlpha = canvasGroup.alpha;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseItemDescription : MonoBehaviour
{
    public CanvasGroup canvasGroupToHide; // CanvasGroup, который нужно скрыть
    public CanvasGroup canvasGroupToShow; // CanvasGroup, который нужно показать
    public float fadeDuration = 1f; // Длительность анимации исчезновения/появления
    public MonoBehaviour cameraScript; // Скрипт, управляющий камерой
    public Button closeButton; // Кнопка, которая запускает скрипт

    private bool isFading = false; // Флаг, чтобы избежать множественных вызовов

    private void Start()
    {
        // Проверяем, назначена ли кнопка
        if (closeButton != null)
        {
            // Подписываемся на событие нажатия кнопки
            closeButton.onClick.AddListener(StartCloseDescription);
        }
        else
        {
            Debug.LogWarning("Кнопка не назначена в инспекторе!");
        }
    }

    public void StartCloseDescription()
    {
        if (!isFading)
        {
            StartCoroutine(FadeOutAndSwitch());
        }
    }

    private System.Collections.IEnumerator FadeOutAndSwitch()
    {
        isFading = true;

        // Плавное исчезновение текущего CanvasGroup
        float elapsedTime = 0f;
        float startAlpha = canvasGroupToHide.alpha;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroupToHide.alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroupToHide.alpha = 0f; // Убедимся, что alpha = 0

        // Включаем другой CanvasGroup и плавно его показываем
        if (canvasGroupToShow != null)
        {
            canvasGroupToShow.gameObject.SetActive(true);
            elapsedTime = 0f;
            startAlpha = canvasGroupToShow.alpha;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                canvasGroupToShow.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
                yield return null;
            }

            canvasGroupToShow.alpha = 1f; // Убедимся, что alpha = 1
        }

        // Включаем вращение камеры
        if (cameraScript != null)
        {
            cameraScript.enabled = true;
            Debug.Log("Скрипт камеры включен: " + cameraScript.enabled);
        }

        // Управление курсором
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("Курсор скрыт и заблокирован.");
        canvasGroupToHide.gameObject.SetActive(false); // Деактивируем панель
        isFading = false;
    }
}

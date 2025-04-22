using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseItemDescription : MonoBehaviour
{
    [Header("References")]
    public CanvasGroup canvasGroupToHide;  // Группа UI-элементов, которую нужно скрыть
    public CanvasGroup canvasGroupToShow;  // Группа UI-элементов, которую нужно показать
    public Button closeButton;             // Кнопка, активирующая закрытие описания
    
    [Header("Settings")]
    public float fadeDuration = 1f;        // Длительность анимации исчезновения/появления
    public InputStatus inputStatus = InputStatus.None;

    private bool isFading = false;         // Флаг, предотвращающий повторный запуск анимации

    private void Start()
    {
        // Проверяем наличие кнопки и подписываемся на событие клика
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(StartCloseDescription);
        }
        else
        {
            Debug.LogWarning("Кнопка не назначена в инспекторе!");
        }
    }

    /// <summary>
    /// Запускает процесс закрытия UI-описания с анимацией
    /// </summary>
    public void StartCloseDescription()
    {
        if (!isFading)
        {
            StartCoroutine(FadeOutAndSwitch());
        }
    }

    /// <summary>
    /// Корутина для плавного скрытия одного UI и показа другого
    /// </summary>
    private IEnumerator FadeOutAndSwitch()
    {
        isFading = true;

        // Анимация исчезновения текущей группы
        float elapsedTime = 0f;
        float startAlpha = canvasGroupToHide.alpha;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroupToHide.alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroupToHide.alpha = 0f;

        // Анимация появления новой группы (если назначена)
        if (canvasGroupToShow != null)
        {
            canvasGroupToShow.gameObject.SetActive(true);
            elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                canvasGroupToShow.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
                yield return null;
            }

            canvasGroupToShow.alpha = 1f;
        }

        // Дополнительные действия после анимации
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("Описание закрыто и скрыто.");
        
        canvasGroupToHide.gameObject.SetActive(false);
        isFading = false;
        
        // Восстановление состояния ввода
        if (inputStatus == InputStatus.None)
            InputManager.Instance.ReturnToPreviousStatus();
        else
            InputManager.Instance.ChangeInputStatus(inputStatus);
        
        inputStatus = InputStatus.None;
    }

    /// <summary>
    /// Устанавливает желаемое состояние ввода после закрытия
    /// </summary>
    public void ChangeInputStatus(InputStatus status) => inputStatus = status;
}

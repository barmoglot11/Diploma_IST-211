using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeCanvasController : MonoBehaviour
{
    public CanvasGroup[] canvasGroups; // Массив CanvasGroup, которые будут показаны/скрыты
    public CanvasGroup panelCanvasGroup; // CanvasGroup панели, которая будет показана
    public float fadeDuration = 1.0f; // Продолжительность перехода при показе/скрытии
    public float panelFadeDuration = 0.5f; // Продолжительность перехода панели

    private bool isPlayerOnCube = false; // Флаг, указывающий, находится ли игрок на кубе

    void Start()
    {
        // Инициализация CanvasGroup в скрытом состоянии
        foreach (var canvasGroup in canvasGroups)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        if (panelCanvasGroup != null)
        {
            panelCanvasGroup.gameObject.SetActive(false); // Скрываем панель
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Проверяем, что триггер сработал для игрока
        {
            isPlayerOnCube = true;
            StartCoroutine(ShowCanvasAndPanel());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Проверяем, что триггер сработал для игрока
        {
            isPlayerOnCube = false;
            StartCoroutine(HideCanvasAndPanel());
        }
    }

    private IEnumerator ShowCanvasAndPanel()
    {
        // Показ CanvasGroup
        foreach (var canvasGroup in canvasGroups)
        {
            StartCoroutine(FadeCanvasGroup(canvasGroup, 1f));
        }

        // Показ панели и её плавное появление
        if (panelCanvasGroup != null)
        {
            panelCanvasGroup.gameObject.SetActive(true); // Активируем панель
            yield return StartCoroutine(FadeCanvasGroup(panelCanvasGroup, 1f, panelFadeDuration));
        }
    }

    private IEnumerator HideCanvasAndPanel()
    {
        // Скрытие панели
        if (panelCanvasGroup != null)
        {
            yield return StartCoroutine(FadeCanvasGroup(panelCanvasGroup, 0f, panelFadeDuration));
            panelCanvasGroup.gameObject.SetActive(false); // Деактивируем панель
        }

        // Скрытие CanvasGroup
        foreach (var canvasGroup in canvasGroups)
        {
            StartCoroutine(FadeCanvasGroup(canvasGroup, 0f));
        }
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float targetAlpha, float duration = -1f)
    {
        if (duration < 0)
        {
            duration = fadeDuration; // Используем fadeDuration по умолчанию
        }

        float startAlpha = canvasGroup.alpha;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            canvasGroup.alpha = alpha;
            yield return null;
        }

        canvasGroup.alpha = targetAlpha; // Устанавливаем конечный альфа-канал
        canvasGroup.interactable = (targetAlpha == 1f); // Устанавливаем интерактивность
        canvasGroup.blocksRaycasts = (targetAlpha == 1f); // Устанавливаем блокировку Raycasts
    }
}

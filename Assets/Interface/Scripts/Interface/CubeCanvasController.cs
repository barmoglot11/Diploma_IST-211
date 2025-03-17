using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeCanvasController : MonoBehaviour
{
    public CanvasGroup[] canvasGroups; // Массив CanvasGroup, которые нужно показать/скрыть
    public CanvasGroup panelCanvasGroup; // CanvasGroup панели, которая будет анимироваться
    public float fadeDuration = 1.0f; // Длительность плавного появления/исчезновения
    public float panelFadeDuration = 0.5f; // Длительность анимации панели

    private bool isPlayerOnCube = false; // Флаг, указывающий, находится ли игрок на кубике

    void Start()
    {
        // Скрываем все CanvasGroup и панель при старте
        foreach (var canvasGroup in canvasGroups)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        if (panelCanvasGroup != null)
        {
            panelCanvasGroup.gameObject.SetActive(false); // Панель изначально отключена
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Проверяем, что на кубик встал игрок
        {
            isPlayerOnCube = true;
            StartCoroutine(ShowCanvasAndPanel());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Проверяем, что игрок ушёл с кубика
        {
            isPlayerOnCube = false;
            StartCoroutine(HideCanvasAndPanel());
        }
    }

    private IEnumerator ShowCanvasAndPanel()
    {
        // Плавное появление CanvasGroup
        foreach (var canvasGroup in canvasGroups)
        {
            StartCoroutine(FadeCanvasGroup(canvasGroup, 1f));
        }

        // Включаем панель и плавно показываем её
        if (panelCanvasGroup != null)
        {
            panelCanvasGroup.gameObject.SetActive(true); // Включаем панель
            yield return StartCoroutine(FadeCanvasGroup(panelCanvasGroup, 1f, panelFadeDuration));
        }
    }

    private IEnumerator HideCanvasAndPanel()
    {
        // Плавное исчезновение панели через alpha
        if (panelCanvasGroup != null)
        {
            yield return StartCoroutine(FadeCanvasGroup(panelCanvasGroup, 0f, panelFadeDuration));
            panelCanvasGroup.gameObject.SetActive(false); // Отключаем панель после завершения анимации
        }

        // Плавное исчезновение CanvasGroup
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

        canvasGroup.alpha = targetAlpha; // Убедимся, что прозрачность установлена точно
        canvasGroup.interactable = (targetAlpha == 1f); // Делаем элементы интерактивными только при полной видимости
        canvasGroup.blocksRaycasts = (targetAlpha == 1f); // Блокируем Raycasts, если CanvasGroup скрыт
    }
}

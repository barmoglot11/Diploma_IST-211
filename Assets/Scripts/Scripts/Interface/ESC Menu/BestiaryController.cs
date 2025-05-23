using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class BestiaryController : MonoBehaviour
{
    // Массив страниц и описаний
    public GameObject[] pages;
    public CanvasGroup[] descriptions;

    // Кнопки навигации
    public Button nextButton;
    public Button prevButton;

    // Звук переворота страницы
    public AudioSource pageFlipSound;

    // Переменные для анимации
    private bool isAnimating = false;
    private int currentPage = 0;

    // Настройки анимации
    [Header("Animation Settings")]
    public float pageTransitionDuration = 0.3f;  // Продолжительность анимации перехода страницы
    public float fadeDuration = 0.3f;  // Продолжительность анимации затухания

    // Целевая масштабировка страницы
    private Vector3 targetScale = new Vector3(430f, 300f, 285f);

    void Start()
    {
        // Устанавливаем активную страницу: показываем первую страницу
        SetPageActive(currentPage);

        // Подписка на события кнопок
        nextButton.onClick.AddListener(OnNextButtonClicked);
        prevButton.onClick.AddListener(OnPrevButtonClicked);

        // Настройка эффекта наведения на кнопки
        SetupButtonHoverEffect(nextButton);
        SetupButtonHoverEffect(prevButton);
    }

    // Обработчик нажатия кнопки "Следующая"
    void OnNextButtonClicked()
    {
        if (isAnimating) return;

        // Переход на следующую страницу или к первой, если достигнута последняя
        if (currentPage >= pages.Length - 1)
        {
            StartCoroutine(SwitchPage(0));
        }
        else
        {
            StartCoroutine(SwitchPage(currentPage + 1));
        }
    }

    // Обработчик нажатия кнопки "Предыдущая"
    void OnPrevButtonClicked()
    {
        if (isAnimating) return;

        // Переход на предыдущую страницу или к последней, если достигнута первая
        if (currentPage <= 0)
        {
            StartCoroutine(SwitchPage(pages.Length - 1));
        }
        else
        {
            StartCoroutine(SwitchPage(currentPage - 1));
        }
    }

    // Корутин для переключения страниц
    IEnumerator SwitchPage(int targetPage)
    {
        isAnimating = true;

        // Воспроизведение звука переворота страницы
        pageFlipSound.Play();

        // Анимация выхода текущей страницы
        AnimatePageTransition(pages[currentPage], false);

        // Затухание текущего описания
        yield return StartCoroutine(FadeCanvasGroup(descriptions[currentPage], 0f));

        // Задержка для завершения затухания
        yield return new WaitForSeconds(fadeDuration);

        // Деактивируем текущую страницу и описание
        pages[currentPage].SetActive(false);
        descriptions[currentPage].gameObject.SetActive(false);

        // Обновляем текущую страницу
        currentPage = targetPage;
        pages[currentPage].SetActive(true);
        descriptions[currentPage].gameObject.SetActive(true);

        // Анимация появления новой страницы
        AnimatePageTransition(pages[currentPage], true);

        // Появление нового описания
        yield return StartCoroutine(FadeCanvasGroup(descriptions[currentPage], 1f));

        isAnimating = false;
    }

    // Анимация перехода страницы (масштаб)
    void AnimatePageTransition(GameObject page, bool isIn)
    {
        Vector3 targetScaleValue = isIn ? targetScale : Vector3.zero;
        StartCoroutine(ScaleTransition(page.transform, targetScaleValue));
    }

    // Корутин для анимации масштабирования
    IEnumerator ScaleTransition(Transform pageTransform, Vector3 targetScale)
    {
        Vector3 startScale = pageTransform.localScale;
        float time = 0f;

        while (time < pageTransitionDuration)
        {
            pageTransform.localScale = Vector3.Lerp(startScale, targetScale, time / pageTransitionDuration);
            time += Time.deltaTime;
            yield return null;
        }

        pageTransform.localScale = targetScale;
    }

    // Корутин для затухания alpha у CanvasGroup
    IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float targetAlpha)
    {
        float startAlpha = canvasGroup.alpha;
        float time = 0f;

        while (time < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
    }

    // Установка активной страницы
    void SetPageActive(int pageIndex)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == pageIndex);
            descriptions[i].gameObject.SetActive(i == pageIndex);
        }
    }

    // Настройка эффекта наведения на кнопки
    void SetupButtonHoverEffect(Button button)
    {
        EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((data) => { OnButtonHoverEnter(button); });
        trigger.triggers.Add(entryEnter);

        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((data) => { OnButtonHoverExit(button); });
        trigger.triggers.Add(entryExit);
    }

    // Увеличение кнопки при наведении
    void OnButtonHoverEnter(Button button)
    {
        button.transform.localScale = Vector3.one * 1.1f;
    }

    // Восстановление размера кнопки при выходе
    void OnButtonHoverExit(Button button)
    {
        button.transform.localScale = Vector3.one;
    }
}

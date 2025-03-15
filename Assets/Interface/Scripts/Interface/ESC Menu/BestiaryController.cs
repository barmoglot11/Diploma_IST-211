using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class BestiaryController : MonoBehaviour
{
    // Массивы листов и групп канвасов с описаниями
    public GameObject[] pages;
    public CanvasGroup[] descriptions;

    // Кнопки стрелок
    public Button nextButton;
    public Button prevButton;

    // Звук перелистывания
    public AudioSource pageFlipSound;

    // Переменные для анимации
    private bool isAnimating = false;
    private int currentPage = 0;

    // Настройка скорости анимации
    [Header("Animation Settings")]
    public float pageTransitionDuration = 0.3f;  // Длительность анимации переключения страницы
    public float fadeDuration = 0.3f;  // Длительность анимации затухания и появления

    // Размеры листа
    private Vector3 targetScale = new Vector3(430f, 300f, 285f);

    void Start()
    {
        // Настроить начальное состояние: активировать первый лист и описание
        SetPageActive(currentPage);

        // Подключение событий к кнопкам
        nextButton.onClick.AddListener(OnNextButtonClicked);
        prevButton.onClick.AddListener(OnPrevButtonClicked);

        // Настроить анимацию при наведении на кнопки
        SetupButtonHoverEffect(nextButton);
        SetupButtonHoverEffect(prevButton);
    }

    // Обработчик нажатия на кнопку "вперед"
    void OnNextButtonClicked()
    {
        if (isAnimating) return;

        // Если мы на последней странице, переключаемся на первую
        if (currentPage >= pages.Length - 1)
        {
            StartCoroutine(SwitchPage(0));
        }
        else
        {
            // Переключаемся на следующую страницу
            StartCoroutine(SwitchPage(currentPage + 1));
        }
    }

    // Обработчик нажатия на кнопку "назад"
    void OnPrevButtonClicked()
    {
        if (isAnimating) return;

        // Если мы на первой странице, переключаемся на последнюю
        if (currentPage <= 0)
        {
            StartCoroutine(SwitchPage(pages.Length - 1));
        }
        else
        {
            // Переключаемся на предыдущую страницу
            StartCoroutine(SwitchPage(currentPage - 1));
        }
    }

    // Анимация переключения страницы
    IEnumerator SwitchPage(int targetPage)
    {
        isAnimating = true;

        // Воспроизводим звук перелистывания
        pageFlipSound.Play();

        // Сначала анимируем текущую страницу: уменьшаем scale
        AnimatePageTransition(pages[currentPage], false);

        // Анимируем исчезновение текущего описания (alpha)
        yield return StartCoroutine(FadeCanvasGroup(descriptions[currentPage], 0f));

        // Ожидаем окончания анимации
        yield return new WaitForSeconds(fadeDuration); // время затухания

        // Отключаем старую страницу и описание
        pages[currentPage].SetActive(false);
        descriptions[currentPage].gameObject.SetActive(false);

        // Устанавливаем новую страницу и описание
        currentPage = targetPage;
        pages[currentPage].SetActive(true);
        descriptions[currentPage].gameObject.SetActive(true);

        // Анимируем новую страницу: увеличиваем scale
        AnimatePageTransition(pages[currentPage], true);

        // Анимируем появление нового описания (alpha)
        yield return StartCoroutine(FadeCanvasGroup(descriptions[currentPage], 1f));

        isAnimating = false;
    }

    // Анимация изменения размера страницы (scale)
    void AnimatePageTransition(GameObject page, bool isIn)
    {
        Vector3 targetScaleValue = isIn ? targetScale : Vector3.zero;
        StartCoroutine(ScaleTransition(page.transform, targetScaleValue));
    }

    // Корутина для анимации изменения масштаба
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

    // Анимация изменения alpha для канвас групп
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

    // Устанавливаем активность текущей страницы и её описания
    void SetPageActive(int pageIndex)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == pageIndex);
            descriptions[i].gameObject.SetActive(i == pageIndex);
        }
    }

    // Настроить эффект увеличения при наведении на кнопки
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

    // Эффект увеличения кнопки при наведении
    void OnButtonHoverEnter(Button button)
    {
        button.transform.localScale = Vector3.one * 1.1f;
    }

    // Эффект восстановления размера кнопки при уходе курсора
    void OnButtonHoverExit(Button button)
    {
        button.transform.localScale = Vector3.one;
    }
}

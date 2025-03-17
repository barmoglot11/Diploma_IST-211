using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class BestiaryController : MonoBehaviour
{
    // ������� ������ � ����� �������� � ����������
    public GameObject[] pages;
    public CanvasGroup[] descriptions;

    // ������ �������
    public Button nextButton;
    public Button prevButton;

    // ���� ��������������
    public AudioSource pageFlipSound;

    // ���������� ��� ��������
    private bool isAnimating = false;
    private int currentPage = 0;

    // ��������� �������� ��������
    [Header("Animation Settings")]
    public float pageTransitionDuration = 0.3f;  // ������������ �������� ������������ ��������
    public float fadeDuration = 0.3f;  // ������������ �������� ��������� � ���������

    // ������� �����
    private Vector3 targetScale = new Vector3(430f, 300f, 285f);

    void Start()
    {
        // ��������� ��������� ���������: ������������ ������ ���� � ��������
        SetPageActive(currentPage);

        // ����������� ������� � �������
        nextButton.onClick.AddListener(OnNextButtonClicked);
        prevButton.onClick.AddListener(OnPrevButtonClicked);

        // ��������� �������� ��� ��������� �� ������
        SetupButtonHoverEffect(nextButton);
        SetupButtonHoverEffect(prevButton);
    }

    // ���������� ������� �� ������ "������"
    void OnNextButtonClicked()
    {
        if (isAnimating) return;

        // ���� �� �� ��������� ��������, ������������� �� ������
        if (currentPage >= pages.Length - 1)
        {
            StartCoroutine(SwitchPage(0));
        }
        else
        {
            // ������������� �� ��������� ��������
            StartCoroutine(SwitchPage(currentPage + 1));
        }
    }

    // ���������� ������� �� ������ "�����"
    void OnPrevButtonClicked()
    {
        if (isAnimating) return;

        // ���� �� �� ������ ��������, ������������� �� ���������
        if (currentPage <= 0)
        {
            StartCoroutine(SwitchPage(pages.Length - 1));
        }
        else
        {
            // ������������� �� ���������� ��������
            StartCoroutine(SwitchPage(currentPage - 1));
        }
    }

    // �������� ������������ ��������
    IEnumerator SwitchPage(int targetPage)
    {
        isAnimating = true;

        // ������������� ���� ��������������
        pageFlipSound.Play();

        // ������� ��������� ������� ��������: ��������� scale
        AnimatePageTransition(pages[currentPage], false);

        // ��������� ������������ �������� �������� (alpha)
        yield return StartCoroutine(FadeCanvasGroup(descriptions[currentPage], 0f));

        // ������� ��������� ��������
        yield return new WaitForSeconds(fadeDuration); // ����� ���������

        // ��������� ������ �������� � ��������
        pages[currentPage].SetActive(false);
        descriptions[currentPage].gameObject.SetActive(false);

        // ������������� ����� �������� � ��������
        currentPage = targetPage;
        pages[currentPage].SetActive(true);
        descriptions[currentPage].gameObject.SetActive(true);

        // ��������� ����� ��������: ����������� scale
        AnimatePageTransition(pages[currentPage], true);

        // ��������� ��������� ������ �������� (alpha)
        yield return StartCoroutine(FadeCanvasGroup(descriptions[currentPage], 1f));

        isAnimating = false;
    }

    // �������� ��������� ������� �������� (scale)
    void AnimatePageTransition(GameObject page, bool isIn)
    {
        Vector3 targetScaleValue = isIn ? targetScale : Vector3.zero;
        StartCoroutine(ScaleTransition(page.transform, targetScaleValue));
    }

    // �������� ��� �������� ��������� ��������
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

    // �������� ��������� alpha ��� ������ �����
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

    // ������������� ���������� ������� �������� � � ��������
    void SetPageActive(int pageIndex)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == pageIndex);
            descriptions[i].gameObject.SetActive(i == pageIndex);
        }
    }

    // ��������� ������ ���������� ��� ��������� �� ������
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

    // ������ ���������� ������ ��� ���������
    void OnButtonHoverEnter(Button button)
    {
        button.transform.localScale = Vector3.one * 1.1f;
    }

    // ������ �������������� ������� ������ ��� ����� �������
    void OnButtonHoverExit(Button button)
    {
        button.transform.localScale = Vector3.one;
    }
}

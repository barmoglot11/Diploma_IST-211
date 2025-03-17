using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class GradientButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image buttonImage;

    [Header("��������� �������")]
    [Tooltip("������, ������� ����� ������������ ��� ���������")]
    public Sprite hoverSprite; // ������ ��� ���������

    [Header("��������� ��������")]
    [Tooltip("������������ �������� � ��������")]
    public float fadeDuration = 0.5f; // ������������ �������� ��������

    [Tooltip("������� ������ ��� ���������")]
    public Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1.1f); // ���������� �� 10%

    private Coroutine fadeCoroutine; // ��� ���������� ���������
    private Vector3 normalScale; // ������� ������� ������

    void Start()
    {
        // �������� ��������� Image ������
        buttonImage = GetComponent<Image>();

        // ������������� ���������� ���� �� ���������
        buttonImage.color = new Color(1, 1, 1, 0); // ���������� ����
        buttonImage.sprite = null; // ������� ������ �� ���������

        // ��������� ������� ������� ������
        normalScale = transform.localScale;
    }

    // ��� ��������� �������
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSprite != null)
        {
            // ������������� ���������� ��������, ���� ��� ����
            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);

            // ��������� ������� ��������� ������� � ���������� ��������
            fadeCoroutine = StartCoroutine(FadeAndScale(hoverSprite, 1f, hoverScale)); // 1f = ��������������
        }
    }

    // ��� ����� �������
    public void OnPointerExit(PointerEventData eventData)
    {
        // ������������� ���������� ��������, ���� ��� ����
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        // ��������� ������� ������������ ������� � ������� � �������� ��������
        fadeCoroutine = StartCoroutine(FadeAndScale(null, 0f, normalScale)); // 0f = ������������
    }

    // �������� ��� �������� ��������� �������, ������������ � ��������
    private IEnumerator FadeAndScale(Sprite targetSprite, float targetAlpha, Vector3 targetScale)
    {
        float elapsedTime = 0f;
        Color startColor = buttonImage.color;
        Sprite startSprite = buttonImage.sprite;
        Vector3 startScale = transform.localScale;

        while (elapsedTime < fadeDuration)
        {
            // ������ �������� ������������
            float newAlpha = Mathf.Lerp(startColor.a, targetAlpha, elapsedTime / fadeDuration);
            buttonImage.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);

            // ������ ������ ������
            if (targetSprite != null)
            {
                buttonImage.sprite = targetSprite;
            }

            // ������ �������� �������
            transform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / fadeDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ��������, ��� �������� �������� ����������� �����
        buttonImage.color = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
        buttonImage.sprite = targetSprite;
        transform.localScale = targetScale;
    }
}
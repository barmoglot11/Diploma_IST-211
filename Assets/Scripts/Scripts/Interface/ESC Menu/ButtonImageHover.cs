using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonImageHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Settings")]
    public Image targetImage; // ����������� ������ ������
    public Sprite normalSprite; // ������� ������
    public Sprite hoveredSprite; // ������ ��� ���������
    public float fadeSpeed = 1f; // �������� ��������

    private RectTransform buttonRect; // RectTransform ������
    private bool isHovered = false; // ���� ��������� �������
    private Canvas canvas; // Canvas, � �������� ����������� ������
    private Camera canvasCamera; // ������, ������������ ��� ���������� Canvas

    private void Start()
    {
        // �������� RectTransform ������
        buttonRect = GetComponent<RectTransform>();

        // �������� Canvas � ������, ������������ ��� ����������
        canvas = GetComponentInParent<Canvas>();
        if (canvas != null)
        {
            canvasCamera = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;
        }

        // ������������� ��������� ������
        if (targetImage != null)
        {
            targetImage.sprite = normalSprite;
            targetImage.color = new Color(1, 1, 1, 1); // ������ ��������������
        }
    }

    private void Update()
    {
        // ���� ������ ������, ���������, ��������� �� �� � �������� ������ � ������ ��������
        if (isHovered)
        {
            Vector2 localCursor;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(buttonRect, Input.mousePosition, canvasCamera, out localCursor);

            // ���������, ��������� �� ������ � �������� RectTransform ������
            if (buttonRect.rect.Contains(localCursor))
            {
                // ������ ������ ������ �� hoveredSprite
                StartCoroutine(FadeTo(hoveredSprite, fadeSpeed));
            }
            else
            {
                // ������ ������������ � normalSprite
                StartCoroutine(FadeTo(normalSprite, fadeSpeed));
            }
        }
    }

    // ���������� ������� ��������� �������
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
    }

    // ���������� ������� ����� �������
    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        StartCoroutine(FadeTo(normalSprite, fadeSpeed));
    }

    // ������� ��������� ������� � ��������������
    private IEnumerator FadeTo(Sprite newSprite, float speed)
    {
        if (targetImage == null) yield break;

        float elapsed = 0f;
        Color startColor = targetImage.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        // ������ ������
        targetImage.sprite = newSprite;

        // ������ �������� ��������������
        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * speed;
            targetImage.color = Color.Lerp(startColor, endColor, elapsed);
            yield return null;
        }

        targetImage.color = endColor;
    }
}
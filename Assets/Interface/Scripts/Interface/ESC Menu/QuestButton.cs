using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class QuestButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Settings")]
    public Button button; // ������ �� ��������� Button
    public Sprite normalSprite; // ������� ������ ������
    public Sprite hoveredSprite; // ������ ��� ���������
    public Sprite clickedSprite; // ������ ��� �������
    public float fadeSpeed = 1f; // �������� �������� ��������� ��������������

    [Header("Other Buttons")]
    public QuestButton[] otherButtons; // ������ ������ ������ � ������

    private bool isClicked = false; // ����, �����������, ���� �� ������ ������

    private void Start()
    {
        // ������������� ��������� ������
        if (button != null && button.image != null)
        {
            button.image.sprite = normalSprite;
        }

        // ��������� ���������� ������� �������
        if (button != null)
        {
            button.onClick.AddListener(OnPointerClick);
        }
    }

    // ���������� ������� ��������� �������
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isClicked)
        {
            // ������� ��������� ������� ��� ���������
            StartCoroutine(FadeTo(hoveredSprite, fadeSpeed));
        }
    }

    // ���������� ������� ����� �������
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isClicked)
        {
            // ������� ����������� � �������� ������� ��� ����� �������
            StartCoroutine(FadeTo(normalSprite, fadeSpeed));
        }
    }

    // ���������� ������� ������� �� ������
    public void OnPointerClick()
    {
        if (!isClicked)
        {
            // ������� ��������� ������� ��� �������
            StartCoroutine(FadeTo(clickedSprite, fadeSpeed));
            isClicked = true;

            // ���������� ��������� ������ � �������� ���������
            foreach (var otherButton in otherButtons)
            {
                if (otherButton != this)
                {
                    otherButton.ResetButton();
                }
            }
        }
    }

    // ����� ������ � �������� ���������
    public void ResetButton()
    {
        isClicked = false;
        StartCoroutine(FadeTo(normalSprite, fadeSpeed));
    }

    // ������� ��������� ������� � ��������������
    private IEnumerator FadeTo(Sprite newSprite, float speed)
    {
        if (button == null || button.image == null) yield break;

        float elapsed = 0f;
        Color startColor = button.image.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        // ������ ������
        button.image.sprite = newSprite;

        // ������ �������� ��������������
        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * speed;
            button.image.color = Color.Lerp(startColor, endColor, elapsed);
            yield return null;
        }

        button.image.color = endColor;
    }
}
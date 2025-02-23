using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TextHoverColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI textComponent; // ��������� TextMeshProUGUI
    private Color normalColor = Color.white; // ������� ���� ������ (�����)
    private Color hoverColor = Color.black; // ���� ������ ��� ��������� (������)

    void Start()
    {
        // �������� ��������� TextMeshProUGUI
        textComponent = GetComponent<TextMeshProUGUI>();

        // ������������� ��������� ���� ������
        textComponent.color = normalColor;
    }

    // ��� ��������� �������
    public void OnPointerEnter(PointerEventData eventData)
    {
        // ������ ���� ������ �� ������
        textComponent.color = hoverColor;
    }

    // ��� ����� �������
    public void OnPointerExit(PointerEventData eventData)
    {
        // ���������� ���� ������ � ������
        textComponent.color = normalColor;
    }
}
using UnityEngine;
using TMPro; // �� �������� ���������� ������������ ���� ��� TextMeshPro

public class RandomTextSelector : MonoBehaviour
{
    [Header("���������")]
    [Tooltip("������ ������� ��� ������")]
    public string[] texts; // ������ �������, ������� ����� ������ � ����������

    [Tooltip("��������� TextMeshPro ��� ����������� ������")]
    public TextMeshProUGUI textComponent; // ������ �� ��������� TMP

    void Start()
    {
        // ���������, ���� �� ��������� TMP
        if (textComponent == null)
        {
            Debug.LogError("��������� TextMeshProUGUI �� ��������!");
            return;
        }

        // ���������, ���� �� ������ � �������
        if (texts == null || texts.Length == 0)
        {
            Debug.LogError("������ ������� ����!");
            return;
        }

        // �������� ��������� ����� � ���������� ���
        SelectRandomText();
    }

    // ����� ��� ������ ���������� ������
    public void SelectRandomText()
    {
        // �������� ��������� ������ �� �������
        int randomIndex = Random.Range(0, texts.Length);

        // ������������� ����� � ��������� TMP
        textComponent.text = texts[randomIndex];
    }
}
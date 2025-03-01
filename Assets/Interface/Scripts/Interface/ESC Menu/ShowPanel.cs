using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowPanel : MonoBehaviour
{
    [Header("Settings")]
    public Button button; // ������, ������� ���������� ������
    public Image panel; // ������, ������� ����� ����������
    public float fadeSpeed = 1f; // �������� ��������

    private void Start()
    {
        // ���������, ��������� �� ������ � ������
        if (button == null || panel == null)
        {
            Debug.LogError("Button or Panel is not assigned in the inspector!");
            return;
        }

        // ���������� �������� ������
        // panel.gameObject.SetActive(false);
        // panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 0);

        // ��������� ���������� ������� ������� �� ������
        button.onClick.AddListener(OnButtonClick);
    }

    // ���������� ������� �� ������
    private void OnButtonClick()
    {
        StartCoroutine(FadeInPanel());
    }

    // �������� ��������� ������
    private IEnumerator FadeInPanel()
    {
        panel.gameObject.SetActive(true);

        float elapsed = 0f;
        Color startColor = panel.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f); // ������ ��������������

        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * fadeSpeed;
            panel.color = Color.Lerp(startColor, endColor, elapsed);
            yield return null;
        }

        panel.color = endColor;
    }
}
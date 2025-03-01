using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OpenClosePanel : MonoBehaviour
{
    [Header("Settings")]
    public CanvasGroup panel; // ������, ������� ����� ����������� � �����������
    public Button openButton; // ������ ��� �������� ������
    public Button closeButton; // ������ ��� �������� ������
    public float fadeSpeed = 1f; // �������� ��������

    private void Start()
    {
        // ���������, ��������� �� ������ � ������
        if (panel == null || openButton == null || closeButton == null)
        {
            Debug.LogError("Panel or buttons are not assigned in the inspector!");
            return;
        }

        // ���������� �������� ������
        panel.alpha = 0;
        panel.gameObject.SetActive(false);

        // ��������� ����������� ������� ��� ������
        openButton.onClick.AddListener(OpenPanel);
        closeButton.onClick.AddListener(ClosePanel);
    }

    // ������� ������
    private void OpenPanel()
    {
        StopAllCoroutines(); // ������������� ��� ��������
        StartCoroutine(FadeIn());
    }

    // ������� ������
    private void ClosePanel()
    {
        StopAllCoroutines(); // ������������� ��� ��������
        StartCoroutine(FadeOut());
    }

    // �������� �������� (���������� ��������������)
    private IEnumerator FadeIn()
    {
        panel.gameObject.SetActive(true);

        float elapsed = 0f;
        float startAlpha = panel.alpha;
        float endAlpha = 1f; // ������ ��������������

        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * fadeSpeed;
            panel.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed);
            yield return null;
        }

        panel.alpha = endAlpha;
    }

    // �������� �������� (���������� ��������������)
    private IEnumerator FadeOut()
    {
        float elapsed = 0f;
        float startAlpha = panel.alpha;
        float endAlpha = 0f; // ������ ������������

        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * fadeSpeed;
            panel.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed);
            yield return null;
        }

        panel.alpha = endAlpha;
        panel.gameObject.SetActive(false);
    }
}
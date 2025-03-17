using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowPanel : MonoBehaviour
{
    [Header("Settings")]
    public Button button; // ������, ������� ���������� ������
    public CanvasGroup panelCanvasGroup; // CanvasGroup, ������� ����� ��������� ������������� ������
    public float fadeSpeed = 1f; // �������� ��������

    private void Start()
    {
        // ���������, ��������� �� ������ � CanvasGroup
        if (button == null || panelCanvasGroup == null)
        {
            Debug.LogError("Button or CanvasGroup is not assigned in the inspector!");
            return;
        }

        // ���������� �������� ������
        panelCanvasGroup.alpha = 0;
        panelCanvasGroup.interactable = false;
        panelCanvasGroup.blocksRaycasts = false;

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
        // �������� ������ ������ (�����������)
        panelCanvasGroup.gameObject.SetActive(true);
        panelCanvasGroup.interactable = true;
        panelCanvasGroup.blocksRaycasts = true;

        float elapsed = 0f;
        float startAlpha = panelCanvasGroup.alpha;
        float endAlpha = 1f; // ������ ��������������

        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * fadeSpeed;
            panelCanvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed);
            yield return null;
        }

        panelCanvasGroup.alpha = endAlpha;
    }
}
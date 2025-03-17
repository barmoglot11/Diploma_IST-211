using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonPanelManager : MonoBehaviour
{
    [Header("Settings")]
    public Button[] buttons; // ������ ������
    public CanvasGroup[] panels; // ������ ������� (���������� CanvasGroup ������ Image)
    public float fadeSpeed = 5f; // �������� ��������

    private void Start()
    {
        // ��������� ����������� ������� ��� ������ ������
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // ��������� ���������� ��� ���������
            buttons[i].onClick.AddListener(() => OnButtonClick(index));
        }

        // ���������� �������� ��� ������
        // foreach (var panel in panels)
        // {
            // if (panel != null)
            // {
                // panel.gameObject.SetActive(false);
                // panel.alpha = 0; // ������ ������������
           // }
        // }
    }

    // ���������� ������� �� ������
    private void OnButtonClick(int buttonIndex)
    {
        // ��������� ������� �������� ���� �������, ����� �������� ���������
        StartCoroutine(CloseAllPanelsThenOpen(buttonIndex));
    }

    // ������� ��� ������, ����� ������� ���������
    private IEnumerator CloseAllPanelsThenOpen(int panelIndexToOpen)
    {
        // ��������� ��� ������
        foreach (var panel in panels)
        {
            if (panel != null && panel.gameObject.activeSelf)
            {
                yield return StartCoroutine(FadeOut(panel)); // ��� ���������� �������� ������������
            }
        }

        // ��������� ��������� ������
        if (panelIndexToOpen >= 0 && panelIndexToOpen < panels.Length && panels[panelIndexToOpen] != null)
        {
            yield return StartCoroutine(FadeIn(panels[panelIndexToOpen])); // ��� ���������� �������� ���������
        }
    }

    // �������� ��������� (���������� ��������������)
    private IEnumerator FadeIn(CanvasGroup panel)
    {
        if (panel == null) yield break;

        panel.gameObject.SetActive(true);
        float elapsed = 0f;
        float startAlpha = 0; // �������� � ������������ 0
        float endAlpha = 1; // ������ ��������������

        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * fadeSpeed;
            panel.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed);
            yield return null;
        }

        panel.alpha = endAlpha;
    }

    // �������� ������������ (���������� ��������������)
    private IEnumerator FadeOut(CanvasGroup panel)
    {
        if (panel == null) yield break;

        float elapsed = 0f;
        float startAlpha = panel.alpha;
        float endAlpha = 0; // ������ ������������

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
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowPanelOnClick : MonoBehaviour
{
    [Header("��������� ������")]
    [Tooltip("������, ������� ����� ����������")]
    public GameObject panel; // ������, ������� ����� ��������

    [Header("��������� ��������")]
    [Tooltip("������������ �������� � ��������")]
    public float fadeDuration = 1f; // ������������ �������� ���������

    private CanvasGroup panelCanvasGroup; // ��������� ��� ���������� �������������
    private Button button; // ��������� ������

    void Start()
    {
        // �������� ��������� ������
        button = GetComponent<Button>();

        // ��������� ����� ��������� �������
        button.onClick.AddListener(OnButtonClick);

        // �������� ��� ��������� CanvasGroup � ������
        if (panel != null)
        {
            panelCanvasGroup = panel.GetComponent<CanvasGroup>();
            if (panelCanvasGroup == null)
            {
                panelCanvasGroup = panel.AddComponent<CanvasGroup>();
            }

            // �������� ������ � ������
            panelCanvasGroup.alpha = 0f;
            panelCanvasGroup.interactable = false;
            panelCanvasGroup.blocksRaycasts = false;
            panel.SetActive(false); // ��������� ������
        }
    }

    // ���������� ������� �� ������
    private void OnButtonClick()
    {
        if (panel != null)
        {
            // �������� ������ � ��������� ��������
            panel.SetActive(true);
            StartCoroutine(FadeIn());
        }
    }

    // �������� ��� �������� ��������� ������
    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        // �������� �������������� � �������
        panelCanvasGroup.interactable = true;
        panelCanvasGroup.blocksRaycasts = true;

        while (elapsedTime < fadeDuration)
        {
            // ������ �������� ������������
            float newAlpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            panelCanvasGroup.alpha = newAlpha;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ��������, ��� �������� ������������ ����������� �����
        panelCanvasGroup.alpha = 1f;
    }
}
using UnityEngine;
using UnityEngine.UI;

public class IdlePanelController : MonoBehaviour
{
    public CanvasGroup panelCanvasGroup; // ������ �� CanvasGroup ������
    public float fadeDuration = 1.0f; // ������������ �������� ���������/������������
    public float idleTimeToShow = 5.0f; // ����� ����������� ��� ��������� ������

    private float idleTimer = 0f; // ������ �����������
    private bool isPanelVisible = false; // ����, �����������, ����� �� ������
    private bool isFading = false; // ����, �����������, ���� �� ������� ��������� ������������

    void Update()
    {
        // ���������, ���� �� �����-���� �������� ������������
        if (Input.anyKeyDown || Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0 || Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            idleTimer = 0f; // ���������� ������ �����������

            // ���� ������ �����, �������� �������� �
            if (isPanelVisible && !isFading)
            {
                StartCoroutine(FadePanel(0f)); // ������ �������� ������
            }
        }
        else
        {
            // ����������� ������ �����������
            idleTimer += Time.deltaTime;

            // ���� ����� ����������� ��������� ����� � ������ �� �����, �������� ���������� �
            if (idleTimer >= idleTimeToShow && !isPanelVisible && !isFading)
            {
                StartCoroutine(FadePanel(1f)); // ������ ���������� ������
            }
        }
    }

    private System.Collections.IEnumerator FadePanel(float targetAlpha)
    {
        isFading = true;

        float startAlpha = panelCanvasGroup.alpha;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            panelCanvasGroup.alpha = alpha; // ������ �������� ������������ CanvasGroup
            yield return null;
        }

        // ������������� �������� �������� ������������
        panelCanvasGroup.alpha = targetAlpha;

        // ��������� �����
        isPanelVisible = (targetAlpha == 1f);
        isFading = false;
    }
}
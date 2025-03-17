using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TogglePanelOnE : MonoBehaviour
{
    public CanvasGroup canvas1; // ������ CanvasGroup
    public CanvasGroup canvas2; // ������ CanvasGroup
    public float fadeDuration = 1f; // ������������ �������� ��������
    public MonoBehaviour cameraScript; // ������, ����������� ������� (��������, ���������� �������� ������)

    private bool isCanvas1Visible = false;
    private bool isFading = false; // ���� ��� ������������ ���������� �������

    void Update()
    {
        if (Input.GetKey(KeyCode.E) && !isFading) // ���������, �� ����������� �� ��� ��������
        {
            ToggleCanvases();
        }
    }

    void ToggleCanvases()
    {
        isCanvas1Visible = !isCanvas1Visible;

        if (isCanvas1Visible)
        {
            StartCoroutine(FadeCanvas(canvas1, 1f, true)); // ������� ��������� canvas1
            StartCoroutine(FadeCanvas(canvas2, 0f, false)); // ������� ������������ canvas2
            Cursor.visible = true; // �������� ������
            Cursor.lockState = CursorLockMode.None; // ������������ ������

            // ��������� ������, ����������� �������
            if (cameraScript != null)
            {
                cameraScript.enabled = false; // ��������� ������
            }
        }
    }

    private System.Collections.IEnumerator FadeCanvas(CanvasGroup canvasGroup, float targetAlpha, bool activateAfterFade)
    {
        isFading = true; // ������������� ����, ��� �������� ��������

        // ���� targetAlpha > 0, ���������� ������ ����� ������� ��������
        if (targetAlpha > 0)
        {
            canvasGroup.gameObject.SetActive(true);
        }

        float startAlpha = canvasGroup.alpha;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;

        // ���� targetAlpha == 0, ������������ ������ ����� ���������� ��������
        if (targetAlpha == 0)
        {
            canvasGroup.gameObject.SetActive(false);
        }

        // ���������� ��� ������������ ������ � ����������� �� ��������� activateAfterFade
        if (activateAfterFade)
        {
            canvasGroup.gameObject.SetActive(true);
        }
        else
        {
            canvasGroup.gameObject.SetActive(false);
        }

        isFading = false; // ���������� ����, ��� �������� ���������
    }
}

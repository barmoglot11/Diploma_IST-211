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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleCanvases();
        }
    }

    void ToggleCanvases()
    {
        isCanvas1Visible = !isCanvas1Visible;

        if (isCanvas1Visible)
        {
            StartCoroutine(FadeCanvas(canvas1, 1f)); // ������� ��������� canvas1
            StartCoroutine(FadeCanvas(canvas2, 0f)); // ������� ������������ canvas2
            Cursor.visible = true; // �������� ������
            Cursor.lockState = CursorLockMode.None; // ������������ ������

            // ��������� ������, ����������� �������
            if (cameraScript != null)
            {
                cameraScript.enabled = false; // ��������� ������
            }
        }
        else
        {
            StartCoroutine(FadeCanvas(canvas1, 0f)); // ������� ������������ canvas1
            StartCoroutine(FadeCanvas(canvas2, 1f)); // ������� ��������� canvas2
            Cursor.visible = false; // ��������� ������
            Cursor.lockState = CursorLockMode.Locked; // ��������� ������

            // �������� ������, ����������� �������
            if (cameraScript != null)
            {
                cameraScript.enabled = true; // �������� ������
            }
        }
    }

    private System.Collections.IEnumerator FadeCanvas(CanvasGroup canvasGroup, float targetAlpha)
    {
        float startAlpha = canvasGroup.alpha;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseItemDescription : MonoBehaviour
{
    public CanvasGroup canvasGroupToHide; // CanvasGroup, ������� ����� ������
    public CanvasGroup canvasGroupToShow; // CanvasGroup, ������� ����� ��������
    public float fadeDuration = 1f; // ������������ �������� ������������/���������
    public MonoBehaviour cameraScript; // ������, ����������� �������
    public Button closeButton; // ������, ������� ��������� ������

    private bool isFading = false; // ����, ����� �������� ������������� �������

    private void Start()
    {
        // ���������, ��������� �� ������
        if (closeButton != null)
        {
            // ������������� �� ������� ������� ������
            closeButton.onClick.AddListener(StartCloseDescription);
        }
        else
        {
            Debug.LogWarning("������ �� ��������� � ����������!");
        }
    }

    public void StartCloseDescription()
    {
        if (!isFading)
        {
            StartCoroutine(FadeOutAndSwitch());
        }
    }

    private System.Collections.IEnumerator FadeOutAndSwitch()
    {
        isFading = true;

        // ������� ������������ �������� CanvasGroup
        float elapsedTime = 0f;
        float startAlpha = canvasGroupToHide.alpha;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroupToHide.alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroupToHide.alpha = 0f; // ��������, ��� alpha = 0

        // �������� ������ CanvasGroup � ������ ��� ����������
        if (canvasGroupToShow != null)
        {
            canvasGroupToShow.gameObject.SetActive(true);
            elapsedTime = 0f;
            startAlpha = canvasGroupToShow.alpha;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                canvasGroupToShow.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
                yield return null;
            }

            canvasGroupToShow.alpha = 1f; // ��������, ��� alpha = 1
        }

        // �������� �������� ������
        if (cameraScript != null)
        {
            cameraScript.enabled = true;
            Debug.Log("������ ������ �������: " + cameraScript.enabled);
        }

        // ���������� ��������
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("������ ����� � ������������.");
        canvasGroupToHide.gameObject.SetActive(false); // ������������ ������
        isFading = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeCanvasController : MonoBehaviour
{
    public CanvasGroup[] canvasGroups; // ������ CanvasGroup, ������� ����� ��������/������
    public CanvasGroup panelCanvasGroup; // CanvasGroup ������, ������� ����� �������������
    public float fadeDuration = 1.0f; // ������������ �������� ���������/������������
    public float panelFadeDuration = 0.5f; // ������������ �������� ������

    private bool isPlayerOnCube = false; // ����, �����������, ��������� �� ����� �� ������

    void Start()
    {
        // �������� ��� CanvasGroup � ������ ��� ������
        foreach (var canvasGroup in canvasGroups)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        if (panelCanvasGroup != null)
        {
            panelCanvasGroup.gameObject.SetActive(false); // ������ ���������� ���������
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // ���������, ��� �� ����� ����� �����
        {
            isPlayerOnCube = true;
            StartCoroutine(ShowCanvasAndPanel());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // ���������, ��� ����� ���� � ������
        {
            isPlayerOnCube = false;
            StartCoroutine(HideCanvasAndPanel());
        }
    }

    private IEnumerator ShowCanvasAndPanel()
    {
        // ������� ��������� CanvasGroup
        foreach (var canvasGroup in canvasGroups)
        {
            StartCoroutine(FadeCanvasGroup(canvasGroup, 1f));
        }

        // �������� ������ � ������ ���������� �
        if (panelCanvasGroup != null)
        {
            panelCanvasGroup.gameObject.SetActive(true); // �������� ������
            yield return StartCoroutine(FadeCanvasGroup(panelCanvasGroup, 1f, panelFadeDuration));
        }
    }

    private IEnumerator HideCanvasAndPanel()
    {
        // ������� ������������ ������ ����� alpha
        if (panelCanvasGroup != null)
        {
            yield return StartCoroutine(FadeCanvasGroup(panelCanvasGroup, 0f, panelFadeDuration));
            panelCanvasGroup.gameObject.SetActive(false); // ��������� ������ ����� ���������� ��������
        }

        // ������� ������������ CanvasGroup
        foreach (var canvasGroup in canvasGroups)
        {
            StartCoroutine(FadeCanvasGroup(canvasGroup, 0f));
        }
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float targetAlpha, float duration = -1f)
    {
        if (duration < 0)
        {
            duration = fadeDuration; // ���������� fadeDuration �� ���������
        }

        float startAlpha = canvasGroup.alpha;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            canvasGroup.alpha = alpha;
            yield return null;
        }

        canvasGroup.alpha = targetAlpha; // ��������, ��� ������������ ����������� �����
        canvasGroup.interactable = (targetAlpha == 1f); // ������ �������� �������������� ������ ��� ������ ���������
        canvasGroup.blocksRaycasts = (targetAlpha == 1f); // ��������� Raycasts, ���� CanvasGroup �����
    }
}

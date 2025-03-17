using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeOutCanvasWithDelay : MonoBehaviour
{
    public CanvasGroup canvasGroup; // CanvasGroup, ������� ����� ������ ��������
    public float fadeDuration = 1f; // ������������ �������� ������������
    public float startDelay = 1f; // �������� ����� ������� ��������

    void Start()
    {
        // ���������, �������� �� CanvasGroup
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup �� ��������!");
            return;
        }

        // ��������� �������� ��� �������� ������������
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        // �������� ����� ������� ��������
        yield return new WaitForSeconds(startDelay);

        float elapsedTime = 0f;
        float startAlpha = canvasGroup.alpha;

        // ������� ��������� ������������
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        // ��������, ��� ������������ ����� 0
        canvasGroup.alpha = 0f;

        // ��������� Canvas (�����������)
        canvasGroup.gameObject.SetActive(false);
    }
}
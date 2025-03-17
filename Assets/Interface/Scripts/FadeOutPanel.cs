using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeOutPanel : MonoBehaviour
{
    public Button hideButton;      // ������, �� ������� �� ������� ����� ���������� ������
    public CanvasGroup panel;      // ������, ������� ����� ������
    public float fadeDuration = 0.5f; // ������������ ��������

    void Start()
    {
        hideButton.onClick.AddListener(() => StartCoroutine(FadeOut()));
    }

    IEnumerator FadeOut()
    {
        // hideButton.interactable = false; // ��������� ����������� �������
        float elapsedTime = 0f;
        float startAlpha = panel.alpha;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            panel.alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        panel.alpha = 0f;
        panel.gameObject.SetActive(false);
    }
}

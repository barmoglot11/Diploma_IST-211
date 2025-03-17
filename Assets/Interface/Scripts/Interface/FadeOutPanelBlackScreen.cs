using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Video;

public class FadeOutPanelBlackScreen : MonoBehaviour
{
    public Button hideButton;      // ������, �� ������� �� ������� ����� ���������� ������
    public CanvasGroup panel;      // ������, ������� ����� ������
    public CanvasGroup blackScreen; // ������ ����� (������ ���� �������� � �����)
    public float fadeDuration = 0.5f; // ������������ ��������

    [Header("�������������� �������")]
    public GameObject objectToActivate; // ������, ������� ����� ������������ ����� ������� ������
    public Camera cameraToDisable;      // ������, ������� ����� ������
    public VideoPlayer videoPlayerToStop; // Video Player, ������� ����� ����������

    void Start()
    {
        // ������������� ������� ������ (���� �� �� �������� � ����������)
        if (blackScreen != null)
        {
            blackScreen.alpha = 0f; // ��������� ������������ ������� ������
            blackScreen.gameObject.SetActive(false); // �������� ������ ����� � ������
        }

        // ��������� ���������� ������� �� ������
        hideButton.onClick.AddListener(() => StartCoroutine(FadeOut()));
    }

    IEnumerator FadeOut()
    {
        // ��������� ������, ����� ������������� ��������� �������
        hideButton.interactable = false;

        // ������� ��������� ������� ������
        if (blackScreen != null)
        {
            blackScreen.gameObject.SetActive(true); // ���������� ������ �����
            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                blackScreen.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
                yield return null;
            }

            blackScreen.alpha = 1f; // ��������, ��� ������ ����� ��������� �����
        }

        // ������� ������� ������
        float elapsedTimePanel = 0f;
        float startAlpha = panel.alpha;

        while (elapsedTimePanel < fadeDuration)
        {
            elapsedTimePanel += Time.deltaTime;
            panel.alpha = Mathf.Lerp(startAlpha, 0f, elapsedTimePanel / fadeDuration);
            yield return null;
        }

        // ���������� ������, ���� �� �����
        objectToActivate.gameObject.SetActive(true);

        // �������� ������, ���� ��� ������
        cameraToDisable.gameObject.SetActive(false);

        // ������������� Video Player, ���� �� �����
        videoPlayerToStop.gameObject.SetActive(false);

        panel.alpha = 0f;
        panel.gameObject.SetActive(false); // �������� ������

        // �������� ������ ������� (���� �����)
        hideButton.interactable = true;
    }
}

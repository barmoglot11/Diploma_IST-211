using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeAndSwitchScene : MonoBehaviour
{
    [Header("Settings")]
    public Button button; // ������, ������� ��������� �������� � ������������ �����
    public GameObject panel; // GameObject, ������� ����� ������ ����������
    public float fadeSpeed = 1f; // �������� ��������
    public string sceneToLoad; // ��� ����� ��� ��������

    private CanvasGroup canvasGroup; // CanvasGroup ��� ���������� ���������������

    private void Start()
    {
        // ���������, ��������� �� ������ � ������
        if (button == null || panel == null)
        {
            Debug.LogError("Button or Panel is not assigned in the inspector!");
            return;
        }

        // �������� ��� ��������� ��������� CanvasGroup
        canvasGroup = panel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = panel.AddComponent<CanvasGroup>();
        }

        // ���������� �������� ������
        canvasGroup.alpha = 0;
        panel.SetActive(false);

        // ��������� ���������� ������� ������� �� ������
        button.onClick.AddListener(OnButtonClick);
    }

    // ���������� ������� �� ������
    private void OnButtonClick()
    {
        StartCoroutine(FadeInAndSwitchScene());
    }

    // �������� ��������� ������ � ������������ �����
    private IEnumerator FadeInAndSwitchScene()
    {
        // ���������� ������
        panel.SetActive(true);

        // ������ ����������� ��������������
        float elapsed = 0f;
        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * fadeSpeed;
            canvasGroup.alpha = Mathf.Lerp(0, 1, elapsed);
            yield return null;
        }

        canvasGroup.alpha = 1; // ��������, ��� �������������� ����������� �� 100%

        // ����������� ����� ����� ���������� ��������
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("Scene to load is not specified!");
        }
    }
}
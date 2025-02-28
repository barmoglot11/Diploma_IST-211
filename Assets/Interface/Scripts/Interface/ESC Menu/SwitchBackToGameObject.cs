using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SwitchBackToGameObject : MonoBehaviour
{
    public GameObject objectToEnable; // GameObject, ������� ����� ��������
    public Canvas canvasToDisable; // Canvas, ������� ����� ���������
    public Camera cameraToDisable; // ������, ������� ����� ���������

    public float fadeSpeed = 2f; // �������� �������� ��������
    public float delayBeforeActivation = 0.5f; // �������� ����� ���������� ����� ���������

    private bool isSwitching = false; // ���� ��� ���������� �����
    private Coroutine currentCoroutine; // ������� �������� ��������

    void Update()
    {
        // ��������� ����, ���� ���� ��������
        if (isSwitching) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // ���� �������� ��� ��������, �� ��������� �����
            if (currentCoroutine != null) return;

            currentCoroutine = StartCoroutine(SwitchBackToOriginalView());
        }
    }

    private IEnumerator SwitchBackToOriginalView()
    {
        isSwitching = true; // ��������� ����

        // �������� ����� ���������� ����� ���������
        yield return new WaitForSeconds(delayBeforeActivation);

        // �������� GameObject
        if (objectToEnable != null)
        {
            objectToEnable.SetActive(true);
            CanvasGroup objectCanvasGroup = objectToEnable.GetComponent<CanvasGroup>();
            if (objectCanvasGroup == null) objectCanvasGroup = objectToEnable.AddComponent<CanvasGroup>();

            // ������� ��������� GameObject
            float alpha = 0;
            while (alpha < 1)
            {
                alpha += Time.deltaTime * fadeSpeed;
                objectCanvasGroup.alpha = alpha;
                yield return null;
            }
        }

        // ������� ���������� Canvas
        if (canvasToDisable != null)
        {
            CanvasGroup canvasGroup = canvasToDisable.GetComponent<CanvasGroup>();
            if (canvasGroup == null) canvasGroup = canvasToDisable.gameObject.AddComponent<CanvasGroup>();

            float alpha = 1;
            while (alpha > 0)
            {
                alpha -= Time.deltaTime * fadeSpeed;
                canvasGroup.alpha = alpha;
                yield return null;
            }

            canvasToDisable.gameObject.SetActive(false);
        }

        // ��������� ������
        if (cameraToDisable != null)
        {
            cameraToDisable.gameObject.SetActive(false);
        }

        isSwitching = false; // ������������ ����
        currentCoroutine = null; // ���������� ������� ��������
    }
}
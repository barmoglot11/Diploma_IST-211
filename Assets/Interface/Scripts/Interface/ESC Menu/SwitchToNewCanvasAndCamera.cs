using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SwitchToNewCanvasAndCamera : MonoBehaviour
{
    public Canvas newCanvas; // ����� Canvas, ������� ����� ��������
    public Camera newCamera; // ����� ������, ������� ����� ��������
    public GameObject objectToDisable; // GameObject, ������� ����� ���������

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

            currentCoroutine = StartCoroutine(SwitchToNewView());
        }
    }

    private IEnumerator SwitchToNewView()
    {
        isSwitching = true; // ��������� ����

        // �������� ����� ���������� ����� ���������
        yield return new WaitForSeconds(delayBeforeActivation);

        // �������� ����� Canvas � ������
        if (newCanvas != null)
        {
            newCanvas.gameObject.SetActive(true);
            CanvasGroup canvasGroup = newCanvas.GetComponent<CanvasGroup>();
            if (canvasGroup == null) canvasGroup = newCanvas.gameObject.AddComponent<CanvasGroup>();

            // ������� ��������� ������ Canvas
            float alpha = 0;
            while (alpha < 1)
            {
                alpha += Time.deltaTime * fadeSpeed;
                canvasGroup.alpha = alpha;
                yield return null;
            }
        }

        if (newCamera != null)
        {
            newCamera.gameObject.SetActive(true);
        }

        // ������� ���������� GameObject
        if (objectToDisable != null)
        {
            CanvasGroup objectCanvasGroup = objectToDisable.GetComponent<CanvasGroup>();
            if (objectCanvasGroup == null) objectCanvasGroup = objectToDisable.AddComponent<CanvasGroup>();

            float alpha = 1;
            while (alpha > 0)
            {
                alpha -= Time.deltaTime * fadeSpeed;
                objectCanvasGroup.alpha = alpha;
                yield return null;
            }

            objectToDisable.SetActive(false);
        }

        isSwitching = false; // ������������ ����
        currentCoroutine = null; // ���������� ������� ��������

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
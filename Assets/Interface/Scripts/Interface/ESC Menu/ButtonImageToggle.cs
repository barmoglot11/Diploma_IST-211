using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonImageToggle : MonoBehaviour
{
    [Header("Settings")]
    public Image appearImage; // �����������, ������� ����������
    public Image[] disappearImages; // ������ �����������, ������� ��������
    public float animationSpeed = 1f; // �������� ��������

    // private void Start()
    // {
        // ���������� �������� ��� �����������
        // if (appearImage != null)
        // {
            // appearImage.gameObject.SetActive(false);
        // }

        // foreach (var image in disappearImages)
        // {
            // if (image != null)
            // {
               // image.gameObject.SetActive(false);
            // }
        // }
    // }

    public void OnButtonClick()
    {
        // ��������� �������� ��������� � ������������
        StartCoroutine(AnimateImages());
    }

    private IEnumerator AnimateImages()
    {
        // �������� ������������ ������ �����������
        foreach (var image in disappearImages)
        {
            if (image != null && image.gameObject.activeSelf)
            {
                StartCoroutine(ScaleDown(image));
            }
        }

        // �������� ��������� ���������� �����������
        if (appearImage != null)
        {
            appearImage.gameObject.SetActive(true);
            StartCoroutine(ScaleUp(appearImage));
        }

        yield return null;
    }

    // �������� ���������� (���������)
    private IEnumerator ScaleUp(Image image)
    {
        float elapsed = 0f;
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = Vector3.one;

        image.transform.localScale = startScale;

        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * animationSpeed;
            image.transform.localScale = Vector3.Lerp(startScale, endScale, elapsed);
            yield return null;
        }

        image.transform.localScale = endScale;
    }

    // �������� ���������� (������������)
    private IEnumerator ScaleDown(Image image)
    {
        float elapsed = 0f;
        Vector3 startScale = image.transform.localScale;
        Vector3 endScale = Vector3.zero;

        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * animationSpeed;
            image.transform.localScale = Vector3.Lerp(startScale, endScale, elapsed);
            yield return null;
        }

        image.transform.localScale = endScale;
        image.gameObject.SetActive(false);
    }
}
using UnityEngine;
using UnityEngine.UI;

public class ChangeSpriteOnLAlt : MonoBehaviour
{
    public Image targetImage; // ������ �� ��������� Image, ������� ����� ��������
    public Sprite newSprite;  // ����� ������, ������� ����� ����������
    public float fadeDuration = 1.0f; // ������������ �������� ��������� ������������

    private Sprite originalSprite; // ������������ ������
    private bool isChanging = false; // ����, �����������, ���� �� ������� ���������
    private bool isDefaultSprite = true; // ����, �����������, ����� ������ ������ ����������

    void Start()
    {
        if (targetImage != null)
        {
            originalSprite = targetImage.sprite; // ��������� ������������ ������
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt) && !isChanging)
        {
            StartCoroutine(ChangeSprite());
        }
    }

    private System.Collections.IEnumerator ChangeSprite()
    {
        isChanging = true;

        // ������� ���������� ������������
        float elapsedTime = 0f;
        Color color = targetImage.color;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            targetImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        // ������ ������ �� ����� ��� ���������� ������������
        if (isDefaultSprite)
        {
            targetImage.sprite = newSprite; // ������������� ����� ������
        }
        else
        {
            targetImage.sprite = originalSprite; // ���������� ������������ ������
        }
        isDefaultSprite = !isDefaultSprite; // ����������� ����

        // ������� ���������� ������������
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            targetImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        isChanging = false;
    }
}
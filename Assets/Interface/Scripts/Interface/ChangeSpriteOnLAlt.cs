using UnityEngine;
using UnityEngine.UI;

public class ChangeSpriteOnLAlt : MonoBehaviour
{
    public Image targetImage; // Ссылка на компонент Image, который нужно изменить
    public Sprite newSprite;  // Новый спрайт, который будет установлен
    public float fadeDuration = 1.0f; // Длительность плавного изменения прозрачности

    private Sprite originalSprite; // Оригинальный спрайт
    private bool isChanging = false; // Флаг, указывающий, идет ли процесс изменения
    private bool isDefaultSprite = true; // Флаг, указывающий, какой спрайт сейчас установлен

    void Start()
    {
        if (targetImage != null)
        {
            originalSprite = targetImage.sprite; // Сохраняем оригинальный спрайт
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

        // Плавное уменьшение прозрачности
        float elapsedTime = 0f;
        Color color = targetImage.color;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            targetImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        // Меняем спрайт на новый или возвращаем оригинальный
        if (isDefaultSprite)
        {
            targetImage.sprite = newSprite; // Устанавливаем новый спрайт
        }
        else
        {
            targetImage.sprite = originalSprite; // Возвращаем оригинальный спрайт
        }
        isDefaultSprite = !isDefaultSprite; // Переключаем флаг

        // Плавное увеличение прозрачности
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
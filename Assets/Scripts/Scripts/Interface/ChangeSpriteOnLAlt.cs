using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSpriteOnLAlt : MonoBehaviour
{
    public Image targetImage; // Целевая Image, чей спрайт будет изменяться
    public Sprite newSprite;  // Новый спрайт, который будет установлен
    public float fadeDuration = 1.0f; // Продолжительность эффекта затухания

    private Sprite originalSprite; // Исходный спрайт
    private bool isChanging = false; // Флаг, указывающий, идет ли изменение спрайта
    public bool isDefaultSprite = true; // Флаг, указывающий, используется ли исходный спрайт

    public bool IsEndedChanging = true;
    
    private Coroutine _fadeCoroutine;
    
    void Start()
    {
        if (targetImage != null)
        {
            originalSprite = targetImage.sprite; // Сохраняем исходный спрайт
        }
    }

    private void OnDisable()
    {
        if (_fadeCoroutine != null)
        {
            StopCoroutine(_fadeCoroutine);
            _fadeCoroutine = null;
        }

        isChanging = false;
        IsEndedChanging = true;

        if (targetImage != null)
        {
            // Восстанавливаем спрайт и альфа-канал
            targetImage.sprite = isDefaultSprite ? originalSprite : newSprite;
            var color = targetImage.color;
            targetImage.color = new Color(color.r, color.g, color.b, 1f);
        }
    }


    
    public void StartChangeSprite()
    {
        // Проверяем нажатие клавиши Left Alt и не идет ли изменение спрайта
        if (!isChanging)
        {
            _fadeCoroutine = StartCoroutine(ChangeSprite());
        }
    }

    private IEnumerator ChangeSprite()
    {
        if (targetImage == null || !targetImage.gameObject.activeInHierarchy)
            yield break;

        isChanging = true;
        IsEndedChanging = false;

        try
        {
            // Начинаем затухание
            float elapsedTime = 0f;
            Color originalColor = targetImage.color;

            while (elapsedTime < fadeDuration)
            {
                if (targetImage == null || !targetImage.gameObject.activeInHierarchy)
                    break;

                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
                targetImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                yield return null;
            }

            // Меняем спрайт, если объект ещё активен
            if (targetImage != null && targetImage.gameObject.activeInHierarchy)
            {
                targetImage.sprite = isDefaultSprite ? newSprite : originalSprite;
                isDefaultSprite = !isDefaultSprite;
            }

            // Начинаем появление
            elapsedTime = 0f;
            while (elapsedTime < fadeDuration)
            {
                if (targetImage == null || !targetImage.gameObject.activeInHierarchy)
                    break;

                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
                targetImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                yield return null;
            }
        }
        finally
        {
            // Гарантированно сбрасываем флаги, даже если корутина прервана
            if (this != null) // Проверка, если объект не уничтожен
            {
                isChanging = false;
                IsEndedChanging = true;
                _fadeCoroutine = null;
                ChangeSprite(isDefaultSprite);
            }
        }
    }

    public void ChangeSprite(bool state)
    {
        if (targetImage == null)
            return;
    
        targetImage.sprite = state ? newSprite : originalSprite;
        isDefaultSprite = !isDefaultSprite;
    
        var color = targetImage.color;
        targetImage.color = new Color(color.r, color.g, color.b, 1);
    }

}

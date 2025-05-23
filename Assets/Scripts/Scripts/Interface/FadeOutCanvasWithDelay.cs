using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeOutCanvasWithDelay : MonoBehaviour
{
    public CanvasGroup canvasGroup; // CanvasGroup, который будет подвергаться затуханию
    public float fadeDuration = 1f; // Продолжительность затухания
    public float startDelay = 1f; // Задержка перед началом затухания

    public bool IsFading => _isFading;
    private bool _isFading;
    
    private bool isFadeIn = false;

    public void StartFade()
    {
        // Проверяем, установлен ли CanvasGroup
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup не установлен!");
            return;
        }

        // Запускаем корутину для затухания
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        _isFading = true;
        // Ждем заданное время перед началом затухания
        yield return new WaitForSeconds(startDelay);

        if(isFadeIn)
            canvasGroup.gameObject.SetActive(true);
        
        float elapsedTime = 0f;
        float startAlpha = canvasGroup.alpha;
        float endAlpha = isFadeIn ? 1f : 0f; // Изменено: теперь корректно

        // Плавно уменьшаем альфа-канал
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            yield return null;
        }

        // Устанавливаем альфа-канал в конечное значение
        canvasGroup.alpha = endAlpha;

        // Деактивируем Canvas (скрываем его) если затухание завершено
        if(!isFadeIn)
            canvasGroup.gameObject.SetActive(false);
        isFadeIn = !isFadeIn; // Переключаем состояние
        _isFading = false;
    }
}
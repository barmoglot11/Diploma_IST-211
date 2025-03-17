using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeOutCanvasWithDelay : MonoBehaviour
{
    public CanvasGroup canvasGroup; // CanvasGroup, который будет плавно исчезать
    public float fadeDuration = 1f; // Длительность анимации исчезновения
    public float startDelay = 1f; // Задержка перед началом анимации

    void Start()
    {
        // Проверяем, назначен ли CanvasGroup
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup не назначен!");
            return;
        }

        // Запускаем корутину для плавного исчезновения
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        // Задержка перед началом анимации
        yield return new WaitForSeconds(startDelay);

        float elapsedTime = 0f;
        float startAlpha = canvasGroup.alpha;

        // Плавное изменение прозрачности
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        // Убедимся, что прозрачность стала 0
        canvasGroup.alpha = 0f;

        // Отключаем Canvas (опционально)
        canvasGroup.gameObject.SetActive(false);
    }
}
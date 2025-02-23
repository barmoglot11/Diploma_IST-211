using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeOutPanel : MonoBehaviour
{
    public Button hideButton;      // Кнопка, по нажатию на которую будет скрываться панель
    public CanvasGroup panel;      // Панель, которую нужно скрыть
    public float fadeDuration = 0.5f; // Длительность анимации

    void Start()
    {
        hideButton.onClick.AddListener(() => StartCoroutine(FadeOut()));
    }

    IEnumerator FadeOut()
    {
        // hideButton.interactable = false; // Отключаем возможность нажатия
        float elapsedTime = 0f;
        float startAlpha = panel.alpha;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            panel.alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        panel.alpha = 0f;
        panel.gameObject.SetActive(false);
    }
}

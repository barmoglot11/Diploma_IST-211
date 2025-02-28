using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Video;

public class FadeOutPanelBlackScreen : MonoBehaviour
{
    public Button hideButton;      // Кнопка, по нажатию на которую будет скрываться панель
    public CanvasGroup panel;      // Панель, которую нужно скрыть
    public CanvasGroup blackScreen; // Черный экран (должен быть добавлен в сцену)
    public float fadeDuration = 0.5f; // Длительность анимации

    [Header("Дополнительные объекты")]
    public GameObject objectToActivate; // Объект, который нужно активировать после скрытия панели
    public Camera cameraToDisable;      // Камера, которую нужно скрыть
    public VideoPlayer videoPlayerToStop; // Video Player, который нужно остановить

    void Start()
    {
        // Инициализация черного экрана (если он не назначен в инспекторе)
        if (blackScreen != null)
        {
            blackScreen.alpha = 0f; // Начальная прозрачность черного экрана
            blackScreen.gameObject.SetActive(false); // Скрываем черный экран в начале
        }

        // Назначаем обработчик нажатия на кнопку
        hideButton.onClick.AddListener(() => StartCoroutine(FadeOut()));
    }

    IEnumerator FadeOut()
    {
        // Отключаем кнопку, чтобы предотвратить повторное нажатие
        hideButton.interactable = false;

        // Плавное появление черного экрана
        if (blackScreen != null)
        {
            blackScreen.gameObject.SetActive(true); // Активируем черный экран
            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                blackScreen.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
                yield return null;
            }

            blackScreen.alpha = 1f; // Убедимся, что черный экран полностью видим
        }

        // Плавное скрытие панели
        float elapsedTimePanel = 0f;
        float startAlpha = panel.alpha;

        while (elapsedTimePanel < fadeDuration)
        {
            elapsedTimePanel += Time.deltaTime;
            panel.alpha = Mathf.Lerp(startAlpha, 0f, elapsedTimePanel / fadeDuration);
            yield return null;
        }

        // Активируем объект, если он задан
        objectToActivate.gameObject.SetActive(true);

        // Скрываем камеру, если она задана
        cameraToDisable.gameObject.SetActive(false);

        // Останавливаем Video Player, если он задан
        videoPlayerToStop.gameObject.SetActive(false);

        panel.alpha = 0f;
        panel.gameObject.SetActive(false); // Скрываем панель

        // Включаем кнопку обратно (если нужно)
        hideButton.interactable = true;
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Video;

public class FadeOutPanelBlackScreen : MonoBehaviour
{
    public Button hideButton;      // Кнопка, по нажатию на которую будет скрыт панель черного экрана
    public CanvasGroup panel;      // Панель, которую нужно скрыть
    public CanvasGroup blackScreen; // Панель черного экрана (которая будет показана на время)
    public float fadeDuration = 0.5f; // Продолжительность затухания

    [Header("Объекты для активации")]
    public GameObject objectToActivate; // Объект, который нужно активировать после затухания
    public Camera cameraToDisable;      // Камера, которую нужно отключить
    public VideoPlayer videoPlayerToStop; // Video Player, который нужно остановить

    void Start()
    {
        // Инициализация панели черного экрана (если она не была активирована)
        if (blackScreen != null)
        {
            blackScreen.alpha = 0f; // Устанавливаем начальную прозрачность панели черного экрана
            blackScreen.gameObject.SetActive(false); // Скрываем панель черного экрана
        }

        // Добавляем слушатель нажатия на кнопку
        hideButton.onClick.AddListener(() => StartCoroutine(FadeOut()));
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; 
    }

    IEnumerator FadeOut()
    {
        // Деактивируем кнопку, пока происходит затухание
        hideButton.interactable = false;

        // Показ панели черного экрана
        if (blackScreen != null)
        {
            blackScreen.gameObject.SetActive(true); // Активируем панель черного экрана
            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                blackScreen.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
                yield return null;
            }

            blackScreen.alpha = 1f; // Устанавливаем конечную прозрачность
        }

        // Затухание панели
        float elapsedTimePanel = 0f;
        float startAlpha = panel.alpha;

        while (elapsedTimePanel < fadeDuration)
        {
            elapsedTimePanel += Time.deltaTime;
            panel.alpha = Mathf.Lerp(startAlpha, 0f, elapsedTimePanel / fadeDuration);
            yield return null;
        }

        // Активируем объект после затухания
        objectToActivate.SetActive(true);

        // Отключаем камеру
        cameraToDisable.gameObject.SetActive(false);

        // Останавливаем Video Player
        videoPlayerToStop.gameObject.SetActive(false);

        panel.alpha = 0f;
        panel.gameObject.SetActive(false); // Скрываем панель

        // Активируем кнопку снова (после завершения процесса)
        hideButton.interactable = true;
    }
}

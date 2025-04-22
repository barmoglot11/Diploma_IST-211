using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeAndSwitchScene : MonoBehaviour
{
    [Header("Settings")]
    public Button button; // Кнопка, которая запускает анимацию и переключение сцены
    public GameObject panel; // GameObject, который будет плавно появляться
    public float fadeSpeed = 1f; // Скорость анимации
    public string sceneToLoad; // Имя сцены для перехода

    private CanvasGroup canvasGroup; // CanvasGroup для управления непрозрачностью

    private void Start()
    {
        // Проверяем, назначены ли кнопка и панель
        if (button == null || panel == null)
        {
            Debug.LogError("Button or Panel is not assigned in the inspector!");
            return;
        }

        // Получаем или добавляем компонент CanvasGroup
        canvasGroup = panel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = panel.AddComponent<CanvasGroup>();
        }

        // Изначально скрываем панель
        canvasGroup.alpha = 0;
        panel.SetActive(false);

        // Назначаем обработчик события нажатия на кнопку
        button.onClick.AddListener(OnButtonClick);
    }

    // Обработчик нажатия на кнопку
    private void OnButtonClick()
    {
        StartCoroutine(FadeInAndSwitchScene());
    }

    // Анимация появления панели и переключение сцены
    private IEnumerator FadeInAndSwitchScene()
    {
        // Активируем панель
        panel.SetActive(true);

        // Плавно увеличиваем непрозрачность
        float elapsed = 0f;
        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * fadeSpeed;
            canvasGroup.alpha = Mathf.Lerp(0, 1, elapsed);
            yield return null;
        }

        canvasGroup.alpha = 1; // Убедимся, что непрозрачность установлена на 100%

        // Переключаем сцену после завершения анимации
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("Scene to load is not specified!");
        }
    }
}
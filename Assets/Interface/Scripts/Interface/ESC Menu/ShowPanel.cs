using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowPanel : MonoBehaviour
{
    [Header("Settings")]
    public Button button; // Кнопка, которая активирует панель
    public Image panel; // Панель, которая будет появляться
    public float fadeSpeed = 1f; // Скорость анимации

    private void Start()
    {
        // Проверяем, назначены ли кнопка и панель
        if (button == null || panel == null)
        {
            Debug.LogError("Button or Panel is not assigned in the inspector!");
            return;
        }

        // Изначально скрываем панель
        // panel.gameObject.SetActive(false);
        // panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 0);

        // Назначаем обработчик события нажатия на кнопку
        button.onClick.AddListener(OnButtonClick);
    }

    // Обработчик нажатия на кнопку
    private void OnButtonClick()
    {
        StartCoroutine(FadeInPanel());
    }

    // Анимация появления панели
    private IEnumerator FadeInPanel()
    {
        panel.gameObject.SetActive(true);

        float elapsed = 0f;
        Color startColor = panel.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f); // Полная непрозрачность

        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * fadeSpeed;
            panel.color = Color.Lerp(startColor, endColor, elapsed);
            yield return null;
        }

        panel.color = endColor;
    }
}
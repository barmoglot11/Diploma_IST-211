using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HidePanelOnClick : MonoBehaviour
{
    [Header("Settings")]
    public Button button; // Кнопка, которая скрывает панель
    public Image panel; // Панель, которая будет скрываться
    public float fadeSpeed = 1f; // Скорость анимации

    private void Start()
    {
        // Проверяем, назначены ли кнопка и панель
        if (button == null || panel == null)
        {
            Debug.LogError("Button or Panel is not assigned in the inspector!");
            return;
        }

        // Назначаем обработчик события нажатия на кнопку
        button.onClick.AddListener(OnButtonClick);
    }

    // Обработчик нажатия на кнопку
    private void OnButtonClick()
    {
        StartCoroutine(FadeOutPanel());
    }

    // Анимация скрытия панели
    private IEnumerator FadeOutPanel()
    {
        float elapsed = 0f;
        Color startColor = panel.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f); // Полная прозрачность

        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * fadeSpeed;
            panel.color = Color.Lerp(startColor, endColor, elapsed);
            yield return null;
        }

        panel.color = endColor;
        panel.gameObject.SetActive(false);
    }
}
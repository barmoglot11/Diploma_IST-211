using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateObjectOnButtonClick : MonoBehaviour
{
    public Button button; // Кнопка на Canvas
    public GameObject objectToActivate; // GameObject, который нужно активировать
    public float delay = 1f; // Задержка перед активацией

    private void Start()
    {
        // Проверяем, что кнопка назначена
        if (button != null)
        {
            // Подписываемся на событие нажатия кнопки
            button.onClick.AddListener(OnButtonClicked);
        }
        else
        {
            Debug.LogError("Кнопка не назначена в инспекторе!");
        }
    }

    private void OnButtonClicked()
    {
        // Запускаем корутину с задержкой
        StartCoroutine(ActivateObjectAfterDelay());
    }

    private System.Collections.IEnumerator ActivateObjectAfterDelay()
    {
        yield return new WaitForSeconds(delay); // Ждем указанное количество секунд

        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true); // Активируем GameObject
        }
        else
        {
            Debug.LogError("GameObject для активации не назначен в инспекторе!");
        }
    }
}

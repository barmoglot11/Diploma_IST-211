using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UI;

public class SwitchToNewCanvasAndCameraTAB : MonoBehaviour
{
    public Canvas newCanvas; // Новый Canvas, который нужно активировать
    public Camera newCamera; // Новая камера, которую нужно активировать
    public GameObject objectToDisable; // GameObject, который нужно деактивировать

    public float fadeSpeed = 2f; // Скорость эффекта fade
    public float delayBeforeActivation = 0.5f; // Задержка перед активацией после нажатия

    private bool isSwitching = false; // Флаг для отслеживания процесса переключения
    private Coroutine currentCoroutine; // Текущая выполняемая корутина

    void Update()
    {
        // Выходим, если идет переключение
        if (isSwitching) return;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Если уже выполняется корутина, выходим
            if (currentCoroutine != null) return;

            currentCoroutine = StartCoroutine(SwitchToNewView());
        }
    }

    private IEnumerator SwitchToNewView()
    {
        isSwitching = true; // Устанавливаем флаг переключения

        // Ждем указанную задержку перед активацией
        yield return new WaitForSeconds(delayBeforeActivation);

        // Активируем новый Canvas и делаем fade-in
        if (newCanvas != null)
        {
            newCanvas.gameObject.SetActive(true);
            CanvasGroup canvasGroup = newCanvas.GetComponent<CanvasGroup>();
            if (canvasGroup == null) canvasGroup = newCanvas.gameObject.AddComponent<CanvasGroup>();

            // Плавное появление нового Canvas
            float alpha = 0;
            while (alpha < 1)
            {
                alpha += Time.deltaTime * fadeSpeed;
                canvasGroup.alpha = alpha;
                yield return null;
            }
        }

        // Активируем новую камеру
        if (newCamera != null)
        {
            newCamera.gameObject.SetActive(true);
        }

        // Деактивируем старый GameObject с fade-out
        if (objectToDisable != null)
        {
            CanvasGroup objectCanvasGroup = objectToDisable.GetComponent<CanvasGroup>();
            if (objectCanvasGroup == null) objectCanvasGroup = objectToDisable.AddComponent<CanvasGroup>();

            float alpha = 1;
            while (alpha > 0)
            {
                alpha -= Time.deltaTime * fadeSpeed;
                objectCanvasGroup.alpha = alpha;
                yield return null;
            }

            objectToDisable.SetActive(false);
        }

        isSwitching = false; // Сбрасываем флаг переключения
        currentCoroutine = null; // Очищаем ссылку на корутину

        // Дополнительные действия после переключения
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        BestiaryManager.Instance.ChangeInfo();
        InputManager.Instance.EnableCharacterControls(false);
    }
}
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SwitchToNewCanvasAndCamera : MonoBehaviour
{
    public Canvas newCanvas; // Новый Canvas, который нужно включить
    public Camera newCamera; // Новая камера, которую нужно включить
    public GameObject objectToDisable; // GameObject, который нужно отключить

    public float fadeSpeed = 2f; // Скорость плавного перехода
    public float delayBeforeActivation = 0.5f; // Задержка перед активацией новых элементов

    private bool isSwitching = false; // Флаг для блокировки ввода
    private Coroutine currentCoroutine; // Текущая активная корутина

    void Update()
    {
        // Блокируем ввод, если идет анимация
        if (isSwitching) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Если корутина уже запущена, не запускаем новую
            if (currentCoroutine != null) return;

            currentCoroutine = StartCoroutine(SwitchToNewView());
        }
    }

    private IEnumerator SwitchToNewView()
    {
        isSwitching = true; // Блокируем ввод

        // Задержка перед активацией новых элементов
        yield return new WaitForSeconds(delayBeforeActivation);

        // Включаем новый Canvas и камеру
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

        if (newCamera != null)
        {
            newCamera.gameObject.SetActive(true);
        }

        // Плавное отключение GameObject
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

        isSwitching = false; // Разблокируем ввод
        currentCoroutine = null; // Сбрасываем текущую корутину

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        InputManager.Instance.DisableCharMoveAndCamera();
    }
}
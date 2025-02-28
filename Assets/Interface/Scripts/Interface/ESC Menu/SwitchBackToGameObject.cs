using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SwitchBackToGameObject : MonoBehaviour
{
    public GameObject objectToEnable; // GameObject, который нужно включить
    public Canvas canvasToDisable; // Canvas, который нужно отключить
    public Camera cameraToDisable; // Камера, которую нужно отключить

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

            currentCoroutine = StartCoroutine(SwitchBackToOriginalView());
        }
    }

    private IEnumerator SwitchBackToOriginalView()
    {
        isSwitching = true; // Блокируем ввод

        // Задержка перед активацией новых элементов
        yield return new WaitForSeconds(delayBeforeActivation);

        // Включаем GameObject
        if (objectToEnable != null)
        {
            objectToEnable.SetActive(true);
            CanvasGroup objectCanvasGroup = objectToEnable.GetComponent<CanvasGroup>();
            if (objectCanvasGroup == null) objectCanvasGroup = objectToEnable.AddComponent<CanvasGroup>();

            // Плавное появление GameObject
            float alpha = 0;
            while (alpha < 1)
            {
                alpha += Time.deltaTime * fadeSpeed;
                objectCanvasGroup.alpha = alpha;
                yield return null;
            }
        }

        // Плавное отключение Canvas
        if (canvasToDisable != null)
        {
            CanvasGroup canvasGroup = canvasToDisable.GetComponent<CanvasGroup>();
            if (canvasGroup == null) canvasGroup = canvasToDisable.gameObject.AddComponent<CanvasGroup>();

            float alpha = 1;
            while (alpha > 0)
            {
                alpha -= Time.deltaTime * fadeSpeed;
                canvasGroup.alpha = alpha;
                yield return null;
            }

            canvasToDisable.gameObject.SetActive(false);
        }

        // Отключаем камеру
        if (cameraToDisable != null)
        {
            cameraToDisable.gameObject.SetActive(false);
        }

        isSwitching = false; // Разблокируем ввод
        currentCoroutine = null; // Сбрасываем текущую корутину
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkerIndicator : MonoBehaviour
{
    public Transform target; // Объект-метка, за которым следим
    public RectTransform panel; // Панель с меткой и стрелками
    public Image[] arrows; // Массив стрелок (0 - верх, 1 - низ, 2 - лево, 3 - право)
    public Camera mainCamera; // Камера игрока
    public float edgeOffset = 50f; // Отступ от края экрана

    private RectTransform canvasRect; // RectTransform канваса

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // Получаем RectTransform канваса
        canvasRect = panel.GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (target == null || mainCamera == null)
        {
            Debug.LogWarning("Target or camera is not assigned.");
            return;
        }

        // Переводим позицию метки в экранные координаты
        Vector3 screenPos = mainCamera.WorldToViewportPoint(target.position);

        // Проверяем, находится ли метка в пределах видимости камеры
        bool isVisible = screenPos.x >= 0 && screenPos.x <= 1 && screenPos.y >= 0 && screenPos.y <= 1 && screenPos.z > 0;

        if (isVisible)
        {
            // Если метка в видимости, отключаем панель и стрелки
            panel.gameObject.SetActive(false);
        }
        else
        {
            // Если метка за пределами видимости, включаем панель и настраиваем стрелки
            panel.gameObject.SetActive(true);

            // Ограничиваем позицию метки в пределах экрана
            Vector2 clampedScreenPos = new Vector2(
                Mathf.Clamp(screenPos.x, 0.05f, 0.95f), // Ограничиваем, чтобы панель не выходила за края
                Mathf.Clamp(screenPos.y, 0.05f, 0.95f)
            );

            // Переводим координаты viewport в координаты канваса
            Vector2 canvasPos = new Vector2(
                (clampedScreenPos.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f),
                (clampedScreenPos.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)
            );

            // Определяем направление к метке
            Vector2 screenCenter = new Vector2(0.5f, 0.5f);
            Vector2 dir = (clampedScreenPos - screenCenter).normalized;

            // Выключаем все стрелки
            foreach (var arrow in arrows)
            {
                arrow.gameObject.SetActive(false);
            }

            // Включаем нужную стрелку в зависимости от направления
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            if (angle > -45 && angle <= 45) // Право
            {
                arrows[3].gameObject.SetActive(true);
                canvasPos.x = (canvasRect.sizeDelta.x * 0.5f) - edgeOffset;
            }
            else if (angle > 45 && angle <= 135) // Верх
            {
                arrows[0].gameObject.SetActive(true);
                canvasPos.y = (canvasRect.sizeDelta.y * 0.5f) - edgeOffset;
            }
            else if (angle > 135 || angle <= -135) // Лево
            {
                arrows[2].gameObject.SetActive(true);
                canvasPos.x = -(canvasRect.sizeDelta.x * 0.5f) + edgeOffset;
            }
            else if (angle > -135 && angle <= -45) // Низ
            {
                arrows[1].gameObject.SetActive(true);
                canvasPos.y = -(canvasRect.sizeDelta.y * 0.5f) + edgeOffset;
            }

            // Устанавливаем позицию панели
            panel.anchoredPosition = canvasPos;
        }
    }
}
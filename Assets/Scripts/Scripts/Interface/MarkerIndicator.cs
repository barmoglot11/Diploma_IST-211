using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkerIndicator : MonoBehaviour
{
    public Transform target; // Цель-объект, к которому будет направлен индикатор
    public RectTransform panel; // Панель для отображения индикатора
    public Image[] arrows; // Изображения стрелок (0 - вверх, 1 - вправо, 2 - вниз, 3 - влево)
    public Camera mainCamera; // Основная камера
    public float edgeOffset = 50f; // Смещение от краев экрана

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

        // Получаем положение цели в координатах экрана
        Vector3 screenPos = mainCamera.WorldToViewportPoint(target.position);

        // Проверяем, находится ли цель в пределах видимости
        bool isVisible = screenPos.x >= 0 && screenPos.x <= 1 && screenPos.y >= 0 && screenPos.y <= 1 && screenPos.z > 0;

        if (isVisible)
        {
            // Если цель видима, скрываем панель
            panel.gameObject.SetActive(false);
        }
        else
        {
            // Если цель не видима, показываем панель
            panel.gameObject.SetActive(true);

            // Ограничиваем положение панели в пределах экрана
            Vector2 clampedScreenPos = new Vector2(
                Mathf.Clamp(screenPos.x, 0.05f, 0.95f), // Ограничиваем, чтобы панель не выходила за края
                Mathf.Clamp(screenPos.y, 0.05f, 0.95f)
            );

            // Преобразуем положение из viewport в координаты канваса
            Vector2 canvasPos = new Vector2(
                (clampedScreenPos.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f),
                (clampedScreenPos.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)
            );

            // Находим центр экрана
            Vector2 screenCenter = new Vector2(0.5f, 0.5f);
            Vector2 dir = (clampedScreenPos - screenCenter).normalized;

            // Скрываем все стрелки
            foreach (var arrow in arrows)
            {
                arrow.gameObject.SetActive(false);
            }

            // Определяем угол направления
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            if (angle > -45 && angle <= 45) // Вправо
            {
                arrows[3].gameObject.SetActive(true);
                canvasPos.x = (canvasRect.sizeDelta.x * 0.5f) - edgeOffset;
            }
            else if (angle > 45 && angle <= 135) // Вверх
            {
                arrows[0].gameObject.SetActive(true);
                canvasPos.y = (canvasRect.sizeDelta.y * 0.5f) - edgeOffset;
            }
            else if (angle > 135 || angle <= -135) // Вниз
            {
                arrows[2].gameObject.SetActive(true);
                canvasPos.x = -(canvasRect.sizeDelta.x * 0.5f) + edgeOffset;
            }
            else if (angle > -135 && angle <= -45) // Влево
            {
                arrows[1].gameObject.SetActive(true);
                canvasPos.y = -(canvasRect.sizeDelta.y * 0.5f) + edgeOffset;
            }

            // Устанавливаем положение панели индикатора
            panel.anchoredPosition = canvasPos;
        }
    }
}

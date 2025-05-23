using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BillboardCanvas : MonoBehaviour
{
    private Camera playerCamera; // Камера игрока

    void Start()
    {
        // Получение основной камеры (предполагается, что она помечена как MainCamera)
        playerCamera = Camera.main;

        // Проверка, была ли найдена камера
        if (playerCamera == null)
        {
            Debug.LogWarning("Камера не найдена. Убедитесь, что она помечена как MainCamera.");
        }
    }

    void Update()
    {
        // Если камера найдена, поворачиваем Canvas к ней
        if (playerCamera != null)
        {
            // Поворачиваем Canvas к камере
            transform.LookAt(transform.position + playerCamera.transform.rotation * Vector3.forward,
                playerCamera.transform.rotation * Vector3.up);
        }
    }
}
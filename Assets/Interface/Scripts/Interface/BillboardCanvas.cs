using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BillboardCanvas : MonoBehaviour
{
    private Camera playerCamera; // Ссылка на камеру игрока

    void Start()
    {
        // Находим камеру игрока автоматически (если она есть на сцене)
        playerCamera = Camera.main;

        // Если камера не найдена, выводим предупреждение
        if (playerCamera == null)
        {
            Debug.LogWarning("Камера игрока не найдена. Убедитесь, что на сцене есть камера с тегом MainCamera.");
        }
    }

    void Update()
    {
        // Если камера найдена, поворачиваем Canvas к камере
        if (playerCamera != null)
        {
            // Поворачиваем Canvas к камере
            transform.LookAt(transform.position + playerCamera.transform.rotation * Vector3.forward,
                            playerCamera.transform.rotation * Vector3.up);
        }
    }
}

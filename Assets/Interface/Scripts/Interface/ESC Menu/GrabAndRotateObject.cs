using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndRotateObject : MonoBehaviour
{
    [Header("Settings")]
    public float rotationSpeed = 10f; // Скорость вращения объекта
    public float maxDistance = 10f; // Максимальное расстояние для захвата объекта
    public Camera targetCamera; // Камера, через которую будет происходить захват

    private Rigidbody grabbedObject; // Объект, который сейчас захвачен
    private Vector3 offset; // Смещение между позицией мыши и центром объекта

    private void Start()
    {
        // Если камера не назначена в инспекторе, используем основную камеру
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
            if (targetCamera == null)
            {
                Debug.LogError("No camera assigned and no main camera found in the scene!");
            }
        }
    }

    private void Update()
    {
        // Если нажата ЛКМ
        if (Input.GetMouseButtonDown(0))
        {
            TryGrabObject();
        }

        // Если ЛКМ отпущена
        if (Input.GetMouseButtonUp(0))
        {
            ReleaseObject();
        }

        // Если объект захвачен, вращаем его
        if (grabbedObject != null)
        {
            RotateObject();
        }
    }

    // Попытка захватить объект
    private void TryGrabObject()
    {
        // Проверяем, есть ли камера
        if (targetCamera == null)
        {
            Debug.LogError("No camera assigned!");
            return;
        }

        // Создаем луч из камеры в направлении мыши
        Ray ray = targetCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Проверяем, попал ли луч в объект с коллайдером
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            // Проверяем, есть ли у объекта Rigidbody
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                grabbedObject = rb;
                grabbedObject.isKinematic = true; // Отключаем физику для плавного вращения
                offset = grabbedObject.position - hit.point; // Вычисляем смещение
            }
            else
            {
                Debug.LogWarning("No Rigidbody found on the object!");
            }
        }
        else
        {
            Debug.LogWarning("No object found within the grab distance!");
        }
    }

    // Вращение объекта
    private void RotateObject()
    {
        if (grabbedObject == null)
        {
            Debug.LogWarning("Grabbed object is null!");
            return;
        }

        // Получаем движение мыши по осям X и Y
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        // Вращаем объект
        grabbedObject.transform.Rotate(Vector3.up, -mouseX, Space.World);
        grabbedObject.transform.Rotate(Vector3.right, mouseY, Space.World);
    }

    // Отпустить объект
    private void ReleaseObject()
    {
        if (grabbedObject != null)
        {
            grabbedObject.isKinematic = false; // Включаем физику обратно
            grabbedObject = null; // Сбрасываем захваченный объект
        }
    }
}
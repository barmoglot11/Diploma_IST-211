using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndRotateObject : MonoBehaviour
{
    [Header("Settings")]
    public float rotationSpeed = 10f; // Скорость вращения объекта
    public float maxDistance = 10f; // Максимальное расстояние для захвата объекта
    public Camera targetCamera; // Камера, через которую осуществляется захват

    private Rigidbody grabbedObject; // Объект, который был захвачен
    private Vector3 offset; // Смещение между точкой клика и центром объекта

    private void Start()
    {
        // Если камера не назначена, пытаемся найти основную камеру
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
            if (targetCamera == null)
            {
                Debug.LogError("Камера не назначена, и основная камера не найдена в сцене!");
            }
        }
    }

    private void Update()
    {
        // Попытка захватить объект при нажатии ЛКМ
        if (Input.GetMouseButtonDown(0))
        {
            TryGrabObject();
        }

        // Отпустить объект при отпускании ЛКМ
        if (Input.GetMouseButtonUp(0))
        {
            ReleaseObject();
        }

        // Если объект захвачен, вращать его
        if (grabbedObject != null)
        {
            RotateObject();
        }
    }

    // Метод попытки захвата объекта
    private void TryGrabObject()
    {
        // Проверяем, назначена ли камера
        if (targetCamera == null)
        {
            Debug.LogError("Камера не назначена!");
            return;
        }

        // Создаём луч из точки курсора через камеру
        Ray ray = targetCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Проверяем, попал ли луч в объект на заданном расстоянии
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            // Ищем компонент Rigidbody у попавшего объекта
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                grabbedObject = rb;
                grabbedObject.isKinematic = true; // Делаем объект кинематическим для управления вращением
                offset = grabbedObject.position - hit.point; // Вычисляем смещение
            }
            else
            {
                Debug.LogWarning("У объекта нет компонента Rigidbody!");
            }
        }
        else
        {
            Debug.LogWarning("Нет объекта в пределах дистанции захвата!");
        }
    }

    // Метод вращения захваченного объекта
    private void RotateObject()
    {
        if (grabbedObject == null)
        {
            Debug.LogWarning("Захваченный объект равен null!");
            return;
        }

        // Получаем значения движения мыши по осям X и Y
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        // Вращаем объект вокруг мировых осей Y и X
        grabbedObject.transform.Rotate(Vector3.up, -mouseX, Space.World);
        grabbedObject.transform.Rotate(Vector3.right, mouseY, Space.World);
    }

    // Метод отпускания объекта
    private void ReleaseObject()
    {
        if (grabbedObject != null)
        {
            grabbedObject.isKinematic = false; // Возвращаем физическое управление объектом
            grabbedObject = null; // Сбрасываем ссылку на захваченный объект
        }
    }
}
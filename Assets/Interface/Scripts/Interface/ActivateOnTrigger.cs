using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnTrigger : MonoBehaviour
{
    public GameObject objectToActivate; // Объект, который нужно активировать/деактивировать

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, если объект, который вошел в триггер, имеет тег "Player"
        if (other.CompareTag("Player"))
        {
            // Активируем объект
            objectToActivate.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Проверяем, если объект, который вышел из триггера, имеет тег "Player"
        if (other.CompareTag("Player"))
        {
            // Деактивируем объект
            objectToActivate.SetActive(false);
        }
    }
}

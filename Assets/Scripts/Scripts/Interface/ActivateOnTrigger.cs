using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnTrigger : MonoBehaviour
{
    public GameObject objectToActivate; // Объект, который будет активироваться/деактивироваться

    private void OnTriggerEnter(Collider other)
    {
        // Проверка, если объект, вошедший в триггер, имеет тег "Player"
        if (other.CompareTag("Player"))
        {
            // Активация объекта
            objectToActivate.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Проверка, если объект, покинувший триггер, имеет тег "Player"
        if (other.CompareTag("Player"))
        {
            // Деактивация объекта
            objectToActivate.SetActive(false);
        }
    }
}
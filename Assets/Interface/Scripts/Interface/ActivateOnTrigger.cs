using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnTrigger : MonoBehaviour
{
    public GameObject objectToActivate; // ������, ������� ����� ������������/��������������

    private void OnTriggerEnter(Collider other)
    {
        // ���������, ���� ������, ������� ����� � �������, ����� ��� "Player"
        if (other.CompareTag("Player"))
        {
            // ���������� ������
            objectToActivate.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // ���������, ���� ������, ������� ����� �� ��������, ����� ��� "Player"
        if (other.CompareTag("Player"))
        {
            // ������������ ������
            objectToActivate.SetActive(false);
        }
    }
}

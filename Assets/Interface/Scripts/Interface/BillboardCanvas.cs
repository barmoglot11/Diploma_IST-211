using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BillboardCanvas : MonoBehaviour
{
    private Camera playerCamera; // ������ �� ������ ������

    void Start()
    {
        // ������� ������ ������ ������������� (���� ��� ���� �� �����)
        playerCamera = Camera.main;

        // ���� ������ �� �������, ������� ��������������
        if (playerCamera == null)
        {
            Debug.LogWarning("������ ������ �� �������. ���������, ��� �� ����� ���� ������ � ����� MainCamera.");
        }
    }

    void Update()
    {
        // ���� ������ �������, ������������ Canvas � ������
        if (playerCamera != null)
        {
            // ������������ Canvas � ������
            transform.LookAt(transform.position + playerCamera.transform.rotation * Vector3.forward,
                            playerCamera.transform.rotation * Vector3.up);
        }
    }
}

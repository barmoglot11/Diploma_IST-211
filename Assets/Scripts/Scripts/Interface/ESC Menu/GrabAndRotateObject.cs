using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndRotateObject : MonoBehaviour
{
    [Header("Settings")]
    public float rotationSpeed = 10f; // �������� �������� �������
    public float maxDistance = 10f; // ������������ ���������� ��� ������� �������
    public Camera targetCamera; // ������, ����� ������� ����� ����������� ������

    private Rigidbody grabbedObject; // ������, ������� ������ ��������
    private Vector3 offset; // �������� ����� �������� ���� � ������� �������

    private void Start()
    {
        // ���� ������ �� ��������� � ����������, ���������� �������� ������
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
        // ���� ������ ���
        if (Input.GetMouseButtonDown(0))
        {
            TryGrabObject();
        }

        // ���� ��� ��������
        if (Input.GetMouseButtonUp(0))
        {
            ReleaseObject();
        }

        // ���� ������ ��������, ������� ���
        if (grabbedObject != null)
        {
            RotateObject();
        }
    }

    // ������� ��������� ������
    private void TryGrabObject()
    {
        // ���������, ���� �� ������
        if (targetCamera == null)
        {
            Debug.LogError("No camera assigned!");
            return;
        }

        // ������� ��� �� ������ � ����������� ����
        Ray ray = targetCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // ���������, ����� �� ��� � ������ � �����������
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            // ���������, ���� �� � ������� Rigidbody
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                grabbedObject = rb;
                grabbedObject.isKinematic = true; // ��������� ������ ��� �������� ��������
                offset = grabbedObject.position - hit.point; // ��������� ��������
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

    // �������� �������
    private void RotateObject()
    {
        if (grabbedObject == null)
        {
            Debug.LogWarning("Grabbed object is null!");
            return;
        }

        // �������� �������� ���� �� ���� X � Y
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        // ������� ������
        grabbedObject.transform.Rotate(Vector3.up, -mouseX, Space.World);
        grabbedObject.transform.Rotate(Vector3.right, mouseY, Space.World);
    }

    // ��������� ������
    private void ReleaseObject()
    {
        if (grabbedObject != null)
        {
            grabbedObject.isKinematic = false; // �������� ������ �������
            grabbedObject = null; // ���������� ����������� ������
        }
    }
}
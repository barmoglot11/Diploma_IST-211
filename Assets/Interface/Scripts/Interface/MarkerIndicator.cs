using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkerIndicator : MonoBehaviour
{
    public Transform target; // ������-�����, �� ������� ������
    public RectTransform panel; // ������ � ������ � ���������
    public Image[] arrows; // ������ ������� (0 - ����, 1 - ���, 2 - ����, 3 - �����)
    public Camera mainCamera; // ������ ������
    public float edgeOffset = 50f; // ������ �� ���� ������

    private RectTransform canvasRect; // RectTransform �������

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // �������� RectTransform �������
        canvasRect = panel.GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (target == null || mainCamera == null)
        {
            Debug.LogWarning("Target or camera is not assigned.");
            return;
        }

        // ��������� ������� ����� � �������� ����������
        Vector3 screenPos = mainCamera.WorldToViewportPoint(target.position);

        // ���������, ��������� �� ����� � �������� ��������� ������
        bool isVisible = screenPos.x >= 0 && screenPos.x <= 1 && screenPos.y >= 0 && screenPos.y <= 1 && screenPos.z > 0;

        if (isVisible)
        {
            // ���� ����� � ���������, ��������� ������ � �������
            panel.gameObject.SetActive(false);
        }
        else
        {
            // ���� ����� �� ��������� ���������, �������� ������ � ����������� �������
            panel.gameObject.SetActive(true);

            // ������������ ������� ����� � �������� ������
            Vector2 clampedScreenPos = new Vector2(
                Mathf.Clamp(screenPos.x, 0.05f, 0.95f), // ������������, ����� ������ �� �������� �� ����
                Mathf.Clamp(screenPos.y, 0.05f, 0.95f)
            );

            // ��������� ���������� viewport � ���������� �������
            Vector2 canvasPos = new Vector2(
                (clampedScreenPos.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f),
                (clampedScreenPos.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)
            );

            // ���������� ����������� � �����
            Vector2 screenCenter = new Vector2(0.5f, 0.5f);
            Vector2 dir = (clampedScreenPos - screenCenter).normalized;

            // ��������� ��� �������
            foreach (var arrow in arrows)
            {
                arrow.gameObject.SetActive(false);
            }

            // �������� ������ ������� � ����������� �� �����������
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            if (angle > -45 && angle <= 45) // �����
            {
                arrows[3].gameObject.SetActive(true);
                canvasPos.x = (canvasRect.sizeDelta.x * 0.5f) - edgeOffset;
            }
            else if (angle > 45 && angle <= 135) // ����
            {
                arrows[0].gameObject.SetActive(true);
                canvasPos.y = (canvasRect.sizeDelta.y * 0.5f) - edgeOffset;
            }
            else if (angle > 135 || angle <= -135) // ����
            {
                arrows[2].gameObject.SetActive(true);
                canvasPos.x = -(canvasRect.sizeDelta.x * 0.5f) + edgeOffset;
            }
            else if (angle > -135 && angle <= -45) // ���
            {
                arrows[1].gameObject.SetActive(true);
                canvasPos.y = -(canvasRect.sizeDelta.y * 0.5f) + edgeOffset;
            }

            // ������������� ������� ������
            panel.anchoredPosition = canvasPos;
        }
    }
}
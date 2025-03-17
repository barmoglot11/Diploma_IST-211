using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateObjectOnButtonClick : MonoBehaviour
{
    public Button button; // ������ �� Canvas
    public GameObject objectToActivate; // GameObject, ������� ����� ������������
    public float delay = 1f; // �������� ����� ����������

    private void Start()
    {
        // ���������, ��� ������ ���������
        if (button != null)
        {
            // ������������� �� ������� ������� ������
            button.onClick.AddListener(OnButtonClicked);
        }
        else
        {
            Debug.LogError("������ �� ��������� � ����������!");
        }
    }

    private void OnButtonClicked()
    {
        // ��������� �������� � ���������
        StartCoroutine(ActivateObjectAfterDelay());
    }

    private System.Collections.IEnumerator ActivateObjectAfterDelay()
    {
        yield return new WaitForSeconds(delay); // ���� ��������� ���������� ������

        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true); // ���������� GameObject
        }
        else
        {
            Debug.LogError("GameObject ��� ��������� �� �������� � ����������!");
        }
    }
}

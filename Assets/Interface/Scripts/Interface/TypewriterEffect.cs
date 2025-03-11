using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TypewriterEffect : MonoBehaviour
{
    [Header("Settings")]
    public float delayBetweenLetters = 0.1f; // �������� ����� ���������� ����
    public string fullText = "Hello, World!"; // ������ �����, ������� ����� ����������
    public AudioClip typeSound; // ������������, ������� ����� �������������

    private TMP_Text textComponent; // ��������� TextMeshPro
    private string currentText = ""; // ������� �����, ������� ������������
    private AudioSource audioSource; // ��������� AudioSource ��� ������������ �����

    private void Awake()
    {
        // �������� ��������� TextMeshPro
        textComponent = GetComponent<TMP_Text>();
        if (textComponent == null)
        {
            Debug.LogError("TextMeshPro component is missing!");
            return;
        }

        // �������� ��� ��������� ��������� AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // ������� ����� ��� ������
        textComponent.text = "";
    }

    private void Start()
    {
        // ��������� �������� ��������� ������
        StartCoroutine(ShowText());
    }

    // �������� ��� �������� ��������� ������
    private IEnumerator ShowText()
    {
        // ����������� ������������, ���� ��� ���������
        if (typeSound != null && audioSource != null)
        {
            audioSource.clip = typeSound;
            audioSource.Play(); // ��������� ������������
        }

        // ���������� ���������� �����
        for (int i = 0; i < fullText.Length; i++)
        {
            // ��������� �� ����� �����
            currentText += fullText[i];
            textComponent.text = currentText;

            // ���� ��������� ����� ����� ����������� ��������� �����
            yield return new WaitForSeconds(delayBetweenLetters);
        }

        // ������������� ������������, ���� ��� ��� ������ (�����������)
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    // ����� ��� ������� �������� ������� (��������, �� ������� �������)
    public void StartTypewriter(string newText)
    {
        fullText = newText;
        currentText = "";
        textComponent.text = "";
        StartCoroutine(ShowText());
    }
}
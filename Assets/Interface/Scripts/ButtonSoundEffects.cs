using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSoundEffects : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [Header("�������� �������")]
    [Tooltip("���� ��� ��������� �������")]
    public AudioClip hoverSound; // ���� ��� ���������

    [Tooltip("���� ��� ������� �� ������")]
    public AudioClip clickSound; // ���� ��� �������

    private AudioSource audioSource; // ��������� ��� ��������������� ������

    void Start()
    {
        // �������� ��� ��������� ��������� AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // ����������� AudioSource
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    // ��� ��������� �������
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSound != null)
        {
            // ������������� ���� ��� ���������
            audioSource.PlayOneShot(hoverSound);
        }
    }

    // ��� ������� �� ������
    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickSound != null)
        {
            // ������������� ���� ��� �������
            audioSource.PlayOneShot(clickSound);
        }
    }
}
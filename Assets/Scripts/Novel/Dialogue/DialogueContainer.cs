using UnityEngine;
using TMPro;

namespace DIALOGUE
{
    [System.Serializable]
    public class DialogueContainer
    {
        /// <summary>
        /// ������, � ������� ����� ������� TextMeshPro (������ ������ Root Container)
        /// </summary>
        public GameObject root;
        /// <summary>
        /// ��������� ���� � ������
        /// </summary>
        public NameContainer nameContainer;
        /// <summary>
        /// ��������� ���� � ������
        /// /// </summary>
        public TextMeshProUGUI dialogueText;

        public void SetDialogueColor(Color color) => dialogueText.color = color;
        public void SetDialogueFont(TMP_FontAsset font) => dialogueText.font = font;
    }
}
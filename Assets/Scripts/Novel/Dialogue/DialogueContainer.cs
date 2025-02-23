using UnityEngine;
using TMPro;

namespace DIALOGUE
{
    [System.Serializable]
    public class DialogueContainer
    {
        /// <summary>
        /// Объект, в котором лежат диалоги TextMeshPro (сейчас назван Root Container)
        /// </summary>
        public GameObject root;
        /// <summary>
        /// Текстовое поле с именем
        /// </summary>
        public NameContainer nameContainer;
        /// <summary>
        /// Текстовое поле с фразой
        /// /// </summary>
        public TextMeshProUGUI dialogueText;

        public void SetDialogueColor(Color color) => dialogueText.color = color;
        public void SetDialogueFont(TMP_FontAsset font) => dialogueText.font = font;
    }
}
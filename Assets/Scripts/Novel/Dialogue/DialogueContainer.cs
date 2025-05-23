using UnityEngine;
using TMPro;

namespace DIALOGUE
{
    [System.Serializable]
    public class DialogueContainer
    {
        /// <summary>
        /// Объект, в который будет помещен текст TextMeshPro (родительский контейнер)
        /// </summary>
        public GameObject root;
        
        /// <summary>
        /// Контейнер для имени
        /// </summary>
        public NameContainer nameContainer;
        
        /// <summary>
        /// Компонент TextMeshProUGUI для отображения диалога
        /// </summary>
        public TextMeshProUGUI dialogueText;

        /// <summary>
        /// Устанавливает цвет текста диалога
        /// </summary>
        /// <param name="color">Цвет текста</param>
        public void SetDialogueColor(Color color) => dialogueText.color = color;

        /// <summary>
        /// Устанавливает шрифт текста диалога
        /// </summary>
        /// <param name="font">Шрифт для текста</param>
        public void SetDialogueFont(TMP_FontAsset font) => dialogueText.font = font;
    }
}
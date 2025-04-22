using TMPro;
using UnityEngine;

namespace DIALOGUE
{
    public class DialogueHistoryCont : MonoBehaviour
    {
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI dialogueText;

        public void SetupContainer(string name, string dialogue)
        {
            nameText.text = name;
            dialogueText.text = dialogue;
        }
    }
}
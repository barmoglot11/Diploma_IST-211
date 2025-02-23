using UnityEngine;

namespace DIALOGUE
{
    public class DialogueUI : MonoBehaviour
    {
        InputManager input => InputManager.Instance;
        public GameObject dialogueCanvas;

        public void EndDialogue()
        {
            dialogueCanvas.SetActive(false);
            input.ChangeInputStatus("Gameplay");
        }
        
        public void StartDialogue()
        {
            input.ChangeInputStatus("Dialogue");
            dialogueCanvas.SetActive(true);
        }
    }
}
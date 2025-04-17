using System.Collections;
using DIALOGUE;
using Interfaces;
using UnityEngine;

namespace Dialogues
{
    public class DialogueHousePapers : MonoBehaviour, IDialogue
    {
        public void StartDialogue()
        {
            
        }

        public IEnumerator Dialogue()
        {
            yield return null;
            DialogueSystem.instance.CloseDialogue();
        }
    }
}
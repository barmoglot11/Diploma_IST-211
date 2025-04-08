using System;
using System.Collections.Generic;
using UnityEngine;

namespace DIALOGUE
{
    public class DialogueHistory : MonoBehaviour
    {
        public static DialogueHistory Instance;
        public DialogueHistoryCont dialogueBoxPrefab;

        public GameObject dialogueBoxContainer;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void AddDialogue(string nameStr, string replica)
        {
            var dialCont = Instantiate(dialogueBoxPrefab, dialogueBoxContainer.transform).GetComponent<DialogueHistoryCont>();
            dialCont.SetupContainer(nameStr, replica);
        }
        
        public void AddDialogue(string nameStr, List<string> replica)
        {
            foreach (var rep in replica)
            {
                AddDialogue(nameStr, rep);
            }
        }
    }
}
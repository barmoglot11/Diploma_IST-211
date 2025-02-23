using UnityEngine;
using DIALOGUE;

public class DialogueController: MonoBehaviour
{
    [SerializeField] private InputReader input;
    [SerializeField] private ConversationManager conversationManager;
    private void Start()
    {
        input.Prompt += HandleHurry;
    }

    private void HandleHurry()
    {
        DialogueSystem.instance.OnUserPrompt_Next();
    }
}
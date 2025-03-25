using UnityEngine;
using DIALOGUE;

public class DialogueController: MonoBehaviour
{
    InputReader input => InputManager.Instance.IR;
    private void Start()
    {
        input.Prompt += HandleHurry;
    }

    private void HandleHurry()
    {
        DialogueSystem.instance.OnUserPrompt_Next();
    }
}
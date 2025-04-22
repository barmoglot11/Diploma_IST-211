using UnityEngine;
using DIALOGUE;

[RequireComponent(typeof(DialogueSystem))]
public class DialogueController : MonoBehaviour
{
    private DialogueSystem _dialogueSystem;
    private InputReader _inputReader;

    private void Awake()
    {
        // Получаем ссылки на компоненты
        _dialogueSystem = GetComponent<DialogueSystem>();
        _inputReader = InputManager.Instance?.IR;
    }

    private void OnEnable()
    {
        if (_inputReader != null)
        {
            _inputReader.PromptEvent += HandleHurry;
        }
        else
        {
            Debug.LogWarning("InputReader not available", this);
        }
    }

    private void OnDisable()
    {
        if (_inputReader != null)
        {
            _inputReader.PromptEvent -= HandleHurry;
        }
    }

    private void HandleHurry()
    {
        if (_dialogueSystem != null)
        {
            _dialogueSystem.OnUserPrompt_Next();
        }
        else
        {
            Debug.LogError("DialogueSystem reference is missing!", this);
        }
    }
}
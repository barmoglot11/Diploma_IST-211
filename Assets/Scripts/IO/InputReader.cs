using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "InputReader")]
public class InputReader : ScriptableObject, PlayerControlPref.IDialogueActions, PlayerControlPref.IUIActions, PlayerControlPref.IGameplayActions
{
    private PlayerControlPref _playerControlPref;
    private PlayerControlPref.GameplayActions prevousType;
    private void OnEnable()
    {
        if (_playerControlPref == null)
        {
            _playerControlPref = new PlayerControlPref();
            
            _playerControlPref.Gameplay.SetCallbacks(this);
            _playerControlPref.UI.SetCallbacks(this);
            _playerControlPref.Dialogue.SetCallbacks(this);
            
            _playerControlPref.Gameplay.Enable();
        }
    }

    public void SetGameplay()
    {
        _playerControlPref.Gameplay.Enable();
        _playerControlPref.UI.Disable();
        _playerControlPref.Dialogue.Disable();
    }
    
    public void SetUI()
    {
        _playerControlPref.Gameplay.Disable();
        _playerControlPref.UI.Enable();
        _playerControlPref.Dialogue.Disable();
    }

    public void SetDialogue()
    {
        _playerControlPref.Gameplay.Disable();
        _playerControlPref.UI.Disable();
        _playerControlPref.Dialogue.Enable();
    }
    
    public event Action<Vector2> MoveEvent;
    
    public event Action Prompt;
    public event Action CloseEvent;
    public event Action MenuEvent;
    public event Action InteractEvent;

    public void OnMovement(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    void PlayerControlPref.IGameplayActions.OnMenu(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            MenuEvent?.Invoke();
            SetUI();
        }
    }

    public void OnUserPrompt(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Prompt?.Invoke();
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            InteractEvent?.Invoke();
        }
    }

    void PlayerControlPref.IDialogueActions.OnMenu(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            MenuEvent?.Invoke();
            SetUI();
        }
    }

    public void OnClose(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            CloseEvent?.Invoke();
            GetPreviousTypeSet();
        }
    }

    private void GetPreviousTypeSet()
    {
        if (Equals(prevousType, _playerControlPref.Gameplay))
        {
            SetGameplay();
        }
        else
        {
            SetDialogue();
        }
    }
}

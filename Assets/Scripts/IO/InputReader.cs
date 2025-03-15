using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "InputReader")]
public class InputReader : ScriptableObject, PlayerControlPref.IDialogueActions, 
    PlayerControlPref.IUIActions, PlayerControlPref.IGameplayActions, PlayerControlPref.ILokcpickMinigameActions
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
        _playerControlPref.LokcpickMinigame.Disable();
    }
    
    public void SetUI()
    {
        _playerControlPref.Gameplay.Disable();
        _playerControlPref.UI.Enable();
        _playerControlPref.Dialogue.Disable();
        _playerControlPref.LokcpickMinigame.Disable();
    }

    public void SetDialogue()
    {
        _playerControlPref.Gameplay.Disable();
        _playerControlPref.LokcpickMinigame.Disable();
        _playerControlPref.UI.Disable();
        _playerControlPref.Dialogue.Enable();
    }
    public void SetLock()
    {
        _playerControlPref.Gameplay.Disable();
        _playerControlPref.UI.Disable();
        _playerControlPref.Dialogue.Disable();
        _playerControlPref.LokcpickMinigame.Enable();
    }
    public event Action<Vector2> MoveEvent;
    public event Action<bool> HurryEvent;
    public event Action Prompt;
    public event Action CloseEvent;
    public event Action MenuEvent;
    public event Action InteractEvent;
    public event Action InvestigationEvent;
    public event Action ExitEvent;
    public event Action MoveLockEvent;
    public event Action ShotEvent;
    public event Action SnipeEvent;

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

    public void OnInvestigation(InputAction.CallbackContext context)
    {
        InvestigationEvent?.Invoke();
    }

    public void OnHurry(InputAction.CallbackContext context)
    {
        HurryEvent?.Invoke(context.action.IsPressed());
    }

    public void OnShot(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
            ShotEvent?.Invoke();
    }

    public void OnSnipe(InputAction.CallbackContext context)
    {
        
        SnipeEvent?.Invoke();
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

    public void OnExit(InputAction.CallbackContext context)
    {
        ExitEvent?.Invoke();
    }

    public void OnMoveLock(InputAction.CallbackContext context)
    {
        MoveLockEvent?.Invoke();
    }
}

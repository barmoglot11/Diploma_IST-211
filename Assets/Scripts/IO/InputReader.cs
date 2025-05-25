using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Input/Input Reader")]
public class InputReader : ScriptableObject, 
    PlayerControlPref.IDialogueActions,
    PlayerControlPref.IUIActions, 
    PlayerControlPref.IGameplayActions,
    PlayerControlPref.ILokcpickMinigameActions
{
    #region Event Fields

    private Action<Vector2> _moveEvent;
    private Action<bool> _hurryEvent;
    private Action _interactEvent;
    private Action _investigationEvent;
    private Action _menuEvent;
    private Action _closeEvent;
    private Action _exitEvent;
    private Action _promptEvent;
    private Action _moveLockEvent;
    private Action _shotEvent;
    private Action<bool> _snipeEvent;
    #endregion

    #region Public Events
    // 2. Контролируемые публичные события
    public event Action<Vector2> MoveEvent
    {
        add => _moveEvent += value;
        remove => _moveEvent -= value;
    }

    public event Action<bool> HurryEvent
    {
        add => _hurryEvent += value;
        remove => _hurryEvent -= value;
    }

    public event Action InteractEvent
    {
        add => _interactEvent += value;
        remove => _interactEvent -= value;
    }

    public event Action InvestigationEvent
    {
        add => _investigationEvent += value;
        remove => _investigationEvent -= value;
    }

    public event Action MenuEvent
    {
        add => _menuEvent += value;
        remove => _menuEvent -= value;
    }

    public event Action CloseEvent
    {
        add => _closeEvent += value;
        remove => _closeEvent -= value;
    }

    public event Action ExitEvent
    {
        add => _exitEvent += value;
        remove => _exitEvent -= value;
    }

    public event Action PromptEvent
    {
        add => _promptEvent += value;
        remove => _promptEvent -= value;
    }

    public event Action MoveLockEvent
    {
        add => _moveLockEvent += value;
        remove => _moveLockEvent -= value;
    }

    public event Action ShotEvent
    {
        add => _shotEvent += value;
        remove => _shotEvent -= value;
    }

    public event Action<bool> SnipeEvent
    {
        add => _snipeEvent += value;
        remove => _snipeEvent -= value;
    }
    #endregion

    #region Subscriber Management
    // 3. Методы управления подписками
    public void ClearAllSubscribers()
    {
        _moveEvent = null;
        _hurryEvent = null;
        _interactEvent = null;
        _investigationEvent = null;
        _menuEvent = null;
        _closeEvent = null;
        _exitEvent = null;
        _promptEvent = null;
        _moveLockEvent = null;
        _shotEvent = null;
        _snipeEvent = null;
    }

    
    public Delegate GetInteractDelegate() => _interactEvent;
    public int GetSubscribersCount(Delegate action) => action?.GetInvocationList().Length ?? 0;

    public int GetInteractSubscribersCount() => GetSubscribersCount(_interactEvent);
    public int GetMoveSubscribersCount() => GetSubscribersCount(_moveEvent);
    public int GetMenuSubscribersCount() => GetSubscribersCount(_menuEvent);
    // Добавьте аналогичные методы для других событий по необходимости
    #endregion

    #region Control Scheme
    private PlayerControlPref _controls;
    private InputActionMap _previousActiveMap;

    private void OnEnable() => InitializeControls();
    private void OnDisable() => DisposeControls();

    private void InitializeControls()
    {
        if (_controls != null) return;

        _controls = new PlayerControlPref();
        
        _controls.Gameplay.SetCallbacks(this);
        _controls.UI.SetCallbacks(this);
        _controls.Dialogue.SetCallbacks(this);
        _controls.LokcpickMinigame.SetCallbacks(this);

        SetGameplay();
    }

    private void DisposeControls()
    {
        if (_controls == null) return;

        ClearAllSubscribers();

        _controls.Gameplay.Disable();
        _controls.UI.Disable();
        _controls.Dialogue.Disable();
        _controls.LokcpickMinigame.Disable();

        _controls = null;
    }
    #endregion

    #region Input Mode Switching
    public void SetGameplay() => SwitchActionMap(_controls?.Gameplay);
    public void SetUI() => SwitchActionMap(_controls?.UI);
    public void SetDialogue() => SwitchActionMap(_controls?.Dialogue);
    public void SetLockpickMinigame() => SwitchActionMap(_controls?.LokcpickMinigame);

    private void SwitchActionMap(InputActionMap newActionMap)
    {
        if (_controls == null || newActionMap == null) 
        {
            Debug.LogWarning("Attempted to switch to null action map");
            return;
        }

        if (_controls.Gameplay.enabled) _previousActiveMap = _controls.Gameplay;
        else if (_controls.UI.enabled) _previousActiveMap = _controls.UI;
        else if (_controls.Dialogue.enabled) _previousActiveMap = _controls.Dialogue;
        else if (_controls.LokcpickMinigame.enabled) _previousActiveMap = _controls.LokcpickMinigame;

        _controls.Gameplay.Disable();
        _controls.UI.Disable();
        _controls.Dialogue.Disable();
        _controls.LokcpickMinigame.Disable();

        newActionMap.Enable();
    }

    public void ReturnToPreviousActionMap()
    {
        if (_previousActiveMap != null)
        {
            SwitchActionMap(_previousActiveMap);
        }
        else
        {
            SetGameplay();
        }
    }
    #endregion

    #region Input Callbacks
    public void OnMovement(InputAction.CallbackContext context)
    {
        _moveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
            _interactEvent?.Invoke();
    }

    public void OnInvestigation(InputAction.CallbackContext context)
    {
        if (context.performed)
            _investigationEvent?.Invoke();
    }

    public void OnHurry(InputAction.CallbackContext context)
    {
        _hurryEvent?.Invoke(context.ReadValueAsButton());
    }

    public void OnMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _menuEvent?.Invoke();
            SetUI();
        }
    }

    public void OnClose(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _closeEvent?.Invoke();
            ReturnToPreviousActionMap();
        }
    }

    public void OnExit(InputAction.CallbackContext context)
    {
        if (context.performed)
            _exitEvent?.Invoke();
    }

    public void OnUserPrompt(InputAction.CallbackContext context)
    {
        if (context.performed)
            _promptEvent?.Invoke();
    }

    public void OnMoveLock(InputAction.CallbackContext context)
    {
        if (context.performed)
            _moveLockEvent?.Invoke();
    }

    public void OnShot(InputAction.CallbackContext context)
    {
        if (context.performed)
            _shotEvent?.Invoke();
    }

    public void OnSnipe(InputAction.CallbackContext context)
    {
        _snipeEvent?.Invoke(context.ReadValueAsButton());
    }
    #endregion

    #region Debug
    public void LogAllSubscribersCount()
    {
        Debug.Log($"Move subscribers: {GetMoveSubscribersCount()}");
        Debug.Log($"Interact subscribers: {GetInteractSubscribersCount()}");
        Debug.Log($"Menu subscribers: {GetMenuSubscribersCount()}");
    }
    #endregion
}
using System;
using BATTLE;
using Cinemachine;
using UnityEngine;

/// <summary>
/// Централизованный менеджер управления вводом с поддержкой состояний и событий
/// </summary>
public class InputManager : MonoBehaviour
{
    #region Singleton Pattern
    public static InputManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // Раскомментировать для сохранения между сценами
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    #region Serialized Fields
    [Header("Основные компоненты")]
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private MainCharacter _characterMovement;
    [SerializeField] private CinemachineBrain _cinemachineBrain;
    #endregion

    public InputReader IR {get { return _inputReader; } }
    
    #region Input State Management
    [Header("Состояния ввода")]
    [SerializeField] private InputStatus _currentInputStatus = InputStatus.Gameplay;
    [SerializeField] private InputStatus _previousInputStatus = InputStatus.None;

    // Событие изменения состояния ввода
    public event Action<InputStatus, InputStatus> OnInputStatusChanged;
    
    /// <summary>
    /// Текущее активное состояние ввода
    /// </summary>
    public InputStatus CurrentInputStatus
    {
        get => _currentInputStatus;
        private set
        {
            if (_currentInputStatus != value)
            {
                _previousInputStatus = _currentInputStatus;
                _currentInputStatus = value;
                OnInputStatusChanged?.Invoke(_previousInputStatus, _currentInputStatus);
            }
        }
    }
    #endregion

    #region Public Methods
    
    public void ChangeInputStatus(InputStatus newStatus)
    {
        if (CurrentInputStatus == newStatus) return;

        switch (newStatus)
        {
            case InputStatus.Gameplay:
                SetGameplayState();
                break;
            case InputStatus.Dialogue:
                SetDialogueState();
                break;
            case InputStatus.UI:
                SetUIState();
                break;
            case InputStatus.Lock:
                SetLockState();
                break;
            case InputStatus.None:
            default:
                Debug.LogWarning($"Unhandled input status: {newStatus}");
                break;
        }

        LogStatusChange(newStatus);
    }
    
    public void ReturnToPreviousStatus()
    {
        if (_previousInputStatus != InputStatus.None)
        {
            ChangeInputStatus(_previousInputStatus);
        }
    }
    #endregion

    #region State Handlers
    private void SetGameplayState()
    {
        _inputReader?.SetGameplay();
        EnableCharacterControls(true);
        CurrentInputStatus = InputStatus.Gameplay;
    }

    private void SetDialogueState()
    {
        _inputReader?.SetDialogue();
        EnableCharacterControls(false);
        CurrentInputStatus = InputStatus.Dialogue;
    }

    private void SetUIState()
    {
        _inputReader?.SetUI();
        EnableCharacterControls(false);
        CurrentInputStatus = InputStatus.UI;
    }

    private void SetLockState()
    {
        _inputReader?.SetLockpickMinigame();
        EnableCharacterControls(false);
        CurrentInputStatus = InputStatus.Lock;
    }
    #endregion

    #region Control Management
    /// <summary>
    /// Включить/отключить управление персонажем и камерой
    /// </summary>
    public void EnableCharacterControls(bool enable)
    {
        if (_characterMovement != null)
        {
            _characterMovement.enabled = enable;
        }

        if (_cinemachineBrain != null)
        {
            _cinemachineBrain.enabled = enable;
        }

        Debug.Log($"Character controls {(enable ? "enabled" : "disabled")}");
    }
    
    public void ChangeCursorState(bool enable) => Cursor.visible = enable;
    public void ChangeCursorLock(bool enable) => Cursor.lockState = enable ? CursorLockMode.Locked : CursorLockMode.None;
    #endregion

    #region Utility Methods
    private void LogStatusChange(InputStatus newStatus)
    {
        Debug.Log($"Input status changed from {_previousInputStatus} to {newStatus}");
    }

    private void OnValidate()
    {
        // Автоматическое заполнение ссылок если они пустые
        if (_inputReader == null) _inputReader = GetComponent<InputReader>();
        if (_characterMovement == null) _characterMovement = FindObjectOfType<MainCharacter>();
        if (_cinemachineBrain == null) _cinemachineBrain = FindObjectOfType<CinemachineBrain>();
    }
    #endregion
}

/// <summary>
/// Возможные состояния системы ввода
/// </summary>
public enum InputStatus
{
    None = -1,
    Gameplay,
    Dialogue,
    UI,
    Lock
}

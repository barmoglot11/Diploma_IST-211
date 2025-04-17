using System;
using Cinemachine;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public InputReader IR;
    public static InputManager Instance;
    public MainCharacter movement;
    public CinemachineBrain camera;
    
    public InputStatus currentInputStatus;
    public InputStatus previousInputStatus;
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

    public void DisableCharMoveAndCamera()
    {
        if (camera != null && movement != null)
        {
            movement.enabled = false;
            camera.enabled = false;
            Debug.Log("Disabling character");
        }
        
    }
    public void EnableCharMoveAndCamera()
    {
        if (camera != null && movement != null)
        {
            movement.enabled = true;
            camera.enabled = true;
            Debug.Log("Enabling character");
        }
    }

    public void ChangeInputStatus(InputStatus status)
    {
        switch (status)
        {
            case InputStatus.Dialogue:
                IR.SetDialogue();
                SwitchStatus(InputStatus.Dialogue);
                DisableCharMoveAndCamera();
                break;
            case InputStatus.Gameplay:
                IR.SetGameplay();
                SwitchStatus(InputStatus.Gameplay);
                EnableCharMoveAndCamera();
                break;
            case InputStatus.UI:
                IR.SetUI();
                SwitchStatus(InputStatus.UI);
                DisableCharMoveAndCamera();
                break;
            case InputStatus.Lock:
                IR.SetLock();
                SwitchStatus(InputStatus.Lock);
                DisableCharMoveAndCamera();
                break;
            default:
                break;
        }
    }

    public void SwitchStatus(InputStatus currentStatus)
    {
        previousInputStatus = currentInputStatus;
        currentInputStatus = currentStatus;
    }

    public void ReturnToLastStatus()
    {
        ChangeInputStatus(previousInputStatus);
    }
}

[Serializable]
public enum InputStatus
{
    None = -1,
    Gameplay,
    Dialogue,
    UI,
    Lock
}

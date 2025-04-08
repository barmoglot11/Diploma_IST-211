using System;
using Cinemachine;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public InputReader IR;
    public static InputManager Instance;
    public MainCharacter movement;
    public CinemachineBrain camera;
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
                DisableCharMoveAndCamera();
                break;
            case InputStatus.Gameplay:
                IR.SetGameplay();
                EnableCharMoveAndCamera();
                break;
            case InputStatus.UI:
                IR.SetUI();
                DisableCharMoveAndCamera();
                break;
            case InputStatus.Lock:
                IR.SetLock();
                DisableCharMoveAndCamera();
                break;
            default:
                break;
        }
    }

    
}

[Serializable]
public enum InputStatus
{
    Gameplay,
    Dialogue,
    UI,
    Lock
}

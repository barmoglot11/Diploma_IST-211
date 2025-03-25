using Cinemachine;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public InputReader IR;
    public static InputManager Instance;
    public Movement movement;
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
        }
        
    }
    public void EnableCharMoveAndCamera()
    {
        if (camera != null && movement != null)
        {
            movement.enabled = true;
            camera.enabled = true;
        }
    }

    public void ChangeInputStatus(string status)
    {
        switch (status)
        {
            case "Dialogue":
                IR.SetDialogue();
                break;
            case "Gameplay":
                IR.SetGameplay();
                break;
            case "UI":
                IR.SetUI();
                break;
            case "Lock":
                IR.SetLock();
                break;
            default:
                break;
        }
    }
}

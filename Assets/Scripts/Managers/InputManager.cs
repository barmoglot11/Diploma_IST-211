using Cinemachine;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private InputReader IR;
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
        movement.enabled = false;
        camera.enabled = false;
    }
    public void EnableCharMoveAndCamera()
    {
        movement.enabled = true;
        camera.enabled = true;
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

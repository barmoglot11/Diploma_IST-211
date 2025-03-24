using Cinemachine;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private InputReader IR;
    public static InputManager Instance;
    public Movement movement;
    public CinemachineBrain camera;

    public GameObject MenuButton;
    private bool _openMenuSetted = false;
    private bool _closeMenuSetted = false;
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

    public void Update()
    {
        if (MenuButton.activeSelf && !_openMenuSetted)
        {
            //открыть меню event
        }

        if (!MenuButton.activeSelf && !_closeMenuSetted)
        {
            //закрыть меню event
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

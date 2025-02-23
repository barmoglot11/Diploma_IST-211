using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private InputReader IR;
    public static InputManager Instance;
    
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
            default:
                break;
        }
    }
}

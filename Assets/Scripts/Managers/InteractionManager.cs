using System;
using UnityEngine;

/// <summary>
/// Менеджер управления взаимодействиями
/// </summary>
public class InteractionManager : MonoBehaviour
{
    #region Singleton Pattern
    public static InteractionManager Instance { get; private set; }

    private void Awake()
    {
        InitializeSingleton();
    }

    private void InitializeSingleton()
    {
        if (Instance is null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    #region Event Management
    
    public void AddInteract(Action action)
    {
        if (action == null)
        {
            Debug.LogWarning("Attempt to add null interact action", this);
            return;
        }

        if (InputManager.Instance?.IR is not null)
        {
            InputManager.Instance.IR.InteractEvent += action;
        }
        else
        {
            Debug.LogError("InputReader is not available!", this);
        }
    }
    
    public void DeleteInteract(Action action)
    {
        if (action == null)
        {
            Debug.LogWarning("Attempt to remove null interact action", this);
            return;
        }

        if (InputManager.Instance?.IR is not null)
        {
            InputManager.Instance.IR.InteractEvent -= action;
        }
        else
        {
            Debug.LogError("InputReader is not available!", this);
        }
    }
    
    public void ClearAllInteracts()
    {
        if (InputManager.Instance?.IR is not null)
        {
            if (InputManager.Instance.IR.GetInteractSubscribersCount() <= 0) return;
            var subscribers = InputManager.Instance.IR.GetInteractDelegate()?.GetInvocationList();

            if (subscribers == null) return;
            foreach (var subscriber in subscribers)
            {
                InputManager.Instance.IR.InteractEvent -= (Action)subscriber;
            }

        }
    }
    #endregion

    #region Utility Methods
    private void OnDestroy()
    {
        
        if (Instance == this)
        {
            ClearAllInteracts();
        }
    }
    #endregion
}

using System;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance;
    
    InputReader input => InputManager.Instance.IR;
    
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

    public void AddInteract(Action action)
    {
        input.InteractEvent += action;
    }
    
    public void DeleteInteract(Action action)
    {
        input.InteractEvent -= action;
    }
}

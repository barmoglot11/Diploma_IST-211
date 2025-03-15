using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance;
    
    [SerializeField]
    InputReader input;
    
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

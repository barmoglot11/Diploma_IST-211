using System;
using UnityEngine;
using BATTLE;

public class GameplayController: MonoBehaviour
{
    public static GameplayController Instance;
    [SerializeField] private InputReader input;

    public GameObject HealthBar;
    public GameObject Arsenal;
    
    [SerializeField] private bool isBattle = false;
    
    public Gun gun => FindObjectOfType<Gun>();

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
}
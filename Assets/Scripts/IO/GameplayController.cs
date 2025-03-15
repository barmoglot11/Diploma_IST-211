using System;
using UnityEngine;
using BATTLE;

public class GameplayController: MonoBehaviour
{
    public static GameplayController Instance;
    [SerializeField] private InputReader input;
    [SerializeField] private bool isBattle = false;
    public Gun gun => FindObjectOfType<Gun>();
    public bool IsBattle
    {
        get => isBattle;
        set
        {
            switch (value)
            {
                case false:
                    isBattle = false;
                    UnlinkInput();
                    break;
                case true:
                    isBattle = true;
                    LinkInput();
                    break;
            }
        }
    }

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

    private void LinkInput()
    {
        input.ShotEvent += gun.Shot;
        input.SnipeEvent += gun.Snipe;
    }

    private void UnlinkInput()
    {
        input.ShotEvent -= gun.Shot;
        input.SnipeEvent -= gun.Snipe;
    }
}
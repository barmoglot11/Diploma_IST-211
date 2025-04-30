using System;
using System.Collections;
using UnityEngine;
using BATTLE;

public class GameplayController: MonoBehaviour
{
    private static GameplayController _instance;
    public static GameplayController Instance => _instance;

    [Header("Dependencies")]
    [SerializeField] private MainCharacter MainCharacter;
    [SerializeField] private Enemy Enemy;
    [SerializeField] private FadeOutCanvasWithDelay BlackScreen;
    [SerializeField] private Transform DestinationPoint;
    
    [SerializeField] private GameObject EnterLabyrinthCollider;
    [SerializeField] private GameObject ExitLabyrinthCollider;
    
    [SerializeField] private bool IsBattleStarted = false;

    private void Awake()
    {
        Instantiate();
    }

    void Instantiate()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Death()
    {
        StartCoroutine(DeathHandle());
    }

    private IEnumerator DeathHandle()
    {
        InputManager.Instance.EnableCharacterControls(false);
        BlackScreen.StartFade();
        Debug.Log($"BlackScreen status: {BlackScreen.IsFading}");
        while(BlackScreen.IsFading)
            yield return null;
        Debug.Log($"BlackScreen status: {BlackScreen.IsFading}");
        if (MainCharacter != null && DestinationPoint != null)
        {
            MainCharacter.transform.position = DestinationPoint.position;
            MainCharacter.transform.rotation = DestinationPoint.rotation;
        }
        Debug.Log($"BlackScreen status: {BlackScreen.IsFading}");
        BlackScreen.StartFade();
        Debug.Log($"BlackScreen status: {BlackScreen.IsFading}");
        while(BlackScreen.IsFading)
            yield return null;
        InputManager.Instance.EnableCharacterControls(true);
        StopBattle();
    }

    public void StartBattle()
    {
        if (IsBattleStarted) return;
        Enemy.gameObject.SetActive(true);
        EnterLabyrinthCollider.gameObject.SetActive(true);
        ExitLabyrinthCollider.gameObject.SetActive(false);
        IsBattleStarted = true;
    }

    public void StopBattle()
    {
        if (!IsBattleStarted) return;
        Enemy.gameObject.SetActive(false);
        EnterLabyrinthCollider.gameObject.SetActive(false);
        ExitLabyrinthCollider.gameObject.SetActive(true);
        PostProcessingManager.Instance.SetProfileByIndexSmooth(0);
        IsBattleStarted = false;
    }
    
    void OnValidate()
    {
        if(MainCharacter == null) Debug.LogError("MainCharacter не назначен");
        if(DestinationPoint == null) Debug.LogError("DestinationPoint не назначен");
        if(BlackScreen == null) Debug.LogError("BlackScreen не назначен");
        if(EnterLabyrinthCollider == null) Debug.LogError("EnterLabyrinthCollider не назначен");
        if(ExitLabyrinthCollider == null) Debug.LogError("ExitLabyrinthCollider не назначен");
    }
}
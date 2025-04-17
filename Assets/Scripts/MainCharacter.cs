using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using BATTLE;
using Cinemachine;
using MANAGER;
using UnityEngine;
public struct CameraOrbits
{
    public float Height;
    public float Radius;

    public CameraOrbits(float height, float radius)
    {
        Height = height;
        Radius = radius;
    }
}

public class MainCharacter : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb => GetComponent<Rigidbody>();
    
    AnimationManager animManager => AnimationManager.Instance;
    InputReader input;
    AudioSource Source => GetComponent<AudioSource>();
    private Vector2 _moveDirection;

    public CinemachineFreeLook cameraFreeLook;
    
    [SerializeField]
    private Transform cam;
    
    public float speed = 5f;
    public int speedMult = 3;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public bool Hurry = false;

    public List<CameraOrbits> OrbitsOutdoors;
    public List<CameraOrbits> OrbitsIndoors;
    
    public SerializedDictionary<string, AudioClip> environmentSounds;
    private void Start()
    {
        input = InputManager.Instance.IR;
        SetupInput();
    }

    public void SetupInput()
    {
        input.MoveEvent += HandleMove;
        input.HurryEvent += HandleHurry;
    }

    public void UnlinkMove()
    {
        input.MoveEvent -= HandleMove;
        input.HurryEvent -= HandleHurry;
    }
    
    public void SetupSnipe(Gun gun)
    {
        input.SnipeEvent += Snipe;
        input.ShotEvent += gun.Shot;
    }
    
    public void UnlinkSnipe(Gun gun)
    {
        input.SnipeEvent -= Snipe;
        input.ShotEvent -= gun.Shot;
    }
    private void OnEnable()
    {
        SetupInput();
    }

    private void OnDisable()
    {
        UnlinkMove();
    }

    private void Update()
    {
        Move();
    }

    public void Snipe(bool state) => animManager.IsSniping = state;
    
    private void HandleMove(Vector2 direction)
    {
        if(animManager.IsSniping)
            return;
        _moveDirection = direction;
    }
    private void HandleHurry(bool state)
    {
        if(animManager.IsSniping)
            return;
        Hurry = state;
        animManager.IsRunning = state;
    }
    private void Move()
    {
        Vector3 direction = new Vector3(_moveDirection.x, 0f, _moveDirection.y).normalized;

        if (direction.magnitude > 0.1f)
        {
            var SpeedFinal = speed;
            if (Hurry)
                SpeedFinal *= speedMult;
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
                turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            rb.velocity = moveDir.normalized * SpeedFinal;
            animManager.IsWalking = true;
            PlaySound();
        }
        else
        {
            animManager.IsWalking = false;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        ChangeSound(other.gameObject.tag);
    }
    private void OnCollisionEnter(Collision other)
    {
        ChangeSound(other.gameObject.tag);
    }

    void ChangeSound(string material)
    {
        switch (material)
        {
            case "Wood":
                Source.clip = environmentSounds["Wood"];
                break;
            case "Stone":
                Source.clip = environmentSounds["Stone"];
                break;
            case "Dirt":
                Source.clip = environmentSounds["Dirt"];
                break;
            default:
                Debug.Log("Unknown material");
                Source.clip = null;
                break;
        }
    }

    void PlaySound()
    {
        if(!Source.isPlaying)
            Source.Play();
    }

    public void ChangeCameraSettings(string settings)
    {
        for (var i = 0; i < cameraFreeLook.m_Orbits.Length; i++)
        {
            cameraFreeLook.m_Orbits[i].m_Height = settings == "Out" ? OrbitsOutdoors[i].Height : OrbitsIndoors[i].Height;
            cameraFreeLook.m_Orbits[i].m_Radius = settings == "Out" ? OrbitsOutdoors[i].Radius : OrbitsIndoors[i].Radius;
        }
    }
}

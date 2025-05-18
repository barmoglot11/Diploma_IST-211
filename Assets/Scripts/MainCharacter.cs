using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using BATTLE;
using Cinemachine;
using MANAGER;
using UnityEngine;

[System.Serializable]
public struct CameraOrbits
{
    [SerializeField] 
    public float Height;
    
    [SerializeField]
    public float Radius;

    public CameraOrbits(float height, float radius)
    {
        Height = height;
        Radius = radius;
    }
}

namespace BATTLE
{
    public class MainCharacter : MonoBehaviour
{
    // Компоненты и зависимости
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private CinemachineFreeLook _freeLookCamera;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private ParticleSystem walkParticles;
    [SerializeField] private Transform _leftFootBone;
    [SerializeField] private Transform _rightFootBone;
    
    // Настройки движения
    [Header("Настройки движения")]
    [SerializeField] private float _baseSpeed = 5f;
    [SerializeField] private int _speedMultiplier = 3;
    [SerializeField] private float _turnSmoothTime = 0.1f;
    
    // Настройки камеры
    [Header("Натсройки камеры")]
    [SerializeField] private List<CameraOrbits> _orbitsOutdoors;
    [SerializeField] private List<CameraOrbits> _orbitsIndoors;
    [SerializeField] private List<CameraOrbits> _orbitsSniping;
    private string lastCameraSetting = "Out";
    private string currentCameraSetting = "Out";
    public float transitionDuration = 1f;
    private Coroutine _transitionCoroutine;
    [SerializeField]private bool isSnipeAvailable= false;
    
    // Звуки окружения
    [Header("Настройки аудио")]
    [SerializeField] private SerializedDictionary<string, AudioClip> _environmentSounds;

    // Состояние персонажа
    private Vector2 _moveDirection;
    private float _turnSmoothVelocity;
    private bool _isHurry = false;
    [SerializeField]private InputReader _input;
    
    // Свойства для быстрого доступа
    private Rigidbody Rb => _rigidbody ? _rigidbody : GetComponent<Rigidbody>();
    private AnimationManager AnimManager => AnimationManager.Instance;
    private InputReader Input => _input ? _input : InputManager.Instance.IR;

    private void Start()
    {
        InitializeComponents();
        SetupInput();
    }

    private void InitializeComponents()
    {
        if (!_rigidbody) _rigidbody = GetComponent<Rigidbody>();
        if (!_audioSource) _audioSource = GetComponent<AudioSource>();
    }
    
    

    #region Input Handling
    private void SetupInput()
    {
        Input.MoveEvent += HandleMove;
        Input.HurryEvent += HandleHurry;
    }

    private void UnlinkInput()
    {
        Input.MoveEvent -= HandleMove;
        Input.HurryEvent -= HandleHurry;
    }

    public void SetupSnipeControls(Gun gun)
    {
        Input.SnipeEvent += Snipe;
        Input.ShotEvent += gun.Shoot;
    }
    
    public void UnlinkSnipeControls(Gun gun)
    {
        Input.SnipeEvent -= Snipe;
        Input.ShotEvent -= gun.Shoot;
    }
    #endregion

    #region Movement System
    private void Update()
    {
        HandleMovement();
        HandleSnipe();
    }

    public void PlayLeftFootEffect()
    {
        PlayParticleOnPlace(_leftFootBone);
    }

    public void PlayRightFootEffect()
    {
        PlayParticleOnPlace(_rightFootBone);
    }

    private void PlayParticleOnPlace(Transform target)
    {
        if (walkParticles == null || target == null) return;
        
        walkParticles.transform.position = target.position;
        
        walkParticles.Play();
    }
    
    private void HandleMovement()
    {
        Vector3 direction = new Vector3(_moveDirection.x, 0f, _moveDirection.y).normalized;

        _audioSource.pitch = Random.Range(1.2f, 1.7f);
        
        if (direction.magnitude > 0.1f)
        {
            float speed = _isHurry ? _baseSpeed * _speedMultiplier : _baseSpeed;
            RotateCharacter(direction, speed);
            MoveCharacter(direction, speed);
            AnimManager.IsWalking = true;
            PlayMovementSound();
        }
        else
        {
            AnimManager.IsWalking = false;
            UseStandardVelocity();
        }
    }

    private void RotateCharacter(Vector3 direction, float speed)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _cameraTransform.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    private void MoveCharacter(Vector3 direction, float speed)
    {
        
        var moveDir = Quaternion.Euler(0f, Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _cameraTransform.eulerAngles.y, 0f) * Vector3.forward;

        
        var horizontalVelocity = moveDir.normalized * speed;

        
        var yVelocity = Rb.velocity.y;

        
        Rb.velocity = new Vector3(horizontalVelocity.x, yVelocity, horizontalVelocity.z);
    }
    
    private void UseStandardVelocity() => Rb.velocity = new Vector3(0f, Rb.velocity.y, 0f);
    #endregion

    #region Audio System
    private void OnCollisionStay(Collision other) => UpdateFootstepSound(other);
    private void OnCollisionEnter(Collision other) => UpdateFootstepSound(other);

    private void UpdateFootstepSound(Collision collision)
    {
        if (_environmentSounds.TryGetValue(collision.gameObject.tag, out AudioClip clip))
        {
            _audioSource.clip = clip;
        }
        else
        {
            Debug.LogWarning($"No sound defined for material: {collision.gameObject.tag}");
            Debug.LogWarning(collision.gameObject.name);
            _audioSource.clip = null;
        }
    }

    private void PlayMovementSound()
    {
        if (!_audioSource.isPlaying && _audioSource.clip != null)
        {
            _audioSource.Play();
        }
    }
    #endregion

    #region Camera Control
    public void SwitchCameraProfile(string profileType)
    {
        if (_freeLookCamera == null || _orbitsOutdoors == null || _orbitsIndoors == null) return;
        var targetOrbits = new List<CameraOrbits>();
        switch (profileType)
        {
            case "Out":
                targetOrbits = _orbitsOutdoors;
                break;
            case "In":
                targetOrbits = _orbitsIndoors;
                break;
            case "Snipe":
                targetOrbits = _orbitsSniping;
                break;
            default:
                break;
        }
        
        if (_transitionCoroutine != null)
        {
            StopCoroutine(_transitionCoroutine);
        }
        
        // Запускаем новую корутину
        _transitionCoroutine = StartCoroutine(TransitionOrbitsCoroutine(targetOrbits));
    }

    private IEnumerator TransitionOrbitsCoroutine(List<CameraOrbits> targetOrbits)
    {
        // Сохраняем начальные значения орбит
        var startOrbits = new CameraOrbits[_freeLookCamera.m_Orbits.Length];
        for (int i = 0; i < startOrbits.Length; i++)
        {
            startOrbits[i] = new CameraOrbits 
            { 
                Height = _freeLookCamera.m_Orbits[i].m_Height, 
                Radius = _freeLookCamera.m_Orbits[i].m_Radius 
            };
        }

        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / transitionDuration);

            // Применяем интерполяцию для каждой орбиты
            for (int i = 0; i < _freeLookCamera.m_Orbits.Length && i < targetOrbits.Count; i++)
            {
                _freeLookCamera.m_Orbits[i].m_Height = Mathf.Lerp(
                    startOrbits[i].Height, 
                    targetOrbits[i].Height, 
                    progress);
                    
                _freeLookCamera.m_Orbits[i].m_Radius = Mathf.Lerp(
                    startOrbits[i].Radius, 
                    targetOrbits[i].Radius, 
                    progress);
            }

            yield return null;
        }

        // Убедимся, что конечные значения точно установлены
        for (int i = 0; i < _freeLookCamera.m_Orbits.Length && i < targetOrbits.Count; i++)
        {
            _freeLookCamera.m_Orbits[i].m_Height = targetOrbits[i].Height;
            _freeLookCamera.m_Orbits[i].m_Radius = targetOrbits[i].Radius;
        }

        _transitionCoroutine = null;
    }
    #endregion

    #region Event Handlers
    private void HandleMove(Vector2 direction)
    {
        if (AnimManager.IsSniping) return;
        _moveDirection = direction;
    }

    private void HandleHurry(bool state)
    {
        if (AnimManager.IsSniping) return;
        _isHurry = state;
        AnimManager.IsRunning = state;
    }

    public void Snipe(bool isSniping) => AnimManager.IsSniping = isSniping;

    public void HandleSnipe()
    {
        if(isSnipeAvailable)
        {
            if(!AnimManager.IsSniping)
            {
                SwitchCameraProfile("Out");
                return;
            }
            SwitchCameraProfile("Snipe");
            float speed = _isHurry ? _baseSpeed * _speedMultiplier : _baseSpeed;
            RotateCharacter(Vector3.forward, speed);
        }
    }
    #endregion

    #region Unity Callbacks
    private void OnEnable() => SetupInput();
    private void OnDisable()
    {
        UnlinkInput();
        UseStandardVelocity();
        AnimManager.ResetAllStates();
    }
    #endregion
}

}

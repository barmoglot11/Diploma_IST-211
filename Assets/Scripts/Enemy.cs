using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace BATTLE
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Collider))]
    public class Enemy : MonoBehaviour
    {
        private static readonly int Stagger1 = Animator.StringToHash("Stagger");
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");

        [Header("Dependencies")] 
        [SerializeField] private Collider TriggerCollider;
        [SerializeField] private MainCharacter MainCharacter;
        [SerializeField] private AudioSource ScreamSound;
        [SerializeField] private AudioSource WalkSound;
        
        
        [Header("Movement Settings")]
        [SerializeField] private float _moveSpeed = 3.5f;
        [SerializeField] private float _staggerTime = 4f;
        
        [Header("Debug")]
        [SerializeField] private bool IsDebug = false;
        private Coroutine _debugCoroutine;
        
        private NavMeshAgent _agent;
        private Transform _playerTarget;
        private Animator _animator;
        private bool _isStaggered;
        
        private bool _isMoving;
        
        private void Awake()
        {
            Initialize();
        }

        void Initialize()
        {
            OnValidate();
            _agent.speed = _moveSpeed;
            TriggerCollider.isTrigger = true;
            FindPlayerTarget();
        }

        private void OnEnable()
        {
            ScreamSound.Play();
        }

        private void FindPlayerTarget()
        {
            var playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                _playerTarget = playerObj.transform;
            }
            else
            {
                Debug.LogError("Player target not found in scene!", this);
                enabled = false;
            }
        }

        private void Update()
        {
            HandleMovement();
            HandleAudio(_isMoving);
            if (_animator != null)
            {
                _animator.SetBool(IsWalking, _isMoving);
            }

            if (_debugCoroutine == null && IsDebug)
                _debugCoroutine = StartCoroutine(DebugStaggerRoutine());

        }

        private void HandleMovement()
        {
            if (_isStaggered || _playerTarget == null) 
            {
                _isMoving = false; // Если монстр не может двигаться, устанавливаем _isMoving в false
                return;
            }

            // Получаем текущее расстояние до цели
            float distanceToTarget = Vector3.Distance(transform.position, _playerTarget.position);

            // Устанавливаем новую цель для агента
            _agent.SetDestination(_playerTarget.position);

            // Проверяем, движется ли агент
            _isMoving = distanceToTarget > _agent.stoppingDistance;
            
            // Проверка на столкновение с препятствием
            if (_agent.pathPending || _agent.hasPath)
            {
                if (_agent.velocity.magnitude < 0.1f) // Если скорость агента низкая
                {
                    _isMoving = false; // Установим _isMoving в false, если он уперся в стену
                }
            }

            
        }


        private void HandleAudio(bool isPlaying)
        {
            if (isPlaying)
            {
                if(!WalkSound.isPlaying)
                    WalkSound.Play();
            }
            else
            {
                if(WalkSound.isPlaying)
                    WalkSound.Stop();
            }
        }
        
        public void Stagger()
        {
            if (!_isStaggered)
            {
                StartCoroutine(StaggerRoutine());
            }
        }

        private IEnumerator StaggerRoutine()
        {
            _isStaggered = true;
    
            try
            {
                if (_animator != null && _animator.isActiveAndEnabled)
                    _animator.SetTrigger(Stagger1);
        
                if (_agent != null && _agent.isActiveAndEnabled)
                    _agent.isStopped = true;
        
                Debug.Log("Staggered");
        
                float timer = 0;
                while (timer < _staggerTime)
                {
                    timer += Time.deltaTime;
                    yield return null;
            
                    // Проверка на уничтожение объекта
                    if (this == null || !gameObject.activeInHierarchy)
                        yield break;
                }
            }
            finally
            {
                if (_agent != null && _agent.isActiveAndEnabled)
                    _agent.isStopped = false;
            
                _isStaggered = false;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                HandlePlayerCollision();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                PostProcessingManager.Instance.SetProfileByIndexSmooth(2);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                PostProcessingManager.Instance.SetProfileByIndexSmooth(0);
            }
        }

        private void HandlePlayerCollision()
        {
            Debug.Log("Player Hit!");
            GameplayController.Instance.Death();
        }

        // For debugging purposes
        private void OnDrawGizmos()
        {
            if (_agent != null && _agent.hasPath)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, _agent.destination);
            }
        }
        
        #region Utility Methods

        private void OnValidate()
        {
            // Автоматическое заполнение ссылок если они пустые
            if (TriggerCollider == null) TriggerCollider = GetComponent<Collider>();
            if (MainCharacter == null) MainCharacter = FindObjectOfType<MainCharacter>();
            if (_agent == null) _agent = GetComponent<NavMeshAgent>();
            if (_animator == null) _animator = GetComponent<Animator>();
            
        }

        private IEnumerator DebugStaggerRoutine()
        {
            while (IsDebug)
            {
                Stagger();
                yield return new WaitForSeconds(5f);
            }
        }
        #endregion
    }
}
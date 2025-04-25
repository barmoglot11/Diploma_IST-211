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
        [Header("Dependencies")] 
        [SerializeField] private Collider TriggerCollider;
        [SerializeField] private MainCharacter MainCharacter;
        
        [Header("Movement Settings")]
        [SerializeField] private float _moveSpeed = 3.5f;
        [SerializeField] private float _staggerTime = 1.5f;
        
        [Header("Debug")]
        private bool IsDebug = false;
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
            FindPlayerTarget();
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
            if (_animator != null)
            {
                _animator.SetBool("IsMoving", _isMoving);
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
            if (distanceToTarget > _agent.stoppingDistance)
            {
                _isMoving = true; // Монстр движется к цели
            }
            else
            {
                _isMoving = false; // Монстр достиг цели
            }

            // Проверка на столкновение с препятствием
            if (_agent.pathPending || _agent.hasPath)
            {
                if (_agent.velocity.magnitude < 0.1f) // Если скорость агента низкая
                {
                    _isMoving = false; // Установим _isMoving в false, если он уперся в стену
                }
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
            _agent.isStopped = true;
            Debug.Log("Staggered");
            yield return new WaitForSeconds(_staggerTime);
            
            _agent.isStopped = false;
            _isStaggered = false;
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
            // Consider using events instead of direct scene management
            // GameManager.Instance.PlayerDied();
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

        private void StaggerDebug()
        {
            
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
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace BATTLE
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Collider))]
    public class Enemy : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float _moveSpeed = 3.5f;
        [SerializeField] private float _staggerTime = 1.5f;
        
        private NavMeshAgent _agent;
        private Transform _playerTarget;
        private bool _isStaggered;
        
        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
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
            if (!_isStaggered && _playerTarget != null)
            {
                _agent.SetDestination(_playerTarget.position);
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
    }
}
using System;
using UnityEngine;
using UnityEngine.AI;

namespace BATTLE
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Collider))]
    public class Enemy : MonoBehaviour
    {
        public NavMeshAgent Agent => GetComponent<NavMeshAgent>();
        public GameObject Target => GameObject.FindGameObjectWithTag("Player");
        public bool IsStaggered = false;
        public float speed;

        public void OnEnable()
        {
            Agent.SetDestination(Target.transform.position);
        }

        void Update()
        {
            Agent.SetDestination(Target.transform.position);
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Bullet"))
                Stagger();
        }

        void Stagger()
        {
            IsStaggered = true;
            Debug.Log("Staggered");
            // Проиграть анимацию и в ней убрать стаггер
        }
    }
}
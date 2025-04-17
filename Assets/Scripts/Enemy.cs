using System.Collections;
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
        public float speed;
        public float staggerTime = 1.5f;

        public void OnEnable()
        {
            Agent.SetDestination(Target.transform.position);
        }

        void Update()
        {
            Agent.SetDestination(Target.transform.position);
        }

        public void Stagger()
        {
                StartCoroutine(Staggering());
        }

        public IEnumerator Staggering()
        {
            Debug.Log("Staggered");
            Agent.isStopped = true;
            yield return new WaitForSeconds(staggerTime);
            Agent.isStopped = false;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player Hit!");
                //kill player => main menu load
            }
        }
    }
}
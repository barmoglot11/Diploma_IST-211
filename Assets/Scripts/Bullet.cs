using System;
using Unity.AI.Navigation;
using UnityEngine;

namespace BATTLE
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if(other.gameObject.TryGetComponent<NavMeshSurface>(out var surface) || other.gameObject.TryGetComponent<Enemy>(out var enemy))
                Destroy(gameObject);
        }
    }
}
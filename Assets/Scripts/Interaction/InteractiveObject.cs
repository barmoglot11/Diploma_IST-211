using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class InteractiveObject : MonoBehaviour, IInteractable
{
    public UnityEvent OnInteract;
    public void Interact()
    {
        OnInteract?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player"))
            return;
        InteractionManager.Instance.AddInteract(Interact);
    }

    private void OnTriggerExit(Collider other)
    {
        if(!other.CompareTag("Player"))
            return;
        InteractionManager.Instance.DeleteInteract(Interact);
    }

    private void OnDisable()
    {
        InteractionManager.Instance.DeleteInteract(Interact);
    }
}

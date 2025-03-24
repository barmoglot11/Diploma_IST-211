using INVENTORY;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InteractiveItem : MonoBehaviour, IInteractable
{
    public InventoryManager Inventory => InventoryManager.Instance;
    public string itemName;
    
    public void Interact()
    {
        Inventory.AddItem(itemName);
        gameObject.SetActive(false);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player"))
            return;
        InteractionManager.Instance.AddInteract(Interact);
        Debug.Log("Interact+");
    }

    private void OnTriggerExit(Collider other)
    {
        if(!other.CompareTag("Player"))
            return;
        InteractionManager.Instance.DeleteInteract(Interact);
        Debug.Log("Interact-");
    }

    private void OnDisable()
    {
        InteractionManager.Instance.DeleteInteract(Interact);
        Debug.Log("Interact-");
    }
}

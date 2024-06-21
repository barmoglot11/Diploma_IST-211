using UnityEngine;
using Zenject;

public class mechanic : MonoBehaviour
{
    public InventoryService inventoryService;
    
    [Inject]
    void Mechanic(InventoryService _inventoryService) 
    {
        inventoryService = _inventoryService;
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.TryGetComponent<Item>(out var item))
            inventoryService.AddItemOnInventoy(item);
    }
}

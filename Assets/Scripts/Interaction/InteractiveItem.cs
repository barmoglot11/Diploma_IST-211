using UnityEngine;

public class InteractiveItem : MonoBehaviour, IInteractable
{
    public InventoryManager Inventory => InventoryManager.Instance;
    public Inv_Item item;
    
    public void Interact()
    {
        Inventory.AddItem(item);
        gameObject.SetActive(false);
    }
}

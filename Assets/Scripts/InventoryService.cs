using System.Collections.Generic;
using UnityEngine;

public class InventoryService : MonoBehaviour
{
    public List<Slot> slots;
    private Dictionary<Slot, Item> InventorySlots;
    private GameObject _UIInventoryPrefab;

   
    void LoadInventory()
    {
        //var _UIInventoryPrefab = Resources.Load("Inventory/Inventory");
        //Instantiate(_UIInventoryPrefab);
    }

    public void AddItemOnInventoy(Item item)
    {
        foreach (Slot slot in slots)
        {
            if(!slot.isFull)
            {
                //slot.isFull = true;
                slot.take(item);
                break;
            }
        } 
    }

    private Slot ChechedSlots()
    {
        foreach (var slot in slots)
        {
            if(!slot.isFull)
            {
                slot.isFull = true;
                return slot;
            }
        }
        return null;
    }
}

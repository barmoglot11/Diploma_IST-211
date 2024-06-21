using System;
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
        slots[1].take(item);
        slots[3].take(item);
        slots[2].take(item);

        Slot freeSlot = ChechedSlots();
        if (freeSlot != null)
        {
            //InventorySlots.Add(freeSlot,item);
            freeSlot.take(item);
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

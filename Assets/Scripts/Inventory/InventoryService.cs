using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryService : MonoBehaviour
{

    public List<Slot> slots;

    private Dictionary<Item, Slot> InventorySlots = new Dictionary<Item, Slot>();
    
    public void AddItemInSlot(Item item)
    {
        bool itemIsExist = InventorySlots.ContainsKey(item);
        Debug.Log(item.GetType());
        InventorySlots.ContainsKey(item);
        if(itemIsExist)
        {
            var slot = InventorySlots[item];
            if(item.isStackable)
                slot.take(item);
        }
        else
        {
            foreach (Slot slot in slots)
            {
                if(!slot.isFull)
                {
                    InventorySlots.Add(item, slot);
                    slot.take(item);

                    break;
                }
            } 
        }
    }

    public void GetItemFromInventory(Type type, Vector3 place)
    {
        foreach(Slot slot in slots)
        {
            if(slot.isFull)
            {
                if(slot._item.GetType() == type)
                {   
                    InventorySlots.Remove(slot._item);
                    slot._item.gameObject.transform.SetParent(null);
                    slot._item.gameObject.SetActive(true);
                    slot._item.gameObject.transform.position = place;
                    slot.Clear();
                    break;
                }
                else
                    Debug.Log("Нету объекта");
            }
        }
    }
    
}

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField]
    private TMP_Text countItem;
    [SerializeField]
    private int countItemItemInSlot = 0;
    [SerializeField]
    private Image imageItem;
    [SerializeField]
    private TMP_Text nameItem;
    public Item _item;    
    public bool isFull = false;

    public void take(Item item)
    {
        item.gameObject.transform.SetParent(this.transform);
        item.gameObject.SetActive(false);
        _item = item;
        if(item.isStackable)
        {
            AddItemInSlot(item);
        }
        else
        {
            countItemItemInSlot = 1;
        }
        imageItem.sprite = item.itemSprite;
        countItem.text = countItemItemInSlot.ToString();
        nameItem.text = item.nameOfItem;
        isFull = true;
    }

    void AddItemInSlot(Item item)
    {
        if(countItemItemInSlot <item.maxCountItem)
        {
            countItemItemInSlot++;
        }
    }

    public void Clear()
    {
        countItem.text = null;
        countItemItemInSlot = 0;
        imageItem.sprite = null;
        nameItem.text  = null;
        _item = null;
        isFull = false;
    }
}

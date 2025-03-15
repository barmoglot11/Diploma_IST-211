using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using INVENTORY;
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    
    public int CellsCount {get; private set;}
    public List<Inv_Item> Items = new List<Inv_Item>();
    public List<Inv_Cell> Cells = new List<Inv_Cell>();

    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDesc;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    public void SetInventory()
    {
        for (var i = 0; i < Items.Count; i++)
        {
            var cellItem = Cells[i].invItem;
            cellItem.CopyData(Items[i]);
            Cells[i].UpdateInfo();
        }
    }

    public void AddItem(Inv_Item item)
    {
        Items.Add(item);
        Debug.Log(Items.Count);
    }

    public void ShowItem(Inv_Item item = null)
    {
        if (item == null)
        {
            itemName.text = "ТЕКСТ";
            itemDesc.text = "ТЕКСТ";
        }
        else
        {
            itemName.text = item.itemName;
            itemDesc.text = item.itemDescription;
        }
        
    }
}
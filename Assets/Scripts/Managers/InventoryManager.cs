using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using INVENTORY;
using UnityEngine.Serialization;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public ItemsConfigData configData;
    public List<Inv_Cell> cells;

    public Image itemImage;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDesc;
    public Button previewButton;
    public PreviewScreen PreviewScreen;
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

    public void Start()
    {
        StartTest();
        SetInventory();
    }

    private void StartTest()
    {
        AddItem("Carbon Gun");
    }
    public void SetInventory()
    {
        foreach (var cell in cells)
        {
            if (cell.invItem != null)
            {
                cell.UpdateInfo();
            }
        }
    }

    public void AddItem(string itemName)
    {
        if (string.IsNullOrEmpty(itemName))
        {
            Debug.LogWarning("Item name cannot be null or empty." + itemName);
        }

        var item = configData.items.FirstOrDefault(i => string.Equals(i.itemName, itemName, StringComparison.CurrentCultureIgnoreCase));

        if (item == null)
        {
            Debug.LogWarning($"Item '{itemName}' not found.");
            return;
        }

        var cell = cells.FirstOrDefault(cell => string.IsNullOrEmpty(cell.invItem.itemName));

        if (cell != null)
        {
            cell.invItem = item;
        }
        else
        {
            Debug.LogWarning("No available cell with null invItem.");
        }
    }

    public void ShowItem(Inv_Item item = null)
    {
        if (item == null) return;
        itemName.text = item.itemName;
        itemImage.sprite = item.itemImage;
        itemDesc.text = item.itemDescription;
        if(item.itemPrefab == null)
            previewButton.gameObject.SetActive(false);
        
    }

    public void SetPreview(Inv_Item item)
    {
        PreviewScreen.CreateItem(item.itemPrefab);
    }
}
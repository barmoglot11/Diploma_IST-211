using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using INVENTORY;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public ItemsConfigData configData;
    public List<Inv_Cell> cells;

    [SerializeField]
    GameObject cellsContainer;
    
    public Image itemImage;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDesc;
    public GameObject previewButton;
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
        cells.AddRange(cellsContainer.GetComponentsInChildren<Inv_Cell>());
        StartTest();
        SetInventory();
    }

    private void StartTest()
    {
        AddItem("Кремниевый пистолет");
    }
    public void SetInventory()
    {
        foreach (var cell in cells)
        {
            cell.UpdateInfo();
            cell.SetHover(cells);
        }
        ShowItem(cells[0].invItem);
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

    public Inv_Item GetItem(string itemName)
    {
        return configData.items.FirstOrDefault
            (i => string.Equals
                (i.itemName, itemName, StringComparison.CurrentCultureIgnoreCase)
            );
    }
    
    public void ShowItem(Inv_Item item)
    {
        itemName.text = item.itemName;

        if (!string.IsNullOrEmpty(item.itemName))
        {
            itemImage.sprite = item.itemImage;
            itemImage.color = new Color(255, 255, 255, 255);
        }
        else
        {
            itemImage.sprite = null;
            itemImage.color = new Color(0, 0, 0, 0);
        }

        itemDesc.text = item.itemDescription;
        previewButton.gameObject.SetActive(!string.IsNullOrEmpty(item.itemName));
    }

    public void SetPreviewAction(UnityAction action)
    {
        previewButton.GetComponentInChildren<Button>().onClick.AddListener(action);
    }
    
    public void SetPreview(Inv_Item item)
    {
        PreviewScreen.CreateItem(item.itemPrefab);
    }
}
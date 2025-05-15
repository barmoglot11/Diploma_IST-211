using System;
using System.Collections.Generic;
using System.Linq;
using INVENTORY;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [Header("Configuration")]
    [SerializeField] private ItemsConfigData _configData;
    
    [Header("UI References")]
    [SerializeField] private GameObject _cellsContainer;
    [SerializeField] private Image _itemImage;
    [SerializeField] private TextMeshProUGUI _itemNameText;
    [SerializeField] private TextMeshProUGUI _itemDescText;
    [SerializeField] private GameObject _previewButton;
    [SerializeField] private PreviewScreen _previewScreen;

    [Header("Debug")]
    [SerializeField] private bool _enableTestItem = true;
    [SerializeField] private string _testItemName = "Кремниевый пистолет";

    private List<Inv_Cell> _cells = new List<Inv_Cell>();
    private Button _cachedPreviewButton;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        Initialize();
    }

    private void Initialize()
    {
        CacheComponents();
        SetupInventory();
        
        if (_enableTestItem)
        {
            AddItem(_testItemName);
        }
    }

    private void CacheComponents()
    {
        if (_cellsContainer != null)
        {
            _cells.AddRange(_cellsContainer.GetComponentsInChildren<Inv_Cell>(true));
        }

        _cachedPreviewButton = _previewButton?.GetComponentInChildren<Button>();
    }

    public void SetupInventory()
    {
        foreach (var cell in _cells)
        {
            cell.UpdateInfo();
            cell.SetHover(_cells);
        }

        if (_cells.Count > 0)
        {
            ShowItem(_cells[0].invItem);
        }
    }

    public bool HasItem(string itemName)
    {
        if (string.IsNullOrWhiteSpace(itemName))
        {
            Debug.LogWarning("Item name is empty or null.");
            return false;
        }

        return _cells.Any(cell => 
            cell.invItem != null && 
            !string.IsNullOrWhiteSpace(cell.invItem.itemName) &&
            string.Equals(cell.invItem.itemName, itemName, StringComparison.OrdinalIgnoreCase));
    }
    
    public void AddItem(string itemName)
    {
        if (string.IsNullOrWhiteSpace(itemName))
        {
            Debug.LogWarning($"Tried to add empty item name", this);
            return;
        }

        var item = GetItem(itemName);
        if (item == null)
        {
            Debug.LogWarning($"Item '{itemName}' not found in config", this);
            return;
        }

        if (HasItem(itemName))
        {
            Debug.LogWarning($"Item '{itemName}' already exists in inventory", this);
            return;
        }
        
        var emptyCell = _cells.FirstOrDefault(c => string.IsNullOrWhiteSpace(c.invItem.itemName));
        if (emptyCell == null)
        {
            Debug.LogWarning("Inventory is full - no empty cells available", this);
            return;
        }

        emptyCell.invItem = item;
        emptyCell.UpdateInfo();
    }

    public Inv_Item GetItem(string itemName)
    {
        if (_configData == null || _configData.items == null)
        {
            Debug.LogError("Inventory configuration missing", this);
            return null;
        }

        return _configData.items.FirstOrDefault(
            i => string.Equals(i.itemName, itemName, StringComparison.OrdinalIgnoreCase));
    }

    public void ShowItem(Inv_Item item)
    {
        if (item == null)
        {
            ClearItemDisplay();
            return;
        }

        _itemNameText.text = item.itemName;
        _itemDescText.text = item.itemDescription;

        if (item.itemImage != null)
        {
            _itemImage.sprite = item.itemImage;
            _itemImage.color = Color.white;
        }
        else
        {
            _itemImage.color = Color.clear;
        }

        _previewButton.gameObject.SetActive(!string.IsNullOrWhiteSpace(item.itemName));
    }

    private void ClearItemDisplay()
    {
        _itemNameText.text = string.Empty;
        _itemDescText.text = string.Empty;
        _itemImage.sprite = null;
        _itemImage.color = Color.clear;
        _previewButton.gameObject.SetActive(false);
    }

    public void SetPreviewAction(UnityAction action)
    {
        if (_cachedPreviewButton != null)
        {
            _cachedPreviewButton.onClick.RemoveAllListeners();
            _cachedPreviewButton.onClick.AddListener(action);
        }
    }

    public void SetPreview(Inv_Item item)
    {
        if (_previewScreen != null && item != null)
        {
            _previewScreen.CreateItem(item.itemPrefab);
        }
    }
}
using INVENTORY;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Collider))]
[DisallowMultipleComponent]
public class InteractiveItem : MonoBehaviour, IInteractable
{
    [Header("Item Settings")]
    [SerializeField] private string _itemName;
    [SerializeField] private GameObject _objectToEnable;
    
    [Header("UI References")]
    [FormerlySerializedAs("addItemScreen")]
    [SerializeField] private AddItemScreen _addItemScreen;
    [FormerlySerializedAs("toggleScript")]
    [SerializeField] private TogglePanelOnE _toggleScript;
    
    [Header("Debug")]
    [SerializeField] private bool _debugLogs = true;
    
    // Инкапсулированные свойства
    public string ItemName => _itemName;
    private InventoryManager Inventory => InventoryManager.Instance;
    private InteractionManager InteractionManager => InteractionManager.Instance;

    private void OnValidate()
    {
        // Автоматически находим компоненты если они не установлены
        if (_toggleScript == null)
            _toggleScript = FindObjectOfType<TogglePanelOnE>();
        
        if (_addItemScreen == null)
            _addItemScreen = FindObjectOfType<AddItemScreen>();
        
        // Проверяем коллайдер
        var collider = GetComponent<Collider>();
        if (!collider.isTrigger)
        {
            collider.isTrigger = true;
            Debug.LogWarning($"Collider on {name} was not set as trigger. Fixed automatically.");
        }
    }

    public void Interact()
    {
        if (string.IsNullOrEmpty(_itemName))
        {
            Debug.LogError("Item name is not set!", this);
            return;
        }

        try
        {
            // Добавляем предмет в инвентарь
            if (Inventory != null)
            {
                Inventory.AddItem(_itemName);
                if (_debugLogs) Debug.Log($"Added item: {_itemName}");
            }

            // Показываем UI
            _addItemScreen?.SetupScreen(_itemName);
            _toggleScript?.ToggleCanvasesAction();
            
            // Активируем связанный объект
            if (_objectToEnable != null)
            {
                _objectToEnable.SetActive(true);
                if (_debugLogs) Debug.Log($"Enabled object: {_objectToEnable.name}");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Interaction failed: {e.Message}", this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) 
            return;
        
        if (InteractionManager != null)
        {
            InteractionManager.AddInteract(Interact);
            if (_debugLogs) Debug.Log($"Registered interaction for {_itemName}");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) 
            return;
        
        RemoveInteraction();
    }

    private void OnDisable() => RemoveInteraction();
    private void OnDestroy() => RemoveInteraction();

    private void RemoveInteraction()
    {
        if (InteractionManager != null)
        {
            InteractionManager.DeleteInteract(Interact);
            if (_debugLogs) Debug.Log($"Unregistered interaction for {_itemName}");
        }
    }

    // Метод для ручной установки параметров (можно использовать для настройки через код)
    public void Configure(string itemName, AddItemScreen screen, TogglePanelOnE toggle, GameObject objectToEnable = null)
    {
        _itemName = itemName;
        _addItemScreen = screen;
        _toggleScript = toggle;
        _objectToEnable = objectToEnable;
    }
}
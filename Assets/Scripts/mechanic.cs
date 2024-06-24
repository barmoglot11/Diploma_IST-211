using UnityEngine;
using Zenject;

public class mechanic : MonoBehaviour
{
    public InventoryService inventoryService;
    
    [Inject]
    void Mechanic(InventoryService _inventoryService) 
    {
        inventoryService = _inventoryService;
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.TryGetComponent<Item>(out var item))
            inventoryService.AddItemInSlot(item);
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            inventoryService.gameObject.SetActive(false);
        }
        if(Input.GetKeyUp(KeyCode.E))
        {
            inventoryService.gameObject.SetActive(true);
        }
        if(Input.GetKey(KeyCode.Alpha1))
        {
            //var temp = false;
           //inventoryService.gameObject.SetActive(temp);
            inventoryService.GetItemFromInventory(typeof(MasterKeyDataInventory), transform.position + new Vector3(0,0,5));
            Debug.Log(typeof(MasterKeyDataInventory));
        }
        if(Input.GetKey(KeyCode.Alpha2))
        {
            //var temp = false;
           //inventoryService.gameObject.SetActive(temp);
            inventoryService.GetItemFromInventory(typeof(FlashlightDataInventory), transform.position + new Vector3(0,0,5));
            Debug.Log(typeof(FlashlightDataInventory));
        }
    }
}

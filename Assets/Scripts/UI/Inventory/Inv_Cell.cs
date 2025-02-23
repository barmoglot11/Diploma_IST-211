using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace INVENTORY
{
    [RequireComponent(typeof(BoxCollider))]
    public class Inv_Cell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        InventoryManager Inventory => InventoryManager.Instance;
        public Inv_Item invItem;
    
        public Image imageContainer;
        public void UpdateInfo()
        {
            imageContainer.sprite = invItem.itemImage;
        }

        public void AddItem(Inv_Item item)
        {
            invItem.CopyData(item);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Inventory.ShowItem(invItem);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Inventory.ShowItem();
        }
    }
}


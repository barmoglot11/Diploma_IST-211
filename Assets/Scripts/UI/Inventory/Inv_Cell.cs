using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace INVENTORY
{
    [RequireComponent(typeof(BoxCollider))]
    public class Inv_Cell : MonoBehaviour
    {
        InventoryManager Inventory => InventoryManager.Instance;
        public Inv_Item invItem;
    
        public Image imageContainer;
        public void UpdateInfo()
        {
            if (!string.IsNullOrEmpty(invItem.itemName))
            {
                imageContainer.sprite = invItem.itemImage;
                GetComponent<Button>().onClick.AddListener(ShowThisItem);
            }
            else
                imageContainer.color = new Color(0, 0, 0, 0);
        }

        private void ShowThisItem()
        {
            Inventory.ShowItem(invItem);
            Debug.Log($"Created link for {invItem.itemName}");
            Inventory.previewButton.onClick.AddListener(SetPreview);
        }
        
        private void SetPreview()
        {
            Inventory.SetPreview(invItem);
        }
    }
}


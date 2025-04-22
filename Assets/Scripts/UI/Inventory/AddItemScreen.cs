using AYellowpaper.SerializedCollections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace INVENTORY
{
    public class AddItemScreen : MonoBehaviour
    {
        public Image itemImage;
        public TextMeshProUGUI itemNameText;
        public TextMeshProUGUI itemDescriptionText;
        public SerializedDictionary<string, InteractiveObject> itemToDialogue;
        public void SetupScreen(string itemName)
        {
            var item = InventoryManager.Instance.GetItem(itemName);
            itemImage.sprite = item.itemImage;
            itemNameText.text = item.itemName;
            itemDescriptionText.text = item.itemDescription;
        }

        private void OnDisable()
        {
            if (itemToDialogue.ContainsKey(itemNameText.text))
            {
                itemToDialogue[itemNameText.text].Interact();
            }
        }
    }
}
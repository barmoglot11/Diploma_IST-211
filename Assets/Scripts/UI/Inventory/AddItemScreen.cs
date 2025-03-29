using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace INVENTORY
{
    public class AddItemScreen : MonoBehaviour
    {
        public Image itemImage;
        public TextMeshProUGUI itemName;
        public TextMeshProUGUI itemDescription;

        public void SetupScreen(Inv_Item item)
        {
            itemImage.sprite = item.itemImage;
            itemName.text = item.itemName;
            itemDescription.text = item.itemDescription;
        }
    }
}
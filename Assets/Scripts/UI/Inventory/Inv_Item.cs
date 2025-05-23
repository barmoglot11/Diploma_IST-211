using UnityEngine;

namespace INVENTORY
{
    [System.Serializable]
    public class Inv_Item
    {
        public string itemName;
        public Sprite itemImage;
        public GameObject itemPrefab;
        [TextArea(minLines: 5, maxLines: 20)]
        public string itemDescription;

        public void CopyData(Inv_Item other)
        {
            itemName = other.itemName;
            itemImage = other.itemImage;
            itemPrefab = other.itemPrefab;
            itemDescription = other.itemDescription;
        }
    }
}

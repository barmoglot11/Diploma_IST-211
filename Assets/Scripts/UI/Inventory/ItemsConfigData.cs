using UnityEngine;

namespace INVENTORY
{
    [CreateAssetMenu(fileName = "ItemsConfigData", menuName = "Inventory", order = 0)]
    public class ItemsConfigData : ScriptableObject
    {
        public Inv_Item[] items;

        public Inv_Item GetConfig(string itemName)
        {
            itemName = itemName.ToLower();

            for (int i = 0; i < items.Length; i++)
            {
                Inv_Item data = items[i];
                
                if(string.Equals(itemName, data.itemName.ToLower()))
                    return data;
            }

            return null;
        }
    }
}
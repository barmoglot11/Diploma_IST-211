using System;
using UnityEngine;

namespace _TEST_
{
    public class AddItemOnActivate : MonoBehaviour
    {
        public string itemName;

        private void OnEnable()
        {
            InventoryManager.Instance.AddItem(itemName);
        }
    }
}
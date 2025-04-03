using System.Collections.Generic;
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

        private QuestButton hover => GetComponent<QuestButton>();
        
        public Image imageContainer;
        public void UpdateInfo()
        {
            if (!string.IsNullOrEmpty(invItem.itemName))
            {
                imageContainer.sprite = invItem.itemImage;
                imageContainer.color = new Color(255, 255, 255, 255);
                
            }
            else
            {
                imageContainer.sprite = null;
                imageContainer.color = new Color(0, 0, 0, 0);
            }
            GetComponent<Button>().onClick.AddListener(ShowThisItem);
        }

        private void ShowThisItem()
        {
            Inventory.ShowItem(invItem);
            Debug.Log($"Created link for {invItem.itemName}");
            Inventory.SetPreviewAction(SetPreview);
        }
        
        private void SetPreview()
        {
            Inventory.SetPreview(invItem);
        }

        public void SetHover(List<Inv_Cell> questButtons)
        {
            QuestButton[] buttons = new QuestButton[questButtons.Count-2];
            var buttonsList = new List<Inv_Cell>();
            buttonsList.AddRange(questButtons);
            buttonsList.Remove(this);
            for (int i = 0; i < buttonsList.Count - 1; i++)
            {
                buttons[i] = buttonsList[i].GetComponent<QuestButton>();
            }
            hover.otherButtons = buttons;
        }
    }
}


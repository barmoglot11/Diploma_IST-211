using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField]
    private TMP_Text countItem;
    
    [SerializeField]
    private Image imageItem;

    [SerializeField]
    private TMP_Text nameItem;
    public bool isFull = false;

    public void take(Item item)
    {
        imageItem.sprite = item.itemSprite;
        countItem.text = item.countItem.ToString();
        nameItem.text = item.nameOfItem;

        isFull = true;
    }
}

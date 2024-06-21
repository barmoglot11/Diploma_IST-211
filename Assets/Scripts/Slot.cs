using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int slotIndex;
    public bool isFull;
    public void take(Item item)
    {
        GetComponent<Image>().sprite = item.itemSprite;
        isFull = true;
    }
}

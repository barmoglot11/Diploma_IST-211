using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int slotIndex;
    public bool isFull = false;
    public void take(Item item)
    {
        Debug.Log(GetComponent<Image>().sprite );
        Debug.Log(item.itemSprite);
        GetComponent<Image>().sprite = item.itemSprite;
        Debug.Log(GetComponent<Image>().sprite);
        slotIndex = item.a;
        Debug.Log(slotIndex);

        Debug.Log("Установлен");
    }
}

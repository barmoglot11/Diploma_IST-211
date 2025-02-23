using UnityEngine;

namespace INVENTORY
{
    
}
public class Inv_Item : MonoBehaviour
{
    public string itemName;
    public Sprite itemImage;
    
    public string itemDescription;

    public void CopyData(Inv_Item other)
    {
        itemName = other.itemName;
        itemImage = other.itemImage;
        itemDescription = other.itemDescription;
    }
}
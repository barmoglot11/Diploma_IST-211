using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item Data", order = 51)]
public class IItemData : ScriptableObject
{
    public string nameOfItem;
    public int MaxCountItem;
    public int CountItem;
    public Sprite itemSprite;
    public bool isStackable = false;
}

using TMPro;
using UnityEngine;

public class Item : MonoBehaviour
{
    //public ScriptableObject item;
    public bool isStackable = true;
    public int countItem;
    public TMP_Text nameOfItem;
    public Sprite itemSprite;
    public int a = 1;
    void Start()
    {
        nameOfItem.text = "Item";
    }


}

internal interface IStackable
{
    public bool isStackable{get;set;}
}
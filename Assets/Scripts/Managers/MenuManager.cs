using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject[] menuPages;

    public void ChangePage(string pageName)
    {
        switch (pageName)
        {
            case "Diary":
                DisablePages();
                menuPages[0].SetActive(true);
                break;
            case "Inventory":
                DisablePages();
                menuPages[1].SetActive(true);
                InventoryManager.Instance.SetInventory();
                break;
            case "Map":
                DisablePages();
                menuPages[2].SetActive(true);
                break;
        }
    }

    private void DisablePages()
    {
        foreach (var page in menuPages)
        {
            page.SetActive(false);
        }
    }
}

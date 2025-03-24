using UnityEngine;
using UnityEngine.UI;

namespace INVENTORY
{
    public class PreviewScreen : MonoBehaviour
    {
        public GameObject itemContainer;
        public GameObject itemPrefab;
        public Button returnButton;
        public void CreateItem(GameObject item)
        {
            itemPrefab = Instantiate(item, itemContainer.transform);
            itemPrefab.transform.localScale = new Vector3(200, 200, 200);
            itemPrefab.transform.Rotate(Vector3.up, -90f);
            returnButton.onClick.AddListener(ClosePanel);
        }

        public void ClosePanel()
        {
            Destroy(itemPrefab);
        }
    }
}
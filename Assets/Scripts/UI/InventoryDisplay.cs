using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Display the contents of the inventory in the agents UI, except for the flags
/// which continue to be rendered
/// </summary>
public class InventoryDisplay : MonoBehaviour
{
    public GameObject ItemSprite;
    public GameObject Inventory;
    private InventoryController _inventoryScript;

    const string TransparentSprite = "TransparentSprite";
    private List<GameObject> _inventoryImages = new List<GameObject>();
    private Sprite _emptySprite;

    // Use this for initialization
    void Start ()
    {
        _inventoryScript = Inventory.GetComponent<InventoryController>();
        _inventoryImages.Capacity = _inventoryScript.Capacity;

        // This is needed to ensure empty inventory slots are invisible
        _emptySprite = Resources.Load< Sprite>(TransparentSprite);

        for (int i = 0; i < _inventoryImages.Capacity; i++)
        {
            _inventoryImages.Add(Instantiate(ItemSprite));
            _inventoryImages[i].transform.SetParent( gameObject.transform, false);
        }
    }

    private void OnGUI()
    {
        int i = 0;

        // Display a sprite for each item in the inventory, as this is a dictionary we have to use foreach
        foreach (KeyValuePair<string, GameObject> item in _inventoryScript.Items)
        {
            //Dont display flags in the inventory display
            if (!item.Value.tag.Equals(Tags.Flag))
            {
                Sprite uiSprite = item.Value.GetComponent<InventorySprite>().UiSprite;
                _inventoryImages[i].GetComponent<Image>().sprite = uiSprite;
                i++;
            }
        }

        // Ensure any remaining slots are invisible
        for (int j = i; j < _inventoryImages.Capacity; j++)
        {
            _inventoryImages[j].GetComponent<Image>().sprite = _emptySprite;
        }
    }
}
    
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject InventoryMenu;
    private bool menuActivated;
    public Itemslot[] itemSlot;

    public ItemSos[] itemSOs;

    void Start()
    {
        InventoryMenu.SetActive(false);
        menuActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && menuActivated)
        {
            Time.timeScale = 1;
            disableInventory();
        }

        else if (Input.GetKeyDown(KeyCode.Tab) && !menuActivated)
        {
            Time.timeScale = 0;
            InventoryMenu.SetActive(true);
            menuActivated = true;
        }
    }

    public void disableInventory()
    {
        InventoryMenu.SetActive(false);
        menuActivated = false;
    }

    public bool UseItem(string itemName)
    {
        for (int x = 0; x < itemSOs.Length; x++)
        {
            // Debug.Log("-------------" + itemSOs[x].itemName + "___ " + itemName);
            if (itemSOs[x].itemName == itemName)
            {
                bool usable = itemSOs[x].UseItem();
                Debug.Log("usable = " + usable);
                return usable;
            }
        }
        return false;
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        Debug.Log("itemName = " + itemName + "quantity = " + quantity + "itemSprite = " + itemSprite);
        for (int i = 0; i < itemSlot.Length; i++)
        {

            if (itemSlot[i].isFull == false && itemSlot[i].name == name || itemSlot[i].quantity == 0)
            {
                int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription);
                if (leftOverItems > 0)
                {
                    leftOverItems = AddItem(itemName, leftOverItems, itemSprite, itemDescription);
                    return leftOverItems;
                }
                return 0;
            }
        }
        return quantity;

    }


    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }

}

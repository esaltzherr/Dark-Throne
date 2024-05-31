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
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab) && menuActivated)
        {
            //Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            menuActivated = false;
        }

        else if (Input.GetKeyDown(KeyCode.Tab) && !menuActivated)
        {
           //Time.timeScale = 0;
            InventoryMenu.SetActive(true);
            menuActivated = true;
        }
    }

    public bool UseItem(string itemName)
    {
        for(int x = 0; x < itemSOs.Length; x++)
        {
            if(itemSOs[x].itemName == itemName){
                bool usable = itemSOs[x].UseItem();
                Debug.Log("usable = " + usable);
                return usable;
            }
        }
        return false;
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite)
    {
        Debug.Log("itemName = " + itemName + "quantity = " + quantity + "itemSprite = " + itemSprite);
        for(int i = 0; i < itemSlot.Length; i++)
        {
            
            if(itemSlot[i].isFull == false && itemSlot[i].name == name || itemSlot[i].quantity == 0) 
            {
                int leftOverItems = itemSlot[i].AddItem(itemName,quantity,itemSprite);
                if(leftOverItems > 0){
                    leftOverItems = AddItem(itemName, leftOverItems, itemSprite);
                    return leftOverItems;
                }
                return 0;
            }
        }
        return quantity;
    
    }


    public void DeselectAllSlots()
    {
        for(int i = 0; i < itemSlot.Length; i++){
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }

}

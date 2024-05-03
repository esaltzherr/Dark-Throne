using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Itemslot : MonoBehaviour, IPointerClickHandler
{

    //Item Data//
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;

    [SerializeField]
    private int maxNumberOfItems;

    //Item SLOT//
    [SerializeField]
    private TMP_Text quantityText;

    [SerializeField]
    private Image itemImage;

    public GameObject selectedShader;
    public bool thisItemSelected;

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("inventory_canvas").GetComponent<InventoryManager>();
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite)
    {
        if(isFull){
            return quantity;
        }

        //update the name
        this.itemName = itemName;

        //update the Image
        this.itemSprite = itemSprite;
        itemImage.sprite = itemSprite;

        //update the quantity
        this.quantity += quantity;
        if(this.quantity >= maxNumberOfItems){
            quantityText.text = maxNumberOfItems.ToString();
            quantityText.enabled = true;
            isFull = true;

        //return the leftover
            int extraItems = this.quantity - maxNumberOfItems;
            this.quantity = maxNumberOfItems;
            return extraItems;
        }

        //update the quantity text
        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;

        return 0;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }


    public void OnLeftClick()
    {
        if(thisItemSelected){
            bool usable = inventoryManager.UseItem(itemName);
            if(usable){
                this.quantity -= 1;
                quantityText.text = this.quantity.ToString();
                if(this.quantity <= 0){
                    EmptySlot();
                }
            }
        }
        else{
        inventoryManager.DeselectAllSlots();
        selectedShader.SetActive(true);
        thisItemSelected = true;
        }
    }

    private void EmptySlot(){
        quantityText.enabled = false;
    }

    public void OnRightClick()
    {   
        //create a new item
        GameObject itemToDrop = new GameObject(itemName);
        Item newItem = itemToDrop.AddComponent<Item>();
        newItem.quantity = 1;
        newItem.itemName = itemName;
        newItem.sprite = itemSprite;

        //create and modify the SR
        SpriteRenderer sr = itemToDrop.AddComponent<SpriteRenderer>();
        sr.sprite = itemSprite;
        sr.sortingOrder = 5;
        sr.sortingLayerName = "Ground";

        //add a collider
        itemToDrop.AddComponent<BoxCollider2D>();

        //set location for drop item
        itemToDrop.transform.position = GameObject.FindWithTag("Player").transform.position + new Vector3(4, 0, 0);
        itemToDrop.transform.localScale = new Vector3(.5f, .5f, .5f);

        //Subtract the item
        this.quantity -= 1;
        quantityText.text = this.quantity.ToString();
        if(this.quantity <= 0){
            EmptySlot();
        }
    }
}
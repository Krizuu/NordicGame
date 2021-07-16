using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject inventory;
    private bool inventoryShow = false;
    public int inventorySize = 16;
    public List<Item> items = new List<Item>();

    //Array of all the inventory slots
    ItemSlot[] itemSlots;
    public Transform itemSlotsParent;

    //Item changed callback
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    private void Start()
    {
        onItemChangedCallback += updateUI; //Do onItemChangedCallback dodajê funkcjê updateUI
        itemSlots = itemSlotsParent.GetComponentsInChildren<ItemSlot>(); //£aduje wszystkie ItemSloty do tablicy
        Debug.Log("Slots: " + itemSlots.Length);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            toggleUI();
        if (inventoryShow == true)
            inventory.SetActive(true); //Wy³¹czenie widocznoœci ekwipunku
        else
            inventory.SetActive(false); //W³¹czenie widocznoœci ekwipunku
        if (Input.GetMouseButtonDown(0))
            pickUp();
    }

    public void addItem(Item item){
        if (items.Count != inventorySize){
            Debug.Log("Added " + item.name);
            items.Add(item);
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke(); //Odœwie¿enie ekwipunku
        }
        else
            Debug.Log("Inventory is full!");
    }

    public void removeItem(Item item)
    {
        
        bool status = items.Remove(item);
        if (status)
        {
            Debug.Log("Removed " + item.name);
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke(); //Odœwie¿enie ekwipunku
        }
        else
            Debug.Log("Failed to remove " + item.name);
    }

    public void toggleUI(){
        inventoryShow = !inventoryShow;
    }

    public void updateUI()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (i < items.Count)
                itemSlots[i].addItem(items[i]);
            else
                itemSlots[i].clearSlot();
        }
    }

    void pickUp()
    {
        Collider2D result = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if (result != null && result.transform.tag == "Item")
        {
            //Debug.Log(Vector2.Distance(transform.position, result.transform.position));
            if (Vector2.Distance(transform.position, result.transform.position) < 1.2)
            {
                addItem(result.transform.GetComponent<ItemPickup>().item);
                Destroy(result.transform.gameObject);
            }
        }
    }
}

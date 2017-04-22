using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : ItemDatabase {
    void Start() {
        InventoryToGUI();
    }

    [SerializeField]
    Transform inventoryGUI;
    [SerializeField]
    private string[] currentInventoryArray;
    public void InventoryToGUI() {
        for (int i = 0; i < inventoryGUI.childCount; i++) {
            inventoryGUI.GetChild(i).gameObject.SetActive(false);
        }

        currentInventoryArray = new string[currentInventory.Count];

        for (int i = 0; i < currentInventory.Count; i++) {
            inventoryGUI.GetChild(i).gameObject.SetActive(true);
            inventoryGUI.GetChild(i).GetComponent<Text>().text = currentInventory[i].Title;

            currentInventoryArray[i] = currentInventory[i].Slug;
        }
    }

    List<Item> currentInventory = new List<Item>();
	public void AddItem(int id) {
        Item itemToAdd = FetchItemByID(id);
        currentInventory.Add(itemToAdd);
        InventoryToGUI();
        Debug.Log(currentInventory[currentInventory.Count-1].Slug + " added to current inventory");
    }

    public void RemoveItem(int id) {
        for (int i = 0; i < currentInventory.Count; i++) {
            if (currentInventory[i].ID == id) {
                currentInventory.RemoveAt(id);
                return;
            }
        }
        Debug.LogWarning("could not remove item because item not found in inventory!");
    }
}

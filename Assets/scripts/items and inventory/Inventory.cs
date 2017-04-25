using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : ItemDatabase {
    void Start() {
        currentInventory = new Dictionary<Item, int>();
        InventoryToGUI();
    }

    [SerializeField]
    Transform inventoryGUI;
    public void InventoryToGUI() {
        for (int i = 0; i < inventoryGUI.childCount; i++) {
            inventoryGUI.GetChild(i).gameObject.SetActive(false);
        }

        int a = 0;
        foreach (var pair in currentInventory) {
            inventoryGUI.GetChild(a).gameObject.SetActive(true);
            inventoryGUI.GetChild(a).GetComponent<Text>().text = pair.Key.Title + " x" + pair.Value;
            a++;
        }
    }

    //List<Item> currentInventory = new List<Item>();
    Dictionary<Item, int> currentInventory;

    public void AddItem(int id) {
        Item itemToAdd = FetchItemByID(id);

        if (!currentInventory.ContainsKey(itemToAdd)) {
            currentInventory.Add(itemToAdd, 1);
        }
        else {
            currentInventory[itemToAdd]++;
        }

        InventoryToGUI();
        Debug.Log(FetchItemByID(id).Slug + " added to current inventory");
    }

    public void RemoveItem(int id) {
        if (currentInventory.ContainsKey(FetchItemByID(id))) {
            currentInventory.Remove(FetchItemByID(id));
            return;
        }

        Debug.LogWarning("could not remove item because item not found in inventory!");
    }

    public Dictionary<Item, int> CurrentInventory {
        get {
            return currentInventory;
        }
    }
}

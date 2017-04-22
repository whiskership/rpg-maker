using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemObject : Interactable {
    [SerializeField]
    int itemId;

    public int ItemID {
        get {
            return itemId;
        }
    }

    public Inventory inventoryDestination;
    void Update() {
        if (Input.GetKeyDown(KeyCode.Z) && IsWithinInteractableDistance == true) {
            inventoryDestination.AddItem(itemId);
            HideItem();
        }
    }

    void HideItem() {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        enabled = false;
    }
}

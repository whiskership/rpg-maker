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

    [SerializeField]
    bool hideItem = true;
    public bool CanHideItem {
        get {
            return hideItem;
        }
    }

    public Inventory inventoryDestination;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Z) && IsWithinInteractableDistance == true
            && GetComponent<ItemDialogue>().IsDialogueRunning == false) {
            inventoryDestination.AddItem(itemId);

            if (hideItem == true)
                HideItem();
        }
    }

    void HideItem() {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        enabled = false;
    }
}

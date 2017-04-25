using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent (typeof(ItemObject))]
public class ItemDialogue : Dialogue {
    ItemObject itemObj;
    void Awake() {
        itemObj = GetComponent<ItemObject>();
    }

    [SerializeField] Text textComp;

    [SerializeField] int currentDialogue;
    public int CurrentDialogue {
        get {
            return currentDialogue;
        }
        set {
            currentDialogue = value;
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Z) && GetComponent<Interactable>().IsWithinInteractableDistance == true) {
            currentCharacterTalking = transform;
            InputDialogue(textComp, GetComponent<ItemObject>().inventoryDestination.FetchItemByID(GetComponent<ItemObject>().ItemID).Title + " was \n found!", 3);

            if (itemObj.CanHideItem == true && IsDialogueEnded == true) {
                enabled = false;
            }
        }
    }


}
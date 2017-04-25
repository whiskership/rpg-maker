using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FetchItemsSystem : MonoBehaviour {
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            StartQuest();
            //FetchQuestSetup(wantedItemId, wantedItemAmount, "fuck meh");
        }
    }

    [SerializeField]
    RectTransform uiAnnounce;
    public void StartQuest() {
        uiAnnounce.gameObject.SetActive(true);
        for (int i = 0; i < wantedItemId.Length; i++) {
            Item item = inventoryToCheck.FetchItemByID(wantedItemId[i]);
            uiAnnounce.GetChild(i).GetComponent<Text>().text = item.Title + " x" + wantedItemAmount[i];
        }
    }

    [SerializeField]
    int[] wantedItemId;
    [SerializeField]
    int[] wantedItemAmount;
    [SerializeField]
    Inventory inventoryToCheck;

    [SerializeField]
    int dialogueToGoTo;

    public bool CheckForItems() {
        for (int i = 0; i < wantedItemId.Length; i++) {
            if (inventoryToCheck.CurrentInventory.ContainsKey(inventoryToCheck.FetchItemByID(wantedItemId[i]))
                && inventoryToCheck.CurrentInventory[inventoryToCheck.FetchItemByID(wantedItemId[i])] >= wantedItemAmount[i]) {
                Debug.Log("yes item found!");
                return true;
            }
        }
        Debug.Log("item not found");
        return false;
    }

    public int DialogueToGoTo {
        get {
            return dialogueToGoTo;
        }
    }
}

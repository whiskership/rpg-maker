using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfDialogueEvent : EventManager {
    CharacterDialogue charDial;
    void Start() {
        charDial = GetComponent<CharacterDialogue>();
    }

    [SerializeField]
    int whenThisDialogueEnds;

    [SerializeField]
    private int currentlyCheckingThisArray;

    bool alreadyRan;
    void Update() {
        /*if (whenThisDialogueEnds.Length != 0 && charDial.CurrentDialogue == whenThisDialogueEnds[currentlyCheckingThisArray] && charDial.IsDialogueEnded == true
            && GetComponent<Interactable>().IsWithinInteractableDistance == true) {
            Debug.Log("ran");
            ChangeNPCSDialogue(this.changeThisNpc[currentlyCheckingThisArray], this.changeToThisDialogue[currentlyCheckingThisArray]);
            //currentlyCheckingThisArray++;
        }*/

        if (charDial.CurrentDialogue == whenThisDialogueEnds && charDial.IsDialogueEnded == true && GetComponent<Interactable>().IsWithinInteractableDistance == true && alreadyRan == false) {
            alreadyRan = true;

            Debug.Log("ran");

            if (changeThisNpc != null) {
                ChangeNPCSDialogue(changeThisNpc, changeToThisDialogue);
            }

            if (soundToPlay != null) {
                PlaySound(soundToPlay);
            }

            if (urlToOpen != "") {
                OpenURL(urlToOpen);
            }

            if (itemToAddId != -1) {
                AddItem(itemToAddId);
            }

            if (npcToNotify != null) {
                ActivateNotifOnNPC(npcToNotify);
            }

            if (fadeIn == true) {
                FadeIn();
            }
        }

        if (alreadyRan == true && charDial.IsDialogueStarted == true) {
            alreadyRan = false;
        }
    }
}

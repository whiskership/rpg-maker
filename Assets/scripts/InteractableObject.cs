using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : Interactable {
    void LateUpdate() {
        if (Dialogue.CurrentCharacterTalking == transform && IsWithinInteractableDistance && GetComponent<Dialogue>().IsDialogueStarted == true) {
            //StartCoroutine(FreezeAxisCR(true));
            interactingWith.GetComponent<Movement>().FreezeAxisVector = true;

        }
        else if (Dialogue.CurrentCharacterTalking == transform && IsWithinInteractableDistance && GetComponent<Dialogue>().IsDialogueEnded == true) {
            //StartCoroutine(FreezeAxisCR(false));
            interactingWith.GetComponent<Movement>().FreezeAxisVector = true;

        }
    }

    IEnumerator FreezeAxisCR(bool freeze) {
        yield return null;
        interactingWith.GetComponent<Movement>().FreezeAxisVector = freeze;
    }
}
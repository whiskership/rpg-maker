using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : EventManager {
    int currentlyCheckingThisArray;
    void OnTriggerEnter2D(Collider2D coll) {
        if (changeThisNpc != null) {
            ChangeNPCSDialogue(changeThisNpc, changeToThisDialogue);
        }

        if (soundToPlay != null) {
            PlaySound(soundToPlay);
        }
        
        if (npcToNotify != null) {
            ActivateNotifOnNPC(npcToNotify);
        }

        if (positionToTeleport != null) {
            TeleportTo(coll.transform, positionToTeleport);
        }

        if (dialogue != null) {
            if (whoIsTalking != null)
                ActivateDialogue(dialogue, whoIsTalking);
            else
                ActivateDialogue(dialogue);
        }

        if (selfDestruct == true) {
            SelfDestruct();
        }

        if (characterToLookAt != null) {
            StartCoroutine(LookAtCharacterUntilDialogueEnds(characterToLookAt));
        }   
    }
}

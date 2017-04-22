using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
    [Header("Change NPC's Dialogue")]
    [SerializeField]
    protected Transform changeThisNpc;
    [SerializeField]
    protected int changeToThisDialogue;

    protected void ChangeNPCSDialogue(Transform changeThisNpc, int changeToThisDialogue) {
        changeThisNpc.GetComponent<CharacterDialogue>().CurrentDialogue = changeToThisDialogue;
    }

    [Header("Play Sound")]
    [SerializeField]
    protected AudioClip soundToPlay;

    protected void PlaySound(AudioClip theSoundToPlay) {
        if (SFXManager.Instance != null) {
            SFXManager.Instance.GetComponent<AudioSource>().clip = theSoundToPlay;
            SFXManager.Instance.GetComponent<AudioSource>().Play();
        }
        else if (GetComponent<AudioSource>() != null) {
            GetComponent<AudioSource>().clip = theSoundToPlay;
            GetComponent<AudioSource>().Play();
        }
        else {
            Debug.LogWarning("no audio source bro");
        }
    }

    [Header("Activate Notification")]
    [SerializeField]
    protected Transform npcToNotify;
    protected void ActivateNotifOnNPC(Transform theNpcToNotify) {
        theNpcToNotify.GetComponent<Interactable>().CreateNotification(new Vector2(0, 16));
    }

    [Header("Teleport Somewhere")]
    [SerializeField]
    protected Transform whatToTelport;
    [SerializeField]
    protected Transform positionToTeleport;

    protected void TeleportTo(Transform theWhatToTeleport, Transform thePositionToTeleport) {
        StartCoroutine(TeleportToCR(theWhatToTeleport, thePositionToTeleport));
    }

    bool isTeleporting;
    IEnumerator TeleportToCR(Transform a, Transform b) {
        whatToTelport.GetComponent<Movement>().FreezeAxisVector = true;
        iTween.CameraFadeTo(1, 0.8f);
        isTeleporting = true;

        yield return new WaitForSeconds(1.6f);
        a.GetComponent<Movement>().MoveTo(b.position);

        isTeleporting = false;
        iTween.CameraFadeTo(0, 0.8f);
        whatToTelport.GetComponent<Movement>().FreezeAxisVector = false;
    }

    [Header("Activate a dialogue")]
    [SerializeField]
    protected TextAsset dialogue;
    [SerializeField]
    protected Transform whoIsTalking;

    protected void ActivateDialogue(TextAsset theText, Transform theWhoIsTalking) {
        theWhoIsTalking.GetComponent<CharacterDialogue>().RunDialogue(theText);
    }

    protected void ActivateDialogue(TextAsset theText) {
        Invisible_Dialogue.Instance.RunDialogue(theText);
    }

    [Header("Open A URL")]
    [SerializeField]
    protected string urlToOpen;

    protected void OpenURL(string theURLToOpen) {
        Application.OpenURL(urlToOpen);
    }

    [Header("Add an item to inventory!")]
    [SerializeField]
    protected int itemToAddId = -1;
    [SerializeField]
    protected Inventory inventoryToAddTo;
    protected void AddItem(int theItemToAddId) {
        inventoryToAddTo.AddItem(theItemToAddId);
    }

    [Header("self destruct?")]
    [SerializeField]
    protected bool selfDestruct;
    protected void SelfDestruct() {
        StartCoroutine(SelfDestructWhenSafe());
    }

    IEnumerator SelfDestructWhenSafe() {
        while (isTeleporting == true)
            yield return null;

        gameObject.SetActive(false);
    }

    [Header("Fade In")]
    [SerializeField]
    protected bool fadeIn;
    protected void FadeIn() {
        iTween.CameraFadeTo(1, 1.5f);
    }

    [Header("Look at character until dialogue ends.")]
    [SerializeField]
    protected Transform characterToLookAt;
    protected IEnumerator LookAtCharacterUntilDialogueEnds(Transform theCharacterToLookAt) {
        while (Dialogue.ActiveDialogueComp.IsDialogueStarted == false) {
            yield return null;
        }

        PlayerInfo.Instance.GetComponent<KeyboardMovement>().FreezeAxisVector = true;
        CameraMovement.CanFollowTarget = false;

        Vector3 cameraTarget = characterToLookAt.position;
        cameraTarget = cameraTarget + new Vector3(0, 0, -10);

        while (Dialogue.ActiveDialogueComp.IsDialogueRunning == true) {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, cameraTarget, 0.06f);
            yield return null;
        }

        CameraMovement.CanFollowTarget = true;
        PlayerInfo.Instance.GetComponent<KeyboardMovement>().FreezeAxisVector = false;
    }
}
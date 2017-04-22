using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDialogue : Dialogue {
    [SerializeField]
    TextAsset[] textFiles;

    [SerializeField]
    int currentDialogue;

    [SerializeField]
    Color textCompColor = Color.white;

    //ok fuck the public text comp, lets make it a singleton
    [SerializeField]
    Text textComp;
    private void Start() {
        //textComp = TextOutputSingleton.Instance.transform.GetComponent<Text>();
    }

    public int CurrentDialogue {
        get {
            return currentDialogue;
        }
        set {
            currentDialogue = value;
        }
    }

    void LateUpdate() {
        base.LateUpdate();

        if (Input.GetKeyDown(KeyCode.Z) && (GetComponent<Interactable>().IsWithinInteractableDistance == true
            /*|| currentCharacterTalking == transform*/)) {
            
            /*if (openDialogueSFX != null && currentCharacterTalking == null) {
                GetComponent<AudioSource>().clip = openDialogueSFX;
                GetComponent<AudioSource>().Play();
            }*/

            textComp.transform.parent.GetComponent<Image>().color = textCompColor;

            RunDialogue(textFiles[currentDialogue]);
            currentCharacterTalking = transform;
        }
    }

    public void RunDialogue(TextAsset dialogueText) {
        InputDialogue(textComp, dialogueText, 3);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Invisible_Dialogue : Dialogue {
    static public Invisible_Dialogue Instance { get; set; }
    void Awake() {
        if (Instance == null) Instance = this;
    }

    [SerializeField]
    Text textComp;
    public void RunDialogue(TextAsset dialogueTxt) {
        InputDialogue(textComp, dialogueTxt);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Z) && isDialogueActive == true) {
            InputDialogue(textComp, "", 3);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour {
    public float ogCharsPerSecond;
    public DialogueOptions optionsMenu = new DialogueOptions();

    private void Start() {
        ogCharsPerSecond = charsPerSecond;
    }

    int chosenOption;
    protected void LateUpdate() {
        if (Input.GetKeyDown(KeyCode.Z)
            && IsDialogueRunning == true && MenuSelect.Instance.IsMenuOpen==true ) {
            chosenOption = MenuSelect.Instance.ConfirmedSelection;
            MenuSelect.Instance.ClearAndCloseAllOptions();

            currentLine = optionsMenu.optionLines[chosenOption] + 1 - 3;
            optionsMenu.StartLine = optionsMenu.optionLines[chosenOption] + 1;

            optionsMenu.ProcessChoicesDialogue(textArray);
            textArray = optionsMenu.ModifiedArray;
        }
    }

    protected static Transform currentCharacterTalking;
    protected bool isDialogueActive;
    static Dialogue activeDialogueComp;

    string ogOutputString;
    Text activeTextComp;
    void StartDialogueInstance() {
        if (GetComponent<Interactable>() != null && GetComponent<Interactable>().NotificationActive == true)
            GetComponent<Interactable>().RemoveNotification();
        
        //doesnt really make sense to do this. but change if needed
        if (GetComponent<Interactable>() != null && GetComponent<Interactable>().IsWithinInteractableDistance)
            PlayerInfo.Instance.GetComponent<KeyboardMovement>().FreezeAxisVector = true;

        activeDialogueComp = this;
        activeTextComp.text = "";

        isDialogueEnded = false;
        isDialogueActive = true;
        isDialogueRunning = true;

        activeTextComp.transform.parent.gameObject.SetActive(true);
    }

    protected string[] textArray;
    int currentLine;
    string outputString;

    public void InputDialogue(Text textComp = null, TextAsset textFile = null, int linesPerPage = 3) {
        //runs when text asset is loaded for the first time
        if (textArray == null || textArray.Length == 0) {

            textArray = textFile.ToString().Split('\n'); //this is the only lince different from the alt func

            while (textArray.Length % linesPerPage != 0) {
                System.Array.Resize(ref textArray, textArray.Length + 1);
            }

            activeTextComp = textComp;

            StartDialogueInstance();

            optionsMenu.StartLine = 0;

            optionsMenu.ProcessChoicesDialogue(textArray);
            textArray = optionsMenu.ModifiedArray;
        }
        else if (MenuSelect.Instance.IsMenuOpen == true) {
            Debug.Log("menu is open");
            return;
        }
        else if (currentLetter < outputString.Length - 1 + charPadding) {
            //could be buggy
            //StopAllCoroutines();
            activeTextComp.text = ogOutputString;
            currentLetter = outputString.Length - 1 + charPadding;
            return;
        }
        else if (optionsMenu.EndLine >= currentLine && optionsMenu.EndLine < currentLine + 3) {
            EndDialogue();
            return;
        }
        else if (currentLine + linesPerPage < textArray.Length) {
            currentLine += linesPerPage;

            charPadding = 0;
            lastColorCharPos = 0;
        }
        else {
            EndDialogue();
            return;
        }

        outputString = textArray[currentLine];
        int countedLines = 1;
        while (countedLines < linesPerPage) {
            outputString += "\n" + textArray[currentLine + countedLines];
            countedLines++;
        }

        currentLetter = 0;

        //resets dialogue speed in the beggining
        charsPerSecond = ogCharsPerSecond;

        ogOutputString = outputString;
        DialogueActivatedFunctions();

        activeTextComp.text = "";

        StartCoroutine(TextScrollOutput());
    }

    public void InputDialogue(Text textComp = null, string textFile = "", int linesPerPage = 3) {

        //runs when text asset is loaded for the first time
        if (textArray == null || textArray.Length == 0) {
            textArray = textFile.Split('\n'); //this is the only lince different from the alt func

            while (textArray.Length % linesPerPage != 0) {
                System.Array.Resize(ref textArray, textArray.Length + 1);
            }

            activeTextComp = textComp;

            StartDialogueInstance();
        }
        else if (currentLetter < outputString.Length - 1 + charPadding) {
            //could be buggy
            activeTextComp.text = ogOutputString;
            currentLetter = outputString.Length - 1;
            StopAllCoroutines();
            return;
        }
        else if (currentLine + linesPerPage < textArray.Length) {
            currentLine += linesPerPage;

            charPadding = 0;
            lastColorCharPos = 0;
        }
        else {
            EndDialogue();
            return;
        }

        outputString = textArray[currentLine];
        int countedLines = 1;
        while (countedLines < linesPerPage) {
            outputString += "\n" + textArray[currentLine + countedLines];
            countedLines++;
        }

        currentLetter = 0;

        //resets dialogue speed in the beggining
        charsPerSecond = ogCharsPerSecond;

        ogOutputString = outputString;
        DialogueActivatedFunctions();

        activeTextComp.text = "";

        StartCoroutine("TextScrollOutput");
    }

    int currentLetter;
    float charsPerSecond = 0.04f;
    int outt = 0;
    IEnumerator TextScrollOutput() {
        outt = 0;
        while (currentLetter < outputString.Length - 1 + charPadding) {
            if (outputString.Length + charPadding > currentLetter) {
                //output string stays the same
                activeTextComp.text = activeTextComp.text.Insert(currentLetter, outputString[outt].ToString());
            }

            //Debug.Log(ogCharsPerSecond);

            yield return new WaitForSeconds(charsPerSecond);

            outt++;
            currentLetter++;
        }

        activeTextComp.text = ogOutputString;
        //StopCoroutine("TextScrollOutput");
        //StopCoroutine("WaitForCharColor");
        if (colorCo != null)
            StopCoroutine(colorCo);

        if (optionsMenu.OpenLine != -1 && optionsMenu.OpenLine >= currentLine && optionsMenu.OpenLine < currentLine + 3) {
            MenuSelect.Instance.OpenMenu();
        }
    }

    private void DialogueActivatedFunctions() {
        if (outputString.Contains("<next>")) {
            if (outputString[outputString.IndexOf("<next") + "<next".Length] == '>') {
                StartCoroutine(QueNextEvent(currentCharacterTalking.GetComponent<CharacterDialogue>().CurrentDialogue + 1));
                outputString = outputString.Remove(outputString.IndexOf("<next>"), "<next>".Length);

                ogOutputString = ogOutputString.Remove(ogOutputString.IndexOf("<next>"), "<next>".Length);
            }
            else {
                StartCoroutine(QueNextEvent(int.Parse(outputString.Substring(outputString.IndexOf("<next") + "<next".Length + 1, 2))));
                outputString = outputString.Remove(outputString.IndexOf("<next"), "<next".Length + 4);

                ogOutputString = ogOutputString.Remove(ogOutputString.IndexOf("<next>"), "<next".Length + 4);
            }
        }

        if (outputString.Contains("<speed ")) {
            StartCoroutine(WaitUntilCharIsOnAction());
        }

        if (outputString.Contains("<color=") && outputString.IndexOf("<color=", lastColorCharPos)>lastColorCharPos) {
            StartCoroutine("WaitForCharColor");
        }

        //text movement effects
        if (outputString.Contains("<shaky>")) {
            //StartCoroutine(ShakeDialogue());
        }
    }

    IEnumerator ShakeDialogue(int startChar, int endChar) {
        string stringToWords = outputString.Substring(startChar, endChar);
        //Vector2[] charPosition = activeTextComp.text
        char[] charArray = stringToWords.ToCharArray();

        //feels like it could be pretty heavy on memory
        for (int i = 0; i < stringToWords.Length; i++) {
            Text newTextComp = gameObject.AddComponent<Text>() as Text;
            yield return null;

            newTextComp.text = charArray[i].ToString();
        }
    }

    int lastColorCharPos=0;
    int charPadding = 0;
    IEnumerator WaitForCharColor() {
        //bug 1: color function wont work if <color... index is on [0] 
        while (outt != outputString.IndexOf("<color=", lastColorCharPos) - 1 /*&& outputString.IndexOf("<color=", lastColorCharPos) != 0*/) {
            yield return null;
        }

        string hexCode = outputString.Substring(outputString.IndexOf("<color=", lastColorCharPos) + "<color=".Length, 7);

        colorCo = ChangeTextColor(hexCode, outputString.IndexOf("</color>")+charPadding);
        StartCoroutine(colorCo);

        //reset when switching to next paragraph
        lastColorCharPos = outt+1;

        DialogueActivatedFunctions();
    }

    IEnumerator colorCo = null;
    IEnumerator ChangeTextColor(string color, int endChar) {
        currentLetter += ("<color=" + color + ">").Length;

        if (currentLetter - ("<color=" + color + ">").Length + 1 < 0) {
            currentLetter -= ("<color=" + color + ">").Length;
            yield break;
        }

        activeTextComp.text = activeTextComp.text.Insert(currentLetter - ("<color=" + color + ">").Length + 1, "<color=" + color + ">");

        outputString = outputString.Remove(outputString.IndexOf("<color=" + color), "<color=".Length + 8);

        charPadding += ("<color=" + color + ">").Length;

        activeTextComp.text += "</color>";
        outputString = outputString.Remove(outputString.IndexOf("</color>", lastColorCharPos), "</color>".Length);

        while (currentLetter != endChar) {
            yield return null;
        }

        currentLetter += ("</color>").Length;
        charPadding += ("</color>").Length;
    }

    IEnumerator WaitUntilCharIsOnAction() {
        int changeTo = int.Parse(outputString.Substring(outputString.IndexOf("<speed ") + "<speed".Length + 1, 1));
        outputString = outputString.Remove(outputString.IndexOf("<speed "), "<speed 0>".Length);

        //remove for og string
        while (ogOutputString.Contains("<speed ")) {
            ogOutputString = ogOutputString.Remove(ogOutputString.IndexOf("<speed "), "<speed 0>".Length);
        }

        while (currentLetter < outputString.IndexOf("<speed ") - 1) {
            yield return null;
        }

        ChangeSpeed(changeTo);

        DialogueActivatedFunctions();
    }

    void ChangeSpeed(int speed) {
        switch (speed) {
            case 0:
            charsPerSecond = ogCharsPerSecond + 0.16f;
            break;

            case 1:
            charsPerSecond = ogCharsPerSecond + 0.08f;
            break;

            case 2:
            charsPerSecond = ogCharsPerSecond + 0.04f;
            break;

            case 3:
            charsPerSecond = ogCharsPerSecond;
            break;

            case 4:
            charsPerSecond = ogCharsPerSecond - 0.04f;
            break;

            case 5:
            charsPerSecond = ogCharsPerSecond - 0.08f;
            break;
        }
    }

    bool isDialogueEnded;

    //this is for resetting values
    public void EndDialogue() {
        currentCharacterTalking = null;

        MenuSelect.Instance.ClearAndCloseAllOptions();

        PlayerInfo.Instance.GetComponent<KeyboardMovement>().FreezeAxisVector = false;

        lastColorCharPos = 0;
        charPadding = 0;

        charsPerSecond = ogCharsPerSecond;

        isDialogueActive = false;

        textArray = null;
        activeTextComp.text = null;
        activeTextComp.transform.parent.gameObject.SetActive(false);
        currentLine = 0;
        isDialogueEnded = true;
        StartCoroutine(EndDialogueCR());

        isDialogueRunning = false;
    }

    IEnumerator QueNextEvent(int dialogueToGoTo) {

        while (isDialogueEnded == false) {
            yield return null;
        }

        yield return null;

        currentCharacterTalking.GetComponent<CharacterDialogue>().CurrentDialogue = dialogueToGoTo;
    }

    IEnumerator EndDialogueCR() {
        yield return null;
        isDialogueEnded = false;
    }

    //cool to find whats the main dialogue comp
    public static Dialogue ActiveDialogueComp {
        get {
            return activeDialogueComp;
        }
    }

    public static Transform CurrentCharacterTalking {
        get {
            return currentCharacterTalking;
        }
    }

    public bool IsDialogueStarted {
        get {
            return isDialogueActive;
        }
    }


    bool isDialogueRunning;
    public bool IsDialogueRunning {
        get {
            return isDialogueRunning;
        }
    }

    public bool IsDialogueEnded {
        get {
            return isDialogueEnded;
        }
    }
}

public class DialogueOptions  {

    List<string> newDialogueArray;
    List<int> openLines;
    public List<int> optionLines;
    List<int> endOptionLines;

    int openCount;
    int optionCount;
    int endLine;
    bool openActivated;

    int startLine;
    public void ProcessChoicesDialogue(string[] textArray) {
        string[] dialogueArray = textArray;

        newDialogueArray = new List<string>(dialogueArray);
        openLines = new List<int>();
        optionLines = new List<int>();
        endOptionLines = new List<int>();

        openCount = 0;
        optionCount = 0;
        endLine = -1;
        openActivated = false;

        for (int i = startLine; i < newDialogueArray.Count; i++) {
            if (newDialogueArray[i] != null) {
                if (newDialogueArray[i].Contains("<open>")) {
                    openCount++;
                    openLines.Add(i);
                    openActivated = true;

                    if (openCount == 1) {
                        newDialogueArray[i] = newDialogueArray[i].Remove(newDialogueArray[i].IndexOf("<open>"), "<open>".Length);
                    }
                }
                else if (newDialogueArray[i].Contains("</open>")) {
                    if (openCount == 1) {
                        newDialogueArray[i] = newDialogueArray[i].Remove(newDialogueArray[i].IndexOf("</open>"), "</open>".Length);
                    }

                    openCount--;
                }

                if (newDialogueArray[i].Contains("<option>")) {
                    if ((i + 1) % 3 != 0) {
                        newDialogueArray.Insert(i, "");
                        continue;
                    }

                    optionCount++;

                    if (openCount == 1) {
                        optionLines.Add(i);
                        MenuSelect.Instance.AddStringToMenuOption(newDialogueArray[i].Remove(newDialogueArray[i].IndexOf("<option>"), "<option>".Length).TrimStart());
                        newDialogueArray[i] = "";
                    }
                }
                else if (newDialogueArray[i].Contains("</option>")) {
                    optionCount--;

                    if (optionCount == -1) {
                        endLine = i;
                        newDialogueArray[i] = newDialogueArray[i].Remove(newDialogueArray[i].IndexOf("</option>"), "</option>".Length);
                        newDialogueArray[i + 1] = "";
                        newDialogueArray[i + 2] = "";
                        break;
                    }

                    if (openCount == 1) {
                        endOptionLines.Add(i);
                    }
                }
                if (openActivated && openCount == 0) {
                    endLine = i;
                    break;
                }

                newDialogueArray[i] = newDialogueArray[i].TrimStart();
            }
        }

        textArray = newDialogueArray.ToArray();
    }

    public string[] ModifiedArray {
        get {
            return newDialogueArray.ToArray();
        }
    }

    public int OpenLine {
        get {
            return openLines.Count > 0 ? openLines[0] : -1;
        }
    }

    public int EndLine {
        get {
            return endLine;
        }
    }

    public int StartLine {
        set {
            startLine = value;
        }
    }
}
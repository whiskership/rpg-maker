using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSelect : MonoBehaviour {
    //public bool isSingleton;

    public static MenuSelect Instance { get; set; }
    private void Awake() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        menuHolder = transform.GetChild(0);
    }

    Transform menuHolder;
    void Start() {
        if (!CurrentText.text.Contains(">"))
            CurrentText.text = CurrentText.text.Insert(0, ">");
    }

    int currentSelection;
    int confirmedSelecton;

    bool keyPressed;

    void Update() {
        if (IsMenuOpen == true) {
            if (keyPressed == false) {
                if ((KeyboardAxis.y == 1 || KeyboardAxis.x == 1) && currentSelection - 1 >= 0) {
                    CurrentText.text = CurrentText.text.Remove(CurrentText.text.IndexOf(">"), 1);

                    currentSelection--;
                }
                else if ((KeyboardAxis.y == -1 || KeyboardAxis.x == -1) && currentSelection + 1 < GetActiveChildCount()) {
                    CurrentText.text = CurrentText.text.Remove(CurrentText.text.IndexOf(">"), 1);

                    currentSelection++;
                }
            }

            if (KeyboardAxis != Vector2.zero && keyPressed == false) {
                if (!CurrentText.text.Contains(">"))
                    CurrentText.text = CurrentText.text.Insert(0, ">");

                keyPressed = true;
            }
            else if (KeyboardAxis == Vector2.zero)
                keyPressed = false;

            //confirm selection
            if (Input.GetKeyDown(KeyCode.Z)) {
                confirmedSelecton = currentSelection;
                currentSelection = 0;
            }
        }
    }

    int GetActiveChildCount() {
        int childCount=0;
        for (int i = 0; i < menuHolder.childCount; i++) {
            if (menuHolder.GetChild(i).gameObject.activeSelf==false)
                return childCount;

            childCount++;
        }

        return childCount;
    }

    public void OpenMenu() {
        menuHolder.gameObject.SetActive(true);

        currentSelection = 0;

        if (!CurrentText.text.Contains(">"))
            CurrentText.text = CurrentText.text.Insert(0, ">");
    }

    public void ClearAndCloseAllOptions() {
        for (int i = 0; i < menuHolder.childCount; i++) {
            menuHolder.GetChild(i).GetComponent<Text>().text = "";
            menuHolder.GetChild(i).gameObject.SetActive(false);
        }

        menuHolder.gameObject.SetActive(false);
    }

    public void AddStringToMenuOption(string stringToAdd) {
        int lastActiveChild=0;
        for (int i = 0; i < menuHolder.childCount; i++) {
            if (menuHolder.GetChild(i).gameObject.activeSelf == false) {
                lastActiveChild = i;
                break;
            }
        }

        menuHolder.GetChild(lastActiveChild).gameObject.SetActive(true);
        menuHolder.GetChild(lastActiveChild).GetComponent<Text>().text=stringToAdd;
    }

    public bool IsMenuOpen {
        get {
            return menuHolder.gameObject.activeSelf;
        }
    }

    Text CurrentText {
        get {
            return menuHolder.GetChild(currentSelection).GetComponent<Text>();
        }
    }

    Vector2 KeyboardAxis {
        get {
            return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
    }

    public int ConfirmedSelection {
        get {
            return confirmedSelecton;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
    [SerializeField]
    protected Transform[] ableToInteractTo;
    [SerializeField]
    protected int interactableDistance = 16;
    bool isWithinInteractableDistance;

    protected Transform interactingWith;

    public bool IsWithinInteractableDistance { 
        get {
            for (int i = 0; i < ableToInteractTo.Length; i++) {
                if (Vector2.Distance(ableToInteractTo[i].position, transform.position) <= interactableDistance && ableToInteractTo[i].GetComponent<Movement>().IsMoving == false) {
                    interactingWith = ableToInteractTo[i].transform;
                    return true;
                }
            }

            return false;
        }
    }

    [SerializeField]
    bool notificationActive;
    public bool NotificationActive {
        get {
            return notificationActive;
        }
        set {
            notificationActive = value;
        }
    }

    [Header("Notifications")]
    [SerializeField]
    protected Transform notificationFab;

    void Start() {
        if (notificationActive == true) {
            CreateNotification(new Vector2(0, 16));
        }

        interactableDistance = 16;//PlayerInfo.Instance.GetComponent<KeyboardMovement>().gridCubeSize * 2;

        ableToInteractTo = new Transform[1];
        ableToInteractTo[0] = PlayerInfo.Instance.transform;
    }

    Transform activeNotification;
    public void CreateNotification(Vector2 notifPos) {
        if (activeNotification == null && notificationFab != null)
            activeNotification = Instantiate(notificationFab, transform) as Transform;

        activeNotification.localPosition = notifPos;
        notificationActive = true;
    }

    public void RemoveNotification() {
        activeNotification.gameObject.SetActive(false);
        activeNotification = null;

        notificationActive = false;
    }
}

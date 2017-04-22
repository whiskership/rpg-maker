using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchRoomTrigger1 : MonoBehaviour {
    Transform sibling;
    void Start() {
        sibling = transform.parent.GetChild(1);
    }

    public bool colliderEntered;

    [SerializeField]
    AudioClip enterSFX;
    void OnTriggerEnter2D(Collider2D coll) {
        if (colliderEntered == false) {
            sibling.GetComponent<SwitchRoomTrigger2>().colliderEntered = true;
            coll.GetComponent<Movement>().MoveTo(sibling.position);

            if (GetComponent<AudioSource>() != null) {
                GetComponent<AudioSource>().clip = enterSFX;
                GetComponent<AudioSource>().Play();
            }

        }
    }

    void OnTriggerExit2D(Collider2D coll) {
        colliderEntered = false;
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.parent.GetChild(1).position);
    }
}

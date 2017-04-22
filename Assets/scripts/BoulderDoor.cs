using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderDoor : MonoBehaviour {
    [SerializeField]
    boulder_behaviour[] boulders;

    [SerializeField]
    float minDistToReset = 128;

    Vector2[] bouldersOgPos;
    void Start() {
        bouldersOgPos = new Vector2[boulders.Length];

        for (int i = 0; i < boulders.Length; i++) {
            bouldersOgPos[i] = boulders[i].transform.position;
        }
    }

    [SerializeField]
    BoulderSwitch[] switches;
    void Update() {
        //resets boulders
        if (Input.GetKeyDown(KeyCode.Space)) {
            float distFromPlayer = Vector2.Distance(transform.position, PlayerInfo.Instance.transform.position);

            if (distFromPlayer < minDistToReset) {
                StopAllCoroutines();
                StartCoroutine(ResetBoulders());
            }
        }

        //check if switches have boulders on them instead of
        //checking if the boulder is on thedoor
        /*if (boulder_behaviour.IsBoulderPositionUpdated == true) {
            for (int i = 0; i < boulders.Length; i++) {

                if (boulders[i].transform.position != transform.position)
                    return;
            }

            gameObject.SetActive(false);
            GetComponent<BoulderSwitch>().enabled = false;
        }*/

        if (boulder_behaviour.IsBoulderPositionUpdated == true) {
            for (int i = 0; i < switches.Length; i++) {
                if (switches[i].IsSwitchActive() == false) {
                    return;
                }
            }
            gameObject.SetActive(false);
        }


    }

    IEnumerator ResetBoulders() {
        Vector2 playerOgPos = PlayerInfo.Instance.transform.position;
        PlayerInfo.Instance.GetComponent<KeyboardMovement>().FreezeAxisVector = true;
        PlayerInfo.Instance.GetComponent<BoxCollider2D>().enabled = false;

        PlayerInfo.Instance.transform.position = new Vector2(PlayerInfo.Instance.transform.position.x, PlayerInfo.Instance.transform.position.y + 64);

        for (int i = 0; i < boulders.Length; i++) {
            boulders[i].transform.position = new Vector2(bouldersOgPos[i].x, bouldersOgPos[i].y + 64);
        }

        bool reachedTarget = false;
        while (reachedTarget == false) {
            reachedTarget = true;

            for (int i = 0; i < boulders.Length; i++) {
                PlayerInfo.Instance.transform.position = Vector2.MoveTowards(PlayerInfo.Instance.transform.position, playerOgPos, 59 * Time.deltaTime);

                boulders[i].transform.position = Vector2.MoveTowards(boulders[i].transform.position, bouldersOgPos[i], 60 * Time.deltaTime);
                boulders[i].GetComponent<BoxCollider2D>().enabled = false;

                if ((Vector2)boulders[i].transform.position != bouldersOgPos[i])
                    reachedTarget = false;
            }

            for (int i = 0; i < boulders.Length; i++) {
                boulders[i].GetComponent<BoxCollider2D>().enabled = true;
            }

            yield return null;
        }

        PlayerInfo.Instance.GetComponent<KeyboardMovement>().FreezeAxisVector = false;
        PlayerInfo.Instance.GetComponent<BoxCollider2D>().enabled = true;
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = new Color(Gizmos.color.r, Gizmos.color.g, Gizmos.color.b, 0.5f);
        Gizmos.DrawSphere(transform.position, minDistToReset);
    }
}
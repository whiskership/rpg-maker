using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boulder_behaviour : MonoBehaviour {
    public bool pushable;
    void Update() {
        //movement
        float player_dist = Vector2.Distance(PlayerInfo.Instance.transform.position, transform.position);

        if (player_dist <= 16) {
            Vector2 playerDirectionToBoulder = -(PlayerInfo.Instance.transform.position - transform.position).normalized;

            pushable = true;

            if ( PlayerInfo.Instance.GetComponent<KeyboardMovement>().KeyAxisVector != Vector2.zero 
                && playerDirectionToBoulder == PlayerInfo.Instance.GetComponent<KeyboardMovement>().FacingDirection) {

                StopAllCoroutines();
                StartCoroutine(MoveTo((Vector2)transform.position + PlayerInfo.Instance.GetComponent<KeyboardMovement>().FacingDirection * 16));
            }
        }

        /*if (boulder_behaviour.IsBoulderPositionUpdated == true) {
            for (int i = 0; i < boulders.Length; i++) {

                if (boulders[i].transform.position != transform.position)
                    return;
            }

            door.gameObject.SetActive(false);
            GetComponent<BoulderSwitch>().enabled = false;
        }*/
    }

    static bool isBoulderPositionUpdated;
    public static bool IsBoulderPositionUpdated {
        get {
            return isBoulderPositionUpdated;
        }
    }

    IEnumerator MoveTo(Vector2 targetPos) {
        if (Physics2D.OverlapCircle(targetPos, 4) != null) {
            yield break;
        }

        PlayerInfo.Instance.GetComponent<KeyboardMovement>().FreezeAxisVector = true;

        while ((Vector2) transform.position != targetPos) {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, 80 * Time.deltaTime);
            yield return null;
        }

        PlayerInfo.Instance.GetComponent<KeyboardMovement>().FreezeAxisVector = false;

        isBoulderPositionUpdated = true;

        yield return null;

        isBoulderPositionUpdated = false;
    }
}

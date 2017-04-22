using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderSwitch : MonoBehaviour {
    boulder_behaviour[] allBoulders;
    private void Start() {
        allBoulders = FindObjectsOfType(typeof(boulder_behaviour)) as boulder_behaviour[];
    }

    public bool IsSwitchActive() {
        for (int i = 0; i < allBoulders.Length; i++) {

            if (allBoulders[i].transform.position == transform.position)
                return true;
        }

        return false;
    }
}

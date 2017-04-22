using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycodeSwitchActivateObject : MonoBehaviour {
    [SerializeField]
    string switchKey;
    void Update () {
		if (Input.GetKeyDown(switchKey)) {
            transform.GetChild(0).gameObject.SetActive(!transform.GetChild(0).gameObject.activeSelf);
        }
	}
}

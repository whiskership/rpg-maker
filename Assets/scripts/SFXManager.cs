using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour {
    static public SFXManager Instance { get; set; }

    void Awake () {
        if (Instance == null) Instance = this;
	}
}
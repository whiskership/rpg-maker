using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {
    public static PlayerInfo Instance { get; set; }
    void Awake() {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }
}

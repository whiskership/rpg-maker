using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scaling : MonoBehaviour {
    public static UI_Scaling Instance { get; set; }
    void Awake() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
}

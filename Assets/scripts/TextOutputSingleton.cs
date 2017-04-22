using UnityEngine;

public class TextOutputSingleton : MonoBehaviour {
    public static TextOutputSingleton Instance { get; set; }
    private void Awake() {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionSettings : MonoBehaviour {
    /*[SerializeField]
    Vector2 screenResolution;

    Vector2 highestResolution;*/
    /*void Awake () {
        Resolution[] resolutions = Screen.resolutions;
        foreach (Resolution res in resolutions) {
            if (res.height > highestResolution.y) {
                highestResolution = new Vector2(res.width, res.height);
            }
        }

        Debug.Log(Screen.currentResolution);
        Screen.SetResolution((int)screenResolution.x * Mathf.CeilToInt(Screen.currentResolution.width / screenResolution.x), (int)screenResolution.y * Mathf.CeilToInt(Screen.currentResolution.height / screenResolution.y), true);
        Debug.Log(Mathf.RoundToInt(Screen.currentResolution.width / screenResolution.x));
    }*/

    static public ResolutionSettings Instance { get; set; }

    void Awake() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        Screen.SetResolution(80 * resMultiplier, 64 * resMultiplier, false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.F4)) {
            Resize();

            UI_Scaling.Instance.GetComponent<CanvasScaler>().scaleFactor = resMultiplier / 5;
        }
    }

    void Resize() {
        if (resMultiplier + 5 < 25)
            resMultiplier += 5;
        else resMultiplier = 5;

        Screen.SetResolution(80 * resMultiplier, 64 * resMultiplier, false);
    }

    static int resMultiplier = 5;
    public static int ResMultiplier {
        get {
            return resMultiplier;
        }
    }
}

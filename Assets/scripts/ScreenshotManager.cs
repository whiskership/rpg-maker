﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotManager : MonoBehaviour {
    void Update() {
        if (Input.GetKeyDown(KeyCode.L)) {
            Application.CaptureScreenshot("screenshot.png");
        }
    }
}

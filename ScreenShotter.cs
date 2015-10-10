using UnityEngine;
using System.Collections;

public class ScreenShotter : MonoBehaviour {
    void Update() {
        if (Application.isEditor && Input.GetKeyDown(KeyCode.P)) {
            string name = "ScreenShot" + System.DateTime.Now.ToString("yyyyMMdd\\THHmmssfff") + ".png";
            Application.CaptureScreenshot(name);
            Debug.Log("Screenshot saved: " + name);
        }
    }
}

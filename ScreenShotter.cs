using UnityEngine;
using System.Collections;

public class ScreenShotter : MonoBehaviour {
    void Update() {
        if (Application.isEditor && Input.GetKeyDown(KeyCode.P)) {
            TakeScreenShot();
        }
    }

    public static void TakeScreenShot() {
        string name = "ScreenShot" + System.DateTime.Now.ToString("yyyyMMdd\\THHmmssfff") + ".png";
        ScreenCapture.CaptureScreenshot(name);
        Debug.Log("Screenshot saved: " + name);
    }
}

using UnityEngine;

public class ScreenShotter {

    /** 
     * Takes a screenshot and puts it in folderName or the root of the project.
     */ 
    public static void TakeScreenShot(string folderName = null) {
        string fileName = "ScreenShot" + System.DateTime.Now.ToString("yyyyMMdd\\THHmmssfff") + ".png";
        string path = (folderName.IsNullOrEmpty() ? "" : folderName + "/") + fileName;
        ScreenCapture.CaptureScreenshot(path);
        Debug.Log("Screenshot saved: " + path);
    }
}

// Separate MonoBehaviour so that the MonoBehaviour methods don't clutter up autocomplete for the ScreenShotter class.
public class ScreenShotterMonoBehaviour : MonoBehaviour {
    void Update() {
        if (Application.isEditor && Input.GetKeyDown(KeyCode.P)) {
            ScreenShotter.TakeScreenShot("Screenshots");
        }
    }
}

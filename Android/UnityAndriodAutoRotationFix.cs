using UnityEngine;

public class UnityAndriodAutoRotationFix : MonoBehaviour {
    private static bool IsDeviceAutoRotationOn;
    private static bool GotRotationSetting;

    private static bool IsProjectSettingsSet;
    private static bool ProjectAutorotateToPortrait;
    private static bool ProjectAutorotateToPortraitUpsideDown;
    private static bool ProjectAutorotateToLandscapeLeft;
    private static bool ProjectAutorotateToLandscapeRight;


    private void Awake() {
        if (Application.platform != RuntimePlatform.Android) {
            return;
        }

        // It's not possible to check Screen.orientation for ScreenOrientation.AutoRotation, because it tracks current rotation.
        // Could possibly check PlayerSettings.defaultInterfaceOrientation, but this requires UnityEditor, so not at runtime.
        // So ScreenOrientation.AutoRotation is assumed to be active if this class is used.

        // Prevent overwriting previously saved project settings with Device settings.
        if (!IsProjectSettingsSet) {
            IsProjectSettingsSet = true;

            ProjectAutorotateToPortrait = Screen.autorotateToPortrait;
            ProjectAutorotateToPortraitUpsideDown = Screen.autorotateToPortraitUpsideDown;
            ProjectAutorotateToLandscapeLeft = Screen.autorotateToLandscapeLeft;
            ProjectAutorotateToLandscapeRight = Screen.autorotateToLandscapeRight;
        }

        UpdateAutoRotation();
    }


    private void OnApplicationFocus(bool hasFocus) {
        if (hasFocus && Application.platform == RuntimePlatform.Android) {
            UpdateAutoRotation();
        }
    }

    private void UpdateAutoRotation() {
        GetDeviceAutoRotation();
        if (GotRotationSetting) {
            SetUnityAutoRotation();
        }
    }
    
    private static void GetDeviceAutoRotation() {
        if (Application.platform != RuntimePlatform.Android) {
            return;
        }

        using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
            using (AndroidJavaObject activityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity")) {
                using (AndroidJavaClass settings = new AndroidJavaClass("android.provider.Settings$System")) {
                    if (settings != null) {
                        var rotationSetting = settings.CallStatic<int>("getInt", activityContext.Call<AndroidJavaObject>("getContentResolver"), "accelerometer_rotation");
                        GotRotationSetting = true;
                        IsDeviceAutoRotationOn = rotationSetting == 1;
                    }
                }
            }
        }
    }

    private static void SetUnityAutoRotation() {
        Screen.autorotateToPortrait = IsDeviceAutoRotationOn && ProjectAutorotateToPortrait;
        Screen.autorotateToPortraitUpsideDown = IsDeviceAutoRotationOn && ProjectAutorotateToPortraitUpsideDown;
        Screen.autorotateToLandscapeLeft = IsDeviceAutoRotationOn && ProjectAutorotateToLandscapeLeft;
        Screen.autorotateToLandscapeRight = IsDeviceAutoRotationOn && ProjectAutorotateToLandscapeRight;
        Screen.orientation = ScreenOrientation.AutoRotation;
    }
}

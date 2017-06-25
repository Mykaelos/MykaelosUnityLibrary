using UnityEngine;

/**
 * ### Android Setup Required
 * You need to include the following permission in your AndroidManifest.xml
 * 
 * ```
 * <uses-permission android:name="android.permission.VIBRATE" />
 * ```
  * 
 * You can either copy the Unity built manifest from Temp/StatingArea/AndroidManifest.xml into your Plugins/Android/ folder, and add the VIBRATE permission, or you can trick Unity to add it for you by including Handheld.Vibrate() in your code somewhere. If you don't want Unity to actually run Handheld.Vibrate(), you can block it with an if statement that never runs:
 * 
 * ```
 * bool alwaysFalse = false;
 * if (alwaysFalse) {
 *     Handheld.Vibrate();
 * }
 * ```
 * 
 * **Note:** `if (false)` wont work because Unity will see it as unreachable code, and not include it in the project, and you won't get the permission automatically added for you.
 * 
 * You could also try to merge in the permission yourself with a PostProcessBuild Step for Android [like this one](https://github.com/gree/unity-webview/blob/master/plugins/Android/Editor/UnityWebViewPostprocessBuild.cs).
 */
public class AndroidVibrateManager {
    private static AndroidJavaObject VibratorInstance = null;


    public static void Initialize() {
        if (Application.platform != RuntimePlatform.Android) {
            return;
        }

        using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
            using (AndroidJavaObject activityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity")) {
                using (AndroidJavaClass pluginClass = new AndroidJavaClass("android.os.Vibrator")) {
                    if (pluginClass != null) {
                        VibratorInstance = activityContext.Call<AndroidJavaObject>("getSystemService", activityContext.GetStatic<AndroidJavaObject>("VIBRATOR_SERVICE"));
                    }
                }
            }
        }

        if (VibratorInstance == null) {
            Debug.Log("UnityAndroidVibratorBridge: Failed to setup the UnityAndroidVibrator.");
        }
    }

    public static bool IsInitialized() {
        return VibratorInstance != null;
    }

    public static bool HasVibrator() {
        return IsInitialized() && VibratorInstance.Call<bool>("hasVibrator");
    }

    public static void Vibrate(long duration) {
        if (IsInitialized()) {
            VibratorInstance.Call("vibrate", duration);
        }
    }

    public static void Vibrate(long[] pattern, int repeat) {
        if (IsInitialized()) {
            VibratorInstance.Call("vibrate", pattern, repeat);
        }
    }

    public static void Cancel() {
        if (IsInitialized()) {
            VibratorInstance.Call("cancel");
        }
    }
}

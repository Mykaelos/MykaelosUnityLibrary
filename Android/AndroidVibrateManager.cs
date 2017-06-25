using UnityEngine;

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

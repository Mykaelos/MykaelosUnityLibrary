using System;
using System.Collections;
using UnityEngine;

//http://forum.unity3d.com/threads/device-screen-rotation-event.118638/#post-2339346 
public class DeviceOrientationNotifier : MonoBehaviour {
    static DeviceOrientationNotifier Self;
    public static float CheckDelay = 0.5f;

    static Vector2 resolution;
    static DeviceOrientation orientation;
    static bool isAlive = true;

    
    public static void Initialize() {
        if (Self != null) {
            return;
        }

        var gameObject = new GameObject("DeviceOrientationNotifier");
        Self = gameObject.AddComponent<DeviceOrientationNotifier>();
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        StartCoroutine(CheckForChange());
    }

    IEnumerator CheckForChange() {
        resolution = new Vector2(Screen.width, Screen.height);
        orientation = Input.deviceOrientation;

        while (isAlive) {
            // Check for a Resolution Change
            if (resolution.x != Screen.width || resolution.y != Screen.height) {
                var previousResolution = resolution;
                resolution = new Vector2(Screen.width, Screen.height);

                Messenger.Fire("DeviceOrientationNotifier.ResolutionChange", new object[] { resolution, previousResolution });
            }

            // Check for an Orientation Change
            switch (Input.deviceOrientation) {
                case DeviceOrientation.Unknown:// Ignore
                case DeviceOrientation.FaceUp:// Ignore
                case DeviceOrientation.FaceDown:// Ignore
                    break;
                default:
                    if (orientation != Input.deviceOrientation) {
                        var previousOrientation = orientation;
                        orientation = Input.deviceOrientation;

                        Messenger.Fire("DeviceOrientationNotifier.OrientationChange", new object[] { orientation, previousOrientation });
                    }
                    break;
            }

            yield return new WaitForSeconds(CheckDelay);
        }
    }

    void OnDestroy() {
        isAlive = false;
        Self = null;
    }
}
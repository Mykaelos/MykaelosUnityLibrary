using System;
using UnityEngine;

/**
 * Fires the ResolutionChanged if the screen width or height changes.
 *
 * Example on how to use:
 * GetComponent<CameraResolutionChange>().ResolutionChanged += OnResolutionChanged;
 * 
 * private void OnResolutionChanged() {
 *      // Do something with the new resolution, like update the camera position/distance. 
 * }
 */
public class CameraResolutionChange : MonoBehaviour {
    private Vector2 Resolution;


    private void Awake() {
        Resolution = new Vector2(Screen.width, Screen.height);
    }

    private void Update() {
        if (Resolution.x != Screen.width || Resolution.y != Screen.height) {
            OnResolutionChanged();

            Resolution.x = Screen.width;
            Resolution.y = Screen.height;
        }
    }

    #region ResolutionChanged Event
    public event Action ResolutionChanged;

    protected void OnResolutionChanged() {
        ResolutionChanged?.Invoke();
    }
    #endregion ResolutionChanged Event
}

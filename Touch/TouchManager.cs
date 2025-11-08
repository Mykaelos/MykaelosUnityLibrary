using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

// This class pretends that the Mouse and Screen touches are one and the same.
// This allows easy development and deployment to either a touch device or a mouse-using device.
public class TouchManager {
    private static Dictionary<int, TouchPoint> Touches = new Dictionary<int, TouchPoint>();
    private static Dictionary<int, TouchPoint> LastTouches = new Dictionary<int, TouchPoint>();
    private static float LastCheck;

    private const int MOUSE_LEFT_CLICK_ID = 100; // StartingButtonId = 100...
    private const int MOUSE_RIGHT_CLICK_ID = 101;
    private const int MOUSE_MIDDLE_CLICK_ID = 102;

    protected static List<TouchInputType> TouchInputTypes = new List<TouchInputType>();



    public static Dictionary<int, TouchPoint> GetTouches() {
        if (TouchInputTypes.IsNullOrEmpty()) {
            if (IsTouch()) {
                TouchInputTypes.Add(new TouchTouchType());
            }
            if (IsMouse()) {
                TouchInputTypes.Add(new MouseTouchType());
            }
        }

        if (AreTouchesStale()) { // Prevents recalculations during the same frame.
            UpdateTouches();
        }

        return Touches;
    }

    public static bool IsMouse() {
        return Input.mousePresent;
    }

    public static bool IsTouch() {
        return Input.touchSupported;
    }

    public static bool HasTouch() {
        return GetTouches().IsNotEmpty();
    }

    static void UpdateTouches() {
        bool isThereATouch = false;
        foreach (var touchInputType in TouchInputTypes) {
            if (touchInputType.IsThereATouch()) {
                isThereATouch = true;
            }
        }

        if (!isThereATouch) {
            if (Touches.Count > 0) {
                Touches.Clear();
            }

            return;
        }

        // Copy over all of the touches to LastTouches, so we can clear and refill Touches.
        LastTouches.Clear();
        foreach (var touchPoint in Touches) {
            LastTouches.Add(touchPoint.Key, touchPoint.Value);
        }
        Touches.Clear();

        LastCheck = Time.time;

        foreach (var touchInputType in TouchInputTypes) {
            touchInputType.UpdateTouches(Touches, LastTouches);
        }
    }

    static bool AreTouchesStale() {
        return Time.time > LastCheck;
    }

    //private const int NOT_DRAGGING = -1;
    //private static int DraggingTouchId = NOT_DRAGGING;
    //public static Vector2 StartDraggingScreenPoint;

    public static TouchDragEvent TouchDragEvent = null;


    // TODO - Add dragging for touch.
    // TODO - Consolidate dragging into Mouse and Touch classes.
    public static TouchDragEvent UpdateScreenDrag() {
        var touches = TouchManager.GetTouches();

        //if (IsTouch() && touches.Count == 2) {
        //    if (DraggingTouchId == NOT_DRAGGING) {
        //        if (IsTouch()) {
        //            var touch = touches.First();
        //            if (touch.IsConsumed() || EventSystem.current.IsPointerOverGameObject()) {
        //                return false;
        //            }

        //            StartDraggingScreenPoint = touch.ScreenPosition;
        //            DraggingTouchId = touch.ID;
        //            //StartingCameraPoint = transform.position;

        //            return true;
        //        }
        //        else if (IsMouse()) {
        //            var touch = touches.Get(MOUSE_RIGHT_CLICK_ID);
        //            if (touch.IsConsumed() || EventSystem.current.IsPointerOverGameObject()) {
        //                return false;
        //            }

        //            StartDraggingScreenPoint = touch.ScreenPosition;
        //            DraggingTouchId = touch.ID;
        //            //StartingCameraPoint = transform.position;

        //            return true;
        //        }
        //    }
        //    else if (DraggingTouchId != NOT_DRAGGING && touches.Get(DraggingTouchId) != null) {
        //        var touch = touches.Get(DraggingTouchId);

        //        if (!touch.IsConsumed() && touch.Phase == TouchPhase.Moved) {

        //            UpdateCameraLocation(ClampedViewRect.Clamp(StartingCameraPoint - (Vector3)(touch.ScreenPosition - StartDraggingScreenPoint) * Camera.UnitsPerPixel()));
        //            //Debug.Log(touch.Velocity);

        //            return touch.ScreenPosition;
        //        }
        //    }
        //}
        //else {
        //    DraggingTouchId = NOT_DRAGGING;
        //}


        if (IsMouse() && touches.Get(MOUSE_RIGHT_CLICK_ID) != null) {
            if (TouchDragEvent == null) {
                var touch = touches.Get(MOUSE_RIGHT_CLICK_ID);
                if (touch.IsConsumed()) {//|| EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) {
                    return null;
                }

                TouchDragEvent = new TouchDragEvent() {
                    StartDraggingScreenPoint = touch.ScreenPosition,
                    CurrentDraggingScreenPoint = touch.ScreenPosition,
                    DraggingTouchIds = new List<int> { touch.ID },
                    Phase = TouchPhase.Began
                };

                //StartingCameraPoint = transform.position;

                return TouchDragEvent;
            }
            else if (TouchDragEvent != null && TouchDragEvent.StillDragging(touches)) {
                var touch = TouchDragEvent.GetPrimaryTouch(touches);
                TouchDragEvent.Phase = touch.Phase;

                if (!touch.IsConsumed() && touch.Phase == TouchPhase.Moved) {

                    //UpdateCameraLocation(ClampedViewRect.Clamp(StartingCameraPoint - (Vector3)(touch.ScreenPosition - StartDraggingScreenPoint) * Camera.UnitsPerPixel()));
                    //Debug.Log(touch.Velocity);

                    TouchDragEvent.CurrentDraggingScreenPoint = touch.ScreenPosition;
                    return TouchDragEvent;
                }
            }
        }
        else {
            TouchDragEvent = null;
        }

        return TouchDragEvent;
    }
}

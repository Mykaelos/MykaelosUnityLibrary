using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchManager {
    private static Dictionary<int, TouchPoint> Touches = new Dictionary<int, TouchPoint>();
    private static Dictionary<int, TouchPoint> LastTouches = new Dictionary<int, TouchPoint>();
    private static float LastCheck;

    private const int MOUSE_ID = 1;


    public static Dictionary<int, TouchPoint> GetTouches() {
        if (AreTouchesStale()) { // Prevents recalculations during the same frame.
            UpdateTouches();
        }

        return Touches;
    }

    static void UpdateTouches() {
        if (!IsThereATouch()) {
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

        if (Input.touchCount > 0) {
            for (int i = 0; i < Input.touchCount; i++) {
                Touch touch = Input.GetTouch(i);
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(touch.position);

                UpdateTouch(touch.fingerId, worldPosition, touch.phase);
            }
        }
        else if (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0)) {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TouchPhase phase = Input.GetMouseButtonDown(0) ? TouchPhase.Began : Input.GetMouseButtonUp(0) ? TouchPhase.Ended : TouchPhase.Moved;

            UpdateTouch(MOUSE_ID, worldPosition, phase);
        }
    }

    static bool IsThereATouch() {
        return Input.touchCount > 0 || Input.GetMouseButton(0) || Input.GetMouseButtonUp(0);
    }

    static bool AreTouchesStale() {
        return Time.time > LastCheck;
    }

    static void UpdateTouch(int id, Vector2 worldPosition, TouchPhase phase) {
        var touchPoint = LastTouches.Get(id);
        if (touchPoint == null) {
            touchPoint = new TouchPoint {
                ID = id,
                TouchStartTime = Time.time,
                Position = worldPosition, // So touch has an initial position for velocity to work
                StartPosition = worldPosition
            };
        }

        touchPoint.LastPosition = touchPoint.Position;
        touchPoint.Position = worldPosition;
        touchPoint.Phase = phase;

        Touches.Add(id, touchPoint);
    }
}

public class TouchPoint {
    public int ID;
    public Vector2 Position;
    public TouchPhase Phase;

    public float TouchStartTime;
    public Vector2 StartPosition;
    public Vector2 LastPosition;


    public float Duration {
        get {
            return Time.time - TouchStartTime;
        }
    }

    public Vector2 Velocity {
        get {
            return Position - LastPosition;
        }
    }

    public bool IsContinuous {
        get {
            return Phase.Is(TouchPhase.Moved) || Phase.Is(TouchPhase.Stationary);
        }
    }

    public override string ToString() {
        return string.Format("{0}: ({1},{2}) {3}", ID, Position.x, Position.y, Phase);
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchManager {
    private static List<TouchPoint> _Touches = new List<TouchPoint>();
    private static Dictionary<int, TouchPoint> _TouchIDs = new Dictionary<int, TouchPoint>();

    private static float LastCheck;

    public static List<TouchPoint> UpdateTouches() {
        _Touches.Clear();
        _TouchIDs.Clear();
        LastCheck = Time.time;

        if (Input.touchCount > 0) {
            for (int i = 0; i < Input.touchCount; i++) {
                Touch touch = Input.GetTouch(i);
                Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);
                var touchPoint = new TouchPoint {
                    ID = touch.fingerId,
                    Position = pos,
                    Phase = touch.phase
                };

                _Touches.Add(touchPoint);
                _TouchIDs.Add(touchPoint.ID, touchPoint);
            }
        }
        else if (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0)) {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TouchPhase phase = Input.GetMouseButtonDown(0) ? TouchPhase.Began : Input.GetMouseButtonUp(0) ? TouchPhase.Ended : TouchPhase.Moved;
            var touchPoint = new TouchPoint {
                ID = 1,
                Position = pos,
                Phase = phase
            };

            _Touches.Add(touchPoint);
            _TouchIDs.Add(touchPoint.ID, touchPoint);
        }
        
        return _Touches;
    }

    static bool AreTouchesStale() {
        return Time.time > LastCheck;
    }

    public static List<TouchPoint> GetTouches() {
        if(AreTouchesStale()) {
            UpdateTouches();
        }

        return _Touches;
    }

    public static Dictionary<int, TouchPoint> GetTouchIDs() {
        if (AreTouchesStale()) {
            UpdateTouches();
        }

        return _TouchIDs;
    }
}

public class TouchPoint {
    public int ID;
    public Vector2 Position;
    public TouchPhase Phase;
}

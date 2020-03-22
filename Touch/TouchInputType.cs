using System.Collections.Generic;
using UnityEngine;

public abstract class TouchInputType {


    public abstract bool IsThereATouch();

    public abstract void UpdateTouches(Dictionary<int, TouchPoint> Touches, Dictionary<int, TouchPoint> LastTouches);

    public void UpdateTouchInTouchLists(Dictionary<int, TouchPoint> touches, Dictionary<int, TouchPoint> lastTouches, int id, Vector2 worldPosition, Vector2 screenPosition, TouchPhase phase) {
        var touchPoint = lastTouches.Get(id);
        if (touchPoint == null) {
            touchPoint = new TouchPoint {
                ID = id,
                TouchStartTime = Time.time,

                Position = worldPosition,
                ScreenPosition = screenPosition,

                StartPosition = worldPosition,
                StartScreenPosition = screenPosition
            };
        }

        touchPoint.LastPosition = touchPoint.Position;
        touchPoint.Position = worldPosition;

        touchPoint.LastScreenPosition = touchPoint.ScreenPosition;
        touchPoint.ScreenPosition = screenPosition;

        touchPoint.Phase = phase;

        touches.Add(id, touchPoint);
    }
}

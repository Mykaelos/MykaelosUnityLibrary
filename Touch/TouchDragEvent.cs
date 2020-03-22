using System.Collections.Generic;
using UnityEngine;

public class TouchDragEvent {
    public List<int> DraggingTouchIds = new List<int>();
    public Vector2 StartDraggingScreenPoint;
    public Vector2 CurrentDraggingScreenPoint;
    public TouchPhase Phase;


    public bool StillDragging(Dictionary<int, TouchPoint> Touches) {
        foreach (var draggingTouchId in DraggingTouchIds) {
            if (!Touches.ContainsKey(draggingTouchId)) {
                return false;
            }
        }
        return true;
    }

    public TouchPoint GetPrimaryTouch(Dictionary<int, TouchPoint> Touches) {
        return Touches.Get(DraggingTouchIds[0]);
    }
}

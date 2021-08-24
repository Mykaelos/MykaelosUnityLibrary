using System.Collections.Generic;
using UnityEngine;

public class MouseTouchType : TouchInputType {
    private int StartingButtonId = 100; // Starting at 100 because that should be more than enough room for real touches.


    public override bool IsThereATouch() {
        return Input.GetMouseButton(0) || Input.GetMouseButtonUp(0) || // Left Click
            Input.GetMouseButton(1) || Input.GetMouseButtonUp(1) || // Right Click
            Input.GetMouseButton(2) || Input.GetMouseButtonUp(2); // Middle Click
    }

    public override void UpdateTouches(Dictionary<int, TouchPoint> Touches, Dictionary<int, TouchPoint> LastTouches) {
        for (int i = 0; i < 3; i++) {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int mouseButtonIndex = i;
            int mouseButtonId = i + StartingButtonId;
            if (Input.GetMouseButton(mouseButtonIndex) || Input.GetMouseButtonUp(mouseButtonIndex)) {
                bool isStationary = false;
                var lastTouchPoint = LastTouches.Get(mouseButtonId);
                if (lastTouchPoint != null) {
                    isStationary = lastTouchPoint.ScreenPosition == (Vector2)Input.mousePosition;
                }

                TouchPhase phase = Input.GetMouseButtonDown(mouseButtonIndex) ? TouchPhase.Began : Input.GetMouseButtonUp(mouseButtonIndex) ? TouchPhase.Ended : isStationary ? TouchPhase.Stationary : TouchPhase.Moved;

                UpdateTouchInTouchLists(Touches, LastTouches, mouseButtonId, worldPosition, Input.mousePosition, phase);
            }
        }
    }
}

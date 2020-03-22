using System.Collections.Generic;
using UnityEngine;

public class TouchTouchType : TouchInputType {


    public override bool IsThereATouch() {
        return Input.touchCount > 0;
    }

    public override void UpdateTouches(Dictionary<int, TouchPoint> Touches, Dictionary<int, TouchPoint> LastTouches) {
        for (int i = 0; i < Input.touchCount; i++) {
            Touch touch = Input.GetTouch(i);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(touch.position);

            UpdateTouchInTouchLists(Touches, LastTouches, touch.fingerId, worldPosition, touch.position, touch.phase);
        }
    }
}

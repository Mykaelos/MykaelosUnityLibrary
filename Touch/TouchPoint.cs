using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchPoint {
    public int ID;
    public TouchPhase Phase;

    public Vector2 StartPosition;
    public Vector2 LastPosition;
    public Vector2 Position;

    public Vector2 StartScreenPosition;
    public Vector2 LastScreenPosition;
    public Vector2 ScreenPosition;

    public float TouchStartTime;
    private bool Consumed = false;


    public void Consume() {
        Consumed = true;
    }

    public bool IsConsumed() {
        return Consumed;
    }

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

    public Vector2 ScreenVelocity {
        get {
            return ScreenPosition - LastScreenPosition;
        }
    }

    public bool IsContinuous {
        get {
            return Phase.Is(TouchPhase.Moved) || Phase.Is(TouchPhase.Stationary);
        }
    }

    // Requires EventSystem in the scene and a GraphicsRaycaster on the Canvas.
    public bool IsTouchingUI {
        get {
            return IsTouchingUICalculate();
        }
    }

    #region TouchingUI
    private bool AlreadyTouchingUICalcualted = false;
    private bool IsTouchingUICalcualted = false;

    // Borrowed http://answers.unity.com/answers/1133912/view.html with some changes.
    private bool IsTouchingUICalculate() {
        if (AlreadyTouchingUICalcualted) {
            return IsTouchingUICalcualted;
        }

        if (EventSystem.current == null) {
            Debug.LogWarning("IsTouchingUI: EventSystem MISSING! Cannot calculate if touching UI.");
            return false;
        }

        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = ScreenPosition;

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        IsTouchingUICalcualted = results.IsNotEmpty();
        AlreadyTouchingUICalcualted = true;

        return IsTouchingUICalcualted;
    }
    #endregion

    public override string ToString() {
        return string.Format("{0}: ({1},{2}) {3}", ID, Position.x, Position.y, Phase);
    }
}

using UnityEngine;

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

    public override string ToString() {
        return string.Format("{0}: ({1},{2}) {3}", ID, Position.x, Position.y, Phase);
    }
}

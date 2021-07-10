using UnityEngine;

public class Timer {
    public float Last = 0;
    public float Delay = 1; // Seconds
    public bool FireOnce = false;
    public bool HasFired = false;

    // Pass in Time.time for startingTime if not firing immediately.
    // Time.time can't be used as an inline default because it isn't a compile time constant, and Timer may be initialized when Time.time isn't available.
    public Timer(float delay = 1, bool fireOnce = false, bool fireImmediately = false, float startingTime = 0) {
        Delay = delay;
        FireOnce = fireOnce;

        Last = fireImmediately ? startingTime - Delay - 0.1f: startingTime;
    }

    public bool Check() {
        return Check(Delay);
    }

    public bool Check(float delay) {
        if (FireOnce && HasFired) {
            return false;
        }

        if (Last + delay < Time.time) {
            HasFired = true;
            return true;
        }

        return false;
    }

    public void Reset() {
        Last = Time.time;
    }

    public void ResetFireOnce() {
        HasFired = false;
    }

    public void FireNextImmediately() {
        Last = Time.time - Delay - 0.1f;
    }

    public float DurationUntilNext(float delay = 0) {
        delay = delay == 0 ? Delay : delay;
        return Mathf.Max((Last + delay) - Time.time, 0);
    }

    public void SetTimeRemaining(float remaining) {
        Last = remaining - Delay + Time.time;
    }
}

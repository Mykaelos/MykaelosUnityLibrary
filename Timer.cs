using UnityEngine;
using System.Collections;

public class Timer {
    public float Last = 0;
    public float Delay = 1;//seconds
    public bool FireOnce = false;
    public bool HasFired = false;


    public Timer(float delay = 1, bool fireOnce = false, bool fireImmediately = false) {
        Delay = delay;
        FireOnce = fireOnce;

        Last = fireImmediately ? Time.time - Delay - 0.1f: Time.time;
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
}

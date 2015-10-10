using UnityEngine;
using System.Collections;

public class Timer {
    public float Last = 0;
    public float Delay = 1;//seconds
    public bool FireOnce = false;
    public bool HasFired = false;


    public Timer(float delay = 1, bool fireOnce = false) {
        Delay = delay;
        FireOnce = fireOnce;
        Reset();
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
}

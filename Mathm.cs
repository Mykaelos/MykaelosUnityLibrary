using UnityEngine;
using System.Collections;

public class Mathm {
    public static int MinimumClamped(int value, int minimum) {
        return Mathf.Max(value, minimum);
    }

    public static float MinimumClamped(float value, float minimum) {
        return Mathf.Max(value, minimum);
    }

    public static int MaximumClamped(int value, int maximum) {
        return Mathf.Min(value, maximum);
    }

    public static float MaximumClamped(float value, float maximum) {
        return Mathf.Min(value, maximum);
    }
}

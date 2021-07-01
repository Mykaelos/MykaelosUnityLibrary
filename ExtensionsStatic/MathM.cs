using UnityEngine;

public class MathM {
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

    // Convenience Clamp methods so I don't have to remember to use Mathf, instead of MathM.
    public static int Clamp(int value, int minimum, int maximum) {
        return Mathf.Clamp(value, minimum, maximum);
    }

    public static float Clamp(float value, float minimum, float maximum) {
        return Mathf.Clamp(value, minimum, maximum);
    }
}

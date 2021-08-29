using UnityEngine;
using System.Collections;

public class LerpHelper {

    /* Returns a curve that starts rising quickly, then slows down as it approaches 1.
     * High velocity to one with deceleration.
     * percentProgress: Lerp between 0 and 1
     * steepness: How quickly the line rises initially. Larger values make it almost a right angle. Must be larger than 0.
     * - 2 seems to be a great value.
     * - 0.1 is almost a straight line.
     */
    public static float CurveToOneFastSlow(float percentProgress, float steepness) {
        steepness = Mathf.Max(0.001f, steepness); //prevents 0 steepness
        float percent = Mathf.Clamp01(percentProgress);
        float curvePercent = (-1f / (steepness * percent + 1) + 1) * (1f + 1f / steepness);

        return curvePercent;
    }

    /* Reverse of CurveToOneFastSlow
     * Low velocity to zero with acceleration.
     * percentProgress: Lerp between 0 and 1
     * steepness: How quickly the line rises initially. Larger values make it almost a right angle. Must be larger than 0.
     */
    public static float CurveToZeroSlowFast(float percentProgress, float steepness) {
        return CurveToOneFastSlow(Reverse(percentProgress), steepness);
    }


    /* Reverses the progress between 0 to 1.
     */
    public static float Reverse(float percentProgress) {
        return Mathf.Clamp01(1f - percentProgress);
    }


    /* Accelerates at beginning, decelerates at the end. 
     */
    public static float SmoothStep(float x) {
        return x * x * (3 - 2 * x);
    }

    public static float SmoothStep2(float x) {
        return SmoothStep(SmoothStep(x));
    }
}

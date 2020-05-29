using UnityEngine;

public class RandomM {
    public static bool Bool() {
        return Random.value > 0.5f;
    }

    // Returns true if Random chance succeeds between 0 and 1.
    // PercentChance is 0-1
    public static bool Chance(float percentChanceZeroToOne) {
        return Random.value < Mathf.Clamp01(percentChanceZeroToOne);
    }
}

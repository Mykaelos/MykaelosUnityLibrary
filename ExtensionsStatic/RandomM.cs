using System.Collections.Generic;
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

    public static List<int> CreateRandomListFromSeed(int seed, int length) {
        Random.InitState(seed);
        List<int> randomNumbers = new List<int>();

        for (int i = 0; i < length; i++) {
            randomNumbers.Add(CreateRandomSeed());
        }

        return randomNumbers;
    }

    public static int CreateRandomSeed() {
        return Random.Range(0, int.MaxValue);
    }
}

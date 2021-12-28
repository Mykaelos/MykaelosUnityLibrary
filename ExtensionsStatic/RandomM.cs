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

    public static int Int(int minInclusive, int maxInclusive) {
        return Random.Range(minInclusive, maxInclusive + 1);
    }

    public static int IntExclusive(int minInclusive, int maxExclusive) {
        return Random.Range(minInclusive, maxExclusive);
    }

    public static float Float(float min, float max) {
        return Random.Range(min, max);
    }

    public static Vector3 Vector(Vector3 min, Vector3 max) {
        return new Vector3(Float(min.x, max.x), Float(min.y, max.y), Float(min.z, max.z));
    }

    public static Vector3 Scale(float min, float max) {
        return Vector3.one * Float(min, max);
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

/**
 * A simple class to temporarily save the Random State and reset it when done.
 */ 
public class RandomTemp {
    private Random.State PreviousState;


    public RandomTemp(int seed) {
        PreviousState = Random.state;

        Random.InitState(seed);
    }

    public void Reset() {
        Random.state = PreviousState;
    }
}

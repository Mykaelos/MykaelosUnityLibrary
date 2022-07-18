using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoundsExtension {

    // Fetches a random point within the Bounds.
    // Useful for choosing a random spawn point within an area.
    public static Vector3 RandomPoint(this Bounds bounds) {
        return new Vector3(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y), Random.Range(bounds.min.z, bounds.max.z));
    }
}

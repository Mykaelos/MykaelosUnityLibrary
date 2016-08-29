using UnityEngine;

public static class RectExtension {

    // Fetches a random point within the Rect.
    // Useful for choosing a random spawn point within an area.
    public static Vector2 RandomPoint(this Rect rect) {
        return new Vector2(Random.Range(rect.xMin, rect.xMax), Random.Range(rect.yMin, rect.yMax));
    }
}

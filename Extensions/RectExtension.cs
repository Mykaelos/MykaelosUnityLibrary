using UnityEngine;

public static class RectExtension {

    // Fetches a random point within the Rect.
    // Useful for choosing a random spawn point within an area.
    public static Vector2 RandomPoint(this Rect rect) {
        return new Vector2(Random.Range(rect.xMin, rect.xMax), Random.Range(rect.yMin, rect.yMax));
    }

    public static Rect Center(this Rect rect, Vector2 centerPoint, Vector2 size) {
        rect.position = centerPoint - size / 2f;
        rect.size = size;
        return rect;
    }

    public static Rect StretchToInclude(this Rect rect, Vector2 point) {
        if (rect.xMin > point.x) {
            rect.xMin = point.x;
        }

        if (rect.xMax < point.x) {
            rect.xMax = point.x;
        }

        if (rect.yMin > point.y) {
            rect.yMin = point.y;
        }

        if (rect.yMax < point.y) {
            rect.yMax = point.y;
        }

        return rect;
    }

    public static Vector3 Clamp(this Rect rect, Vector3 position) {
        if (position.x < rect.xMin) {
            position.x = rect.xMin;
        }
        if (position.x > rect.xMax) {
            position.x = rect.xMax;
        }

        if (position.y < rect.yMin) {
            position.y = rect.yMin;
        }
        if (position.y > rect.yMax) {
            position.y = rect.yMax;
        }
        return position;
    }
}

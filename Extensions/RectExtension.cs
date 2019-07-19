using System.Collections.Generic;
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

    public static Vector2 WrapRect(this Rect rect, Vector2 position) {
        float x = (position.x > rect.xMax) ? rect.xMin : (position.x < rect.xMin) ? rect.xMax : position.x;
        float y = (position.y > rect.yMax) ? rect.yMin : (position.y < rect.yMin) ? rect.yMax : position.y;

        return new Vector2(x, y);
    }

    public static List<Vector2> GetTopEdgeVector2s(this Rect rect) {
        List<Vector2> topEdgeVector2s = new List<Vector2>();
        for (int i = 0; i <= rect.width; i++) {
            topEdgeVector2s.Add(new Vector2(rect.x + i, rect.yMax));
        }

        return topEdgeVector2s;
    }

    public static List<Vector2> GetBottomEdgeVector2s(this Rect rect) {
        List<Vector2> bottomEdgeVector2s = new List<Vector2>();
        for (int i = 0; i <= rect.width; i++) {
            bottomEdgeVector2s.Add(new Vector2(rect.x + i, rect.y));
        }

        return bottomEdgeVector2s;
    }

    public static List<Vector2> GetLeftEdgeVector2s(this Rect rect) {
        List<Vector2> leftEdgeVector2s = new List<Vector2>();
        for (int i = 0; i <= rect.height; i++) {
            leftEdgeVector2s.Add(new Vector2(rect.x, rect.y + i));
        }

        return leftEdgeVector2s;
    }

    public static List<Vector2> GetRightEdgeVector2s(this Rect rect) {
        List<Vector2> rightEdgeVector2s = new List<Vector2>();
        for (int i = 0; i <= rect.height; i++) {
            rightEdgeVector2s.Add(new Vector2(rect.xMax, rect.y + i));
        }

        return rightEdgeVector2s;
    }

    public static List<Vector2> GetEdgeVector2s(this Rect rect) {
        List<Vector2> edgeVector2s = new List<Vector2>();

        edgeVector2s.AddRange(rect.GetTopEdgeVector2s());
        edgeVector2s.AddRange(rect.GetBottomEdgeVector2s());

        var leftEdge = rect.GetLeftEdgeVector2s();
        leftEdge.RemoveLast();
        leftEdge.RemoveFirst();
        edgeVector2s.AddRange(leftEdge);

        var rightEdge = rect.GetRightEdgeVector2s();
        rightEdge.RemoveLast();
        rightEdge.RemoveFirst();
        edgeVector2s.AddRange(rightEdge);

        return edgeVector2s;
    }

    public static List<Vector2> GetInsideVector2s(this Rect rect) {
        List<Vector2> insideVector2s = new List<Vector2>();
        for (int i = 1; i <= rect.width - 1; i++) {
            for (int j = 1; j <= rect.height - 1; j++) {
                insideVector2s.Add(new Vector2(rect.x + i, rect.y + j));
            }
        }

        return insideVector2s;
    }

    public static bool OverlapsIncludingEdge(this Rect rect, Rect otherRect) {
        return rect.xMax >= otherRect.xMin
            && rect.xMin <= otherRect.xMax
            && rect.yMax >= otherRect.yMin
            && rect.yMin <= otherRect.yMax;
    }
}

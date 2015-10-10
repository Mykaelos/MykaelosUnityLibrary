using UnityEngine;
using System.Collections;

public static class Vector2Extensions {
    public static Vector2 SetX(this Vector2 position, float x) {
        Vector2 temp = position;
        temp.x = x;
        position = temp;
        return position;
    }

    public static Vector2 SetY(this Vector2 position, float y) {
        Vector2 temp = position;
        temp.y = y;
        position = temp;
        return position;
    }
}
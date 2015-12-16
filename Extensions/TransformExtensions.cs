using UnityEngine;
using System.Collections;

public static class TransformExtensions {


    public static Transform SetX(this Transform transform, float x) {
        Vector3 temp = transform.position;
        temp.x = x;
        transform.position = temp;
        return transform;
    }

    public static Transform SetY(this Transform transform, float y) {
        Vector3 temp = transform.position;
        temp.y = y;
        transform.position = temp;
        return transform;
    }

    public static Transform SetZ(this Transform transform, float z) {
        Vector3 temp = transform.position;
        temp.z = z;
        transform.position = temp;
        return transform;
    }

    public static Transform AddX(this Transform transform, float x) {
        return transform.SetX(transform.position.x + x);
    }

    public static Transform AddY(this Transform transform, float y) {
        return transform.SetY(transform.position.y + y);
    }

    public static RectTransform RectTransform(this Transform transform) {
        return (RectTransform)transform;
    }

    public static Rect Rect(this Transform transform) {
        return ((RectTransform)transform).rect;
    }
}

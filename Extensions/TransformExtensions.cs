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

    public static Transform FindFirstChildByName(this Transform transform, string name) {
        foreach (Transform child in transform) {
            if (name.Equals(child.name)) {
                return child;
            }
        }

        foreach (Transform child in transform) {
            var returnedChild = child.FindFirstChildByName(name);
            if (returnedChild != null && name.Equals(returnedChild.name)) {
                return returnedChild;
            }
        }

        return null;
    }

    public static T FindFirstChildComponentByType<T>(this Transform transform) {
        var returnedComponent = transform.GetComponent<T>();

        if (returnedComponent != null) {
            return returnedComponent;
        }

        foreach (Transform child in transform) {
            var returnedChildComponent = child.FindFirstChildComponentByType<T>();
            if (returnedChildComponent != null) {
                return returnedChildComponent;
            }
        }

        return default(T);
    }
}

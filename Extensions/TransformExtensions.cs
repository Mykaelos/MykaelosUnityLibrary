﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    public static List<Transform> GetChildren(this Transform transform) {
        List<Transform> children = new List<Transform>();
        foreach (Transform child in transform) {
            children.Add(child);
        }
        return children;
    }

    public static void DestroyAllChildren(this Transform transform) {
        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
    }

    public static void RemoveAll(this Transform transform) { // Alias for DestroyAllChildren to match Lists.
        DestroyAllChildren(transform);
    }

    public static void SetActiveAllChildren(this Transform transform, bool setActive) {
        var children = transform.GetChildren();
        foreach (var child in children) {
            child.gameObject.SetActive(setActive);
        }
    }

    public static void SetActiveChild(this Transform transform, string childName, bool setActive) {
        var child = transform.FindFirstChildByName(childName);
        if (child != null) {
            child.gameObject.SetActive(setActive);
        }
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

    public static List<Transform> FindChildrenByName(this Transform transform, string name) {
        List<Transform> children = new List<Transform>();
        foreach (Transform child in transform) {
            if (name.Equals(child.name)) {
                children.Add(child);
            }
        }

        foreach (Transform child in transform) {
            var returnedChildren = child.FindChildrenByName(name);
            if (!returnedChildren.IsNullOrEmpty()) {
                children.AddRange(returnedChildren);
            }
        }

        return children;
    }

    public static Transform FindFirstChildThatContainsName(this Transform transform, string name) {
        foreach (Transform child in transform) {
            if (child.name.Contains(name)) {
                return child;
            }
        }

        foreach (Transform child in transform) {
            var returnedChild = child.FindFirstChildByName(name);
            if (returnedChild != null && returnedChild.name.Contains(name)) {
                return returnedChild;
            }
        }

        return null;
    }

    // Looks for a transform by name starting with siblings, then looking in the parents all the way up.
    // This has the obvious risk of searching every object at the top level, so use wisely.
    public static Transform FindSiblingOrParent(this Transform transform, string name) {
        if (transform.parent != null) {
            var parent = transform.parent;

            foreach (Transform child in parent) {
                if (name.Equals(child.name)) {
                    return child;
                }
            }

            return parent.FindSiblingOrParent(name);
        }

        return null;
    }

    // Looks for a transform by name starting with siblings, then looking in the parents all the way up.
    // This has the obvious risk of searching every object at the top level, so use wisely.
    public static Transform FindSiblingOrParentThatContainsName(this Transform transform, string name) {
        if (transform.parent != null) {
            var parent = transform.parent;

            foreach (Transform child in parent) {
                if (child.name.Contains(name)) {
                    return child;
                }
            }

            return parent.FindSiblingOrParentThatContainsName(name);
        }

        return null;
    }

    // Looks for a component starting with siblings, then looking in the parents all the way up.
    // This has the obvious risk of searching every object at the top level, so use wisely.
    public static T FindComponentInSiblingOrParent<T>(this Transform transform) {
        if (transform.parent != null) {
            var parent = transform.parent;

            foreach (Transform child in parent) {
                var component = child.GetComponent<T>();

                if (component != null) {
                    return component;
                }
            }

            return parent.FindComponentInSiblingOrParent<T>();
        }

        return default(T);
    }

    public static T FindRequiredComponentInSiblingOrParent<T>(this Transform transform) {
        var requiredComponent = transform.FindComponentInSiblingOrParent<T>();
        if (requiredComponent == null) {
            Debug.LogError(typeof(T).Name + " IS REQUIRED FOR " + transform.gameObject.name);
        }

        return requiredComponent;
    }

    public static Vector3 RandomPosition(this Transform transform) {
        return new Vector3(
            transform.position.x + Random.Range(transform.localScale.x / -2f, transform.localScale.x / 2f),
            transform.position.y + Random.Range(transform.localScale.y / -2f, transform.localScale.y / 2f),
            transform.position.z + Random.Range(transform.localScale.z / -2f, transform.localScale.z / 2f));
    }
}

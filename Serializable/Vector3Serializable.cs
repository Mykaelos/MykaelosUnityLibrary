using System;
using UnityEngine;

[Serializable]
public class Vector3Serializable {
    public float x;
    public float y;
    public float z;


    public Vector3Serializable() {
        x = 0;
        y = 0;
        z = 0;
    }

    public Vector3Serializable(float x, float y, float z) {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public static implicit operator Vector3(Vector3Serializable vs) {
        return new Vector3(vs.x, vs.y, vs.z);
    }

    public static implicit operator Vector3Serializable(Vector3 v) {
        return new Vector3Serializable(v.x, v.y, v.z);
    }

    public static implicit operator Vector2(Vector3Serializable vs) {
        return new Vector2(vs.x, vs.y);
    }

    public static implicit operator Vector3Serializable(Vector2 v) {
        return new Vector3Serializable(v.x, v.y, 0);
    }
}


public static class Vector3SerializableExtension {
    public static Vector2 ToVector2xy(this Vector3Serializable vs) {
        return new Vector2(vs.x, vs.y);
    }

    public static Vector2 ToVector2xz(this Vector3Serializable vs) {
        return new Vector2(vs.x, vs.z);
    }
}

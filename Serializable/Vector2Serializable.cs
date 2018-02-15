using System;
using UnityEngine;

[Serializable]
public class Vector2Serializable {
    public float x;
    public float y;

    public Vector2Serializable(float x, float y) {
        this.x = x;
        this.y = y;
    }

    public static implicit operator Vector2(Vector2Serializable vs) {
        return new Vector2(vs.x, vs.y);
    }

    public static implicit operator Vector2Serializable(Vector2 v) {
        return new Vector2Serializable(v.x, v.y);
    }

    public static implicit operator Vector3(Vector2Serializable vs) {
        return new Vector3(vs.x, vs.y, 0);
    }

    public static implicit operator Vector2Serializable(Vector3 v) {
        return new Vector2Serializable(v.x, v.y);
    }
}


public static class Vector2SerializableExtension {
    public static Vector3 ToVector3(this Vector2Serializable vs, float z = 0) {
        return new Vector3(vs.x, vs.y, z);
    }

    public static Vector3 ToVector3xz(this Vector2Serializable vs, float y = 0) {
        return new Vector3(vs.x, y, vs.y);
    }
}

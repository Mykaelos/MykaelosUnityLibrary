using UnityEngine;
using System.Collections;

public static class Vector3Extensions {
    public static Vector3 SetX(this Vector3 position, float x) {
        //Vector3 temp = position;
        //temp.x = x;
        //position = temp;
        position.x = x;
        return position;
    }

    public static Vector3 SetY(this Vector3 position, float y) {
        //Vector3 temp = position;
        //temp.y = y;
        //position = temp;
        position.y = y;
        return position;
    }

    public static Vector3 SetZ(this Vector3 position, float z) {
        //Vector3 temp = position;
        //temp.z = z;
        //position = temp;
        position.z = z;
        return position;
    }

    public static Vector3 RandomPosition(this Vector3 position) {
        return new Vector3(Random.Range(0f, position.x), Random.Range(0f, position.y), Random.Range(0f, position.z));
    }

    public static Vector2 Vector2XZ(this Vector3 position) {
        return new Vector2(position.x, position.z);
    }
}

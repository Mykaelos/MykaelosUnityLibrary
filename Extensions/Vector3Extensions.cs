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
}

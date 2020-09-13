using System;
using UnityEngine;

[Serializable]
public struct Point : IEquatable<Point> {
    public int X, Y;


    public Point(Point point) {
        X = point.X;
        Y = point.Y;
    }

    public Point(int x, int y) {
        X = x;
        Y = y;
    }

    public Point(Vector2 vector2, bool roundNearest = true) {
        X = roundNearest ? Mathf.RoundToInt(vector2.x) : (int)vector2.x;
        Y = roundNearest ? Mathf.RoundToInt(vector2.y) : (int)vector2.y;
    }

    public Point(Vector3 vector3, bool roundNearest = true) {
        X = roundNearest ? Mathf.RoundToInt(vector3.x) : (int)vector3.x;
        Y = roundNearest ? Mathf.RoundToInt(vector3.y) : (int)vector3.y;
    }

    public Vector2 Vector2() {
        return new Vector2(X, Y);
    }

    public Vector3 VectorXZ() {
        return new Vector3(X, 0, Y);
    }

    public Vector3 Vector3() {
        return new Vector3(X, Y, 0);
    }

    public Point Normalize() {
        X = Mathf.Clamp(X, -1, 1);
        Y = Mathf.Clamp(Y, -1, 1);

        return this;
    }

    public bool Equals(Point other) {
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object other) {
        if (other is Point) {
            return Equals((Point)other);
        }

        return false;
    }

    // http://stackoverflow.com/a/263416/1437653
    public override int GetHashCode() {
        unchecked { // Overflow is fine, just wrap
            int hash = 17;
            hash = hash * 23 + X.GetHashCode();
            hash = hash * 23 + Y.GetHashCode();
            return hash;
        }
    }


    public static implicit operator Vector2(Point point) {
        return point.Vector2();
    }

    // Implicit Vector3 conflicts with Rect.Contains() because it handles both Vector2 and Vector3:
    // The call is ambiguous between the following methods or properties: 'Rect.Contains(Vector2)' and 'Rect.Contains(Vector3)'
    // TODO Find a way around this. Use point.Vector3() for now.
    //public static implicit operator Vector3(Point point) {
    //    return point.Vector3();
    //}

    // 2020/9/13 BREAKING CHANGE - converting vector2 to point will now use Mathf.RoundToInt() by default to
    // solve minor float rounding issues in Vectors. It used to just cleave off the decimal with (int) casting,
    // but a vast majority of the time when converting from Vector to Point, it would be better if the Vector
    // "snapped" to the point. This originally came up because a Vector2 was reporting as (6.0, -1.0) via its
    // ToString(), but the value was actually (5.98..., -1.0) which caused the Point(Vector2) to convert to (5, -1).
    // Mathf.RoundToInt() fixes float rounding errors for this case.
    public static implicit operator Point(Vector2 vector2) {
        return new Point(vector2);
    }

    // 2020/9/13 BREAKING CHANGE - converting vector2 to point will now use Mathf.RoundToInt() by default so solve minor float rounding issues in Vectors.
    public static implicit operator Point(Vector3 vector3) {
        return new Point(vector3);
    }

    public static bool operator ==(Point term1, Point term2) {
        return term1.Equals(term2);
    }

    public static bool operator !=(Point term1, Point term2) {
        return !term1.Equals(term2);
    }

    public static Point operator +(Point term1, Point term2) {
        return new Point(term1.X + term2.X, term1.Y + term2.Y);
    }

    public static Point operator -(Point term1, Point term2) {
        return new Point(term1.X - term2.X, term1.Y - term2.Y);
    }

    public static Point operator *(Point term1, int term2) {
        return new Point(term1.X * term2, term1.Y * term2);
    }

    public override string ToString() {
        return string.Format("({0}, {1})", X, Y);
    }
}

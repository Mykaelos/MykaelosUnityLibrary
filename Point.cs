using System;
using UnityEngine;

[Serializable]
public struct Point : System.IEquatable<Point> {
    public int X, Y;


    public Point(Point point) {
        X = point.X;
        Y = point.Y;
    }

    public Point(int x, int y) {
        X = x;
        Y = y;
    }

    public Vector3 VectorXZ() {
        return new Vector3(X, 0, Y);
    }

    public Vector2 Vector2() {
        return new Vector2(X, Y);
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

    public override int GetHashCode() {
        return X.GetHashCode() ^ Y.GetHashCode();
    }

    public static implicit operator Vector2(Point point) {
        return point.Vector2();
    }

    public static implicit operator Point(Vector2 vector2) {
        return new Point((int)vector2.x, (int)vector2.y);
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

using UnityEngine;

public struct Point : System.IEquatable<Point> {
    public int X, Y;

    public Point(int x, int y) {
        X = x;
        Y = y;
    }

    public Vector3 VectorXZ() {
        return new Vector3(X, 0, Y);
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

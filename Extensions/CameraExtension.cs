using UnityEngine;

public static class CameraExtension {
    //Returns the visible world space as a Rect. lower left is the the Rect's origin.
    public static Rect VisibleWorldRect(this Camera camera) {
        // This only works for orthographic cameras for now!
        Vector2 lowerLeft = camera.ViewportToWorldPoint(Vector2.zero);
        Vector2 upperRight = camera.ViewportToWorldPoint(Vector2.one);

        return new Rect(lowerLeft, upperRight - lowerLeft);
    }

    public static float WorldLeft(this Camera camera) {
        return camera.ViewportToWorldPoint(Vector2.zero).x;
    }

    public static float WorldBottom(this Camera camera) {
        return camera.ViewportToWorldPoint(Vector2.zero).y;
    }

    public static float WorldRight(this Camera camera) {
        return camera.ViewportToWorldPoint(Vector2.one).x;
    }

    public static float WorldTop(this Camera camera) {
        return camera.ViewportToWorldPoint(Vector2.one).y;
    }

    // Stolen from https://answers.unity.com/questions/480972/how-to-convert-screen-units-to-world-units.html?childToView=481193#comment-481193
    public static float UnitsPerPixel(this Camera camera) {
        var leftPoint = camera.ScreenToWorldPoint(Vector3.zero);
        var rightPoint = camera.ScreenToWorldPoint(Vector3.right);
        return Vector3.Distance(leftPoint, rightPoint);
    }

    public static float PixelsPerUnit(this Camera camera) {
        return 1f / UnitsPerPixel(camera);
    }
}

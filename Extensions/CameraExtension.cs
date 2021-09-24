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

    // Borrowed from this beautiful thread:
    // https://forum.unity.com/threads/fit-object-exactly-into-perspective-cameras-field-of-view-focus-the-object.496472/
    public static void FocusOn(this Camera camera, GameObject gameObject, float marginPercentage = 1f) {
        FocusOn(camera, gameObject.GetBoundsWithChildren(), marginPercentage);
    }

    // Adjusts the camera's distance to the bounds so that the entire bounds will be within view.
    // This takes both vertical and horizonal field of views into consideration.
    // It uses the bound's maximum extent, which makes sure the whole bounds is visible, but is a bit too generous 
    // because the extent magnitude goes to the corner of the bounds, which will always be a bit larger than the 
    // x/y/z of the bounds. However, this is by far the easiest way to make sure the entire object will be in view.
    public static void FocusOn(this Camera camera, Bounds bounds, float marginPercentage = 1f) {
        float maxExtent = bounds.extents.magnitude;

        float widthFOV = Camera.VerticalToHorizontalFieldOfView(camera.fieldOfView, camera.aspect);
        float smallestFOV = Mathf.Min(widthFOV, camera.fieldOfView);
        float minDistance = (maxExtent * marginPercentage) / Mathf.Sin(Mathf.Deg2Rad * smallestFOV / 2f);
        var cameraDirection = camera.transform.forward;
        camera.transform.position = bounds.center - cameraDirection * minDistance;
    }

    private static Bounds GetBoundsWithChildren(this GameObject gameObject) {
        Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
        Bounds bounds = renderers.Length > 0 ? renderers[0].bounds : new Bounds();

        for (int i = 1; i < renderers.Length; i++) {
            if (renderers[i].enabled) {
                bounds.Encapsulate(renderers[i].bounds);
            }
        }

        return bounds;
    }

    // This is a very specific method to try and get the camera as close as possible to the object, and taking the
    // camera orientation into consideration.
    // This method assumes a camera's x/y/z aligned with the bounds x/y/z, so that the horizontal calculation uses the x
    // and the vertical calculation uses the y/z axis (to support a camera angled down a bit).
    // This should work well for an angled down camera view of a battle field or a chess board, and using the bounds
    // to include the entire area that needs to be viewed.
    public static void FocusOnMinimalAspectIsometricCamera(this Camera camera, Bounds bounds, float marginPercentage = 1f) {
        // Assumes that the camera is aligned on the x/z axis with the bounds/object, and that the camera is angled downward a bit.
        float maxHeight = Mathf.Max(bounds.extents.y, bounds.extents.z);
        float verticalFOV = camera.fieldOfView;
        float minDistanceVertical = (maxHeight * marginPercentage) / Mathf.Sin(Mathf.Deg2Rad * verticalFOV / 2f);

        float maxWidth = bounds.extents.x;
        float horizontalFOV = Camera.VerticalToHorizontalFieldOfView(camera.fieldOfView, camera.aspect);
        float minDistanceHorizonal = (maxWidth * marginPercentage) / Mathf.Sin(Mathf.Deg2Rad * horizontalFOV / 2f);

        var cameraDirection = camera.transform.forward;
        camera.transform.position = bounds.center - cameraDirection * Mathf.Max(minDistanceVertical, minDistanceHorizonal);
    }
}

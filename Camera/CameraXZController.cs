using UnityEngine;


// TODO
//  - top down camera controller
//  -- that limits camera dragging/movement/zooming to a specific area
//  -- that makes initial camera placement/zoom easy


public class CameraXZController : MonoBehaviour {
    public bool IsMovementEnabled = true;
    public float MoveSpeed = 0.1f;
    public float ZoomSpeed = 1f;

    private Rect ClampedViewRect = new Rect();
    private bool IsCameraClamped = false;

    private Camera Camera;
    private CameraXZData CameraData = new CameraXZData();
    private Vector3 StartingCameraPoint;
    private TouchDragEvent TouchDragEvent = null;


    public void SetViewBounds(Rect viewBounds, Vector3 initialFocusStartingPoint) {
        Camera = Camera ?? this.GetRequiredComponent<Camera>();
        // Assuming the camera angle doesn't change, what is the view point area,
        // and determine what space the camera can move inside of that area.

        // Determine the distance that the camera needs to be to include the whole view,
        // based on the camera's FOV and Aspect ratio.
        float width = viewBounds.width;
        float widthFOV = Camera.VerticalToHorizontalFieldOfView(Camera.fieldOfView, Camera.aspect);
        float tAngle = widthFOV / 2f * Mathf.Deg2Rad;
        float opposite = width / 2f;
        float distance = opposite / Mathf.Tan(tAngle);

        var cameraDirection = transform.forward;
        var cameraEndPosition = initialFocusStartingPoint + -cameraDirection * distance;

        IsCameraClamped = viewBounds.size.magnitude > 0;
        ClampedViewRect = new Rect().Center(cameraEndPosition.Vector2XZ(), viewBounds.size);
        SetCameraLocation(cameraEndPosition);
    }

    public void SetCameraFocusPoint(Vector3 focusPoint, float distance) {
        var cameraDirection = transform.forward;
        var cameraEndPosition = focusPoint + -cameraDirection * distance;

        SetCameraLocation(cameraEndPosition);
    }

    void Awake() {
        Camera = Camera ?? this.GetRequiredComponent<Camera>();
    }

    void Update() {
        if (!IsMovementEnabled) {
            return;
        }

        UpdateCameraMovementFromKeyboard();
        UpdateCameraMovementFromDrag();
        //UpdateZoom();
    }

    void UpdateCameraMovementFromKeyboard() {
        float xMove = Mathf.Clamp(Input.GetAxis("Horizontal"), -1, 1);
        float yMove = Mathf.Clamp(Input.GetAxis("Vertical"), -1, 1);
        if (xMove == 0 && yMove == 0) {
            return;
        }

        Vector3 movement = new Vector3(xMove, 0, yMove);
        movement.Normalize();
        movement *= MoveSpeed;
        SetCameraLocation(transform.position + movement);
    }

    void UpdateCameraMovementFromDrag() {
        TouchDragEvent = TouchManager.UpdateScreenDrag();

        if (TouchDragEvent != null) {
            if (TouchDragEvent.Phase == TouchPhase.Began) {
                StartingCameraPoint = transform.position;
            }
            else if (TouchDragEvent.Phase == TouchPhase.Moved) {
                Vector3 screenDragVector = TouchDragEvent.CurrentDraggingScreenPoint - TouchDragEvent.StartDraggingScreenPoint;
                Vector3 worldDragVector = screenDragVector * Camera.UnitsPerPixel();

                SetCameraLocation(StartingCameraPoint - worldDragVector);
            }
        }
    }

    void UpdateZoom() {
        float wheel = Input.GetAxis("Mouse ScrollWheel");
        if (wheel != 0) {
            float newSize = Camera.orthographicSize + wheel * ZoomSpeed;
            UpdateCameraZoom(newSize);
        }
    }

    private void SetCameraLocation(Vector3 location) {
        CameraData.Location = IsCameraClamped ? ClampedViewRect.ClampXZ(location) : location; // Maybe make Box someday.
        transform.position = CameraData.Location;
    }

    private void UpdateCameraZoom(float zoom) {
        CameraData.Zoom = Mathf.Clamp(zoom, 1, 10);
        Camera.orthographicSize = CameraData.Zoom;
    }
}

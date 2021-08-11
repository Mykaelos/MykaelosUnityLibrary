using UnityEngine;
using UnityEngine.EventSystems;


// TODO
//  - top down camera controller
//  -- that limits camera dragging/movement/zooming to a specific area
//  -- that makes initial camera placement/zoom easy


public class CameraXZController : MonoBehaviour {
    public float MoveSpeed = 0.1f;
    public float ZoomSpeed = 1f;

    private Rect ClampedViewRect = new Rect();
    private bool IsCameraClamped = false;

    private Camera Camera;
    private CameraXZData CameraData = new CameraXZData();
    private Vector3 StartingCameraPoint;
    private TouchDragEvent TouchDragEvent = null;


    void Awake() {
        Camera = this.GetRequiredComponent<Camera>();
    }

    void Update() {
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

    public void Setup(CameraXZData cameraData = null, Rect cameraBounds = new Rect()) {
        IsCameraClamped = cameraBounds.size.magnitude > 0;
        ClampedViewRect = cameraBounds;

        CameraData = cameraData ?? new CameraXZData(Camera);

        SetCameraLocation(CameraData.Location);
        UpdateCameraZoom(CameraData.Zoom);
    }

    public void SetViewBounds(Vector3 centerPoint, Rect cameraBounds) {
        // assuming the camera angle doesn't change, what is the view point area, and determine what space the camera can move inside of that area
        Camera = this.GetRequiredComponent<Camera>();

        float width = cameraBounds.width;
        float widthFOV = Camera.VerticalToHorizontalFieldOfView(Camera.fieldOfView, Camera.aspect);
        float tAngle = widthFOV / 2f * Mathf.Deg2Rad;
        float opposite = width / 2f;
        float distance = opposite / Mathf.Tan(tAngle);

        // center camera view on a point in a plane
        //
        Debug.Log("Center: {0}".FormatWith(centerPoint));
        Vector3 focusPoint = centerPoint;
        // preserving camera y, because plane is xz

        var cameraDirection = transform.forward;
        //float distance = 25f;

        var cameraEndPosition = focusPoint + -cameraDirection * distance;

        //Plane plane = new Plane(Vector3.up, Vector3.zero);
        //plane.Raycast()

        //transform.position = cameraEndPosition;


        IsCameraClamped = cameraBounds.size.magnitude > 0;
        ClampedViewRect = new Rect().Center(cameraEndPosition.Vector2XZ(), cameraBounds.size);
        SetCameraLocation(cameraEndPosition);

        //Camera.FieldOfViewToFocalLength
    }

    void PrepareCamera() {
        Vector2 startingViewSize = ClampedViewRect.size + Vector2.one;
        Camera.orthographicSize = startingViewSize.y / 2f;

        float currentWidth = Camera.orthographicSize * 2f * Camera.aspect;
        if (startingViewSize.x > currentWidth) {
            Camera.orthographicSize = startingViewSize.x / Camera.aspect / 2f;
        }
    }
}

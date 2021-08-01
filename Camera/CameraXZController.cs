using UnityEngine;
using UnityEngine.EventSystems;

// Currently only works with 2D X,Y space. UpdateCameraMovementFromDrag is broken for 3D.
public class CameraXZController : MonoBehaviour {
    public float MoveSpeed = 0.1f;
    public float ZoomSpeed = 1f;

    private Rect ClampedViewRect = new Rect();
    private bool IsCameraClamped = false;

    private Camera Camera;
    private CameraData CameraData;
    private bool HasBeenSetup = false;


    void Awake() {
        Camera = this.GetRequiredComponent<Camera>();
    }

    private void Start() {
        if (!HasBeenSetup) {
            HasBeenSetup = true;
            Setup();
        }
    }

    void Update() {
        UpdateCameraMovement();
        UpdateCameraMovementFromDrag();
        UpdateZoom();
    }

    void UpdateCameraMovement() {
        float xMove = Mathf.Clamp(Input.GetAxis("Horizontal"), -1, 1);
        float yMove = Mathf.Clamp(Input.GetAxis("Vertical"), -1, 1);
        if (xMove == 0 && yMove == 0) {
            return;
        }

        Vector3 movement = new Vector3(xMove, yMove, 0);
        movement.Normalize();
        movement *= MoveSpeed;
        UpdateCameraLocation(transform.position + movement);
    }

    private Vector3 StartingCameraPoint;
    private TouchDragEvent TouchDragEvent = null;

    void UpdateCameraMovementFromDrag() {
        TouchDragEvent = TouchManager.UpdateScreenDrag();

        if (TouchDragEvent != null) {
            if (TouchDragEvent.Phase == TouchPhase.Began) {
                StartingCameraPoint = transform.position;
            }
            else if (TouchDragEvent.Phase == TouchPhase.Moved) {
                UpdateCameraLocation(StartingCameraPoint - (Vector3)(TouchDragEvent.CurrentDraggingScreenPoint - TouchDragEvent.StartDraggingScreenPoint) * Camera.UnitsPerPixel());
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

    private void UpdateCameraLocation(Vector2 location) {
        CameraData.Location = IsCameraClamped ? (Vector2)ClampedViewRect.Clamp(location) : location;
        transform.position = CameraData.Location.ToVector3(transform.position.z);
    }

    private void UpdateCameraZoom(float zoom) {
        CameraData.Zoom = Mathf.Clamp(zoom, 1, 10);
        Camera.orthographicSize = CameraData.Zoom;
    }

    public void Setup(CameraData cameraData = null, Rect cameraBounds = new Rect()) {
        IsCameraClamped = cameraBounds.size.magnitude > 0;
        ClampedViewRect = cameraBounds;

        CameraData = cameraData ?? new CameraData(Camera);

        UpdateCameraLocation(CameraData.Location);
        UpdateCameraZoom(CameraData.Zoom);
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

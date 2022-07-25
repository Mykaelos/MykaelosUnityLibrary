using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObjectController : MonoBehaviour {
    public GameObject FollowTarget;
    private Vector3 VectorDifference; // Used to keep the camera a specific distance from the target.


    public void SetFollowTarget(GameObject followTarget) {
        FollowTarget = followTarget;
        VectorDifference = transform.position - FollowTarget.transform.position;
    }

    private void Start() {
        if (FollowTarget != null) {
            SetFollowTarget(FollowTarget); // Initialize target if it was added via the scene.
        }
    }

    private void LateUpdate() {
        if (FollowTarget != null) {
            transform.position = FollowTarget.transform.position + VectorDifference;
        }
    }
}

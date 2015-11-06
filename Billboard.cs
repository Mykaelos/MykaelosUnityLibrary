using UnityEngine;

public class Billboard : MonoBehaviour {

    //LateUpdate prevents any flickering that might happen if the camera moves after the Billboards update.
    //Camera movement should happen during the update function.
    void LateUpdate() {
        Vector3 cameraDirection = Camera.main.transform.forward;
        cameraDirection.y = 0.0f; //remove the y rotation
        transform.rotation = Quaternion.LookRotation(cameraDirection);
    }
}

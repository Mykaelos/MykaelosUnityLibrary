using UnityEngine;

public class Billboard : MonoBehaviour {

    //LateUpdate prevents any flickering that might happen if the camera moves after the Billboards update.
    //Camera movement should happen during the update function.
    void LateUpdate() {
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
    }
}

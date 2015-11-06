using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {

    void LateUpdate() {
        Vector3 dir = Camera.main.transform.forward;
        dir.y = 0.0f;
        transform.rotation = Quaternion.LookRotation(dir);
    }
}

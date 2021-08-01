using System;
using UnityEngine;

[Serializable]
public class CameraXZData {
    public Vector3Serializable Location = new Vector3Serializable();
    public float Zoom;
    public Vector3 MovementDirection = Vector3.right + Vector3.up;


    public CameraXZData() { }

    public CameraXZData(Camera camera) {
        Location = camera.transform.position;
        Zoom = camera.orthographicSize;
    }
}

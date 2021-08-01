using System;
using UnityEngine;

[Serializable]
public class CameraData {
    public Vector2Serializable Location = new Vector2Serializable();
    public float Zoom;


    public CameraData() { }

    public CameraData(Camera camera) {
        Location = camera.transform.position;
        Zoom = camera.orthographicSize;
    }
}

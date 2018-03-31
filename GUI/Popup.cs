using UnityEngine;
using System.Collections.Generic;

public class Popup : MonoBehaviour {
    private static Popup Self;

    public List<GameObject> Popups = new List<GameObject>();
    private GameObject PopupPrefab;
    public float SpawnY;


    public void Awake() {
        Self = this;
        PopupPrefab = Resources.Load<GameObject>("GUI/PopupPrefab");
    }

    public void Start() {
        SpawnY = ((RectTransform)transform).rect.height / -2f;
        //Debug.Log("SpawnY" + SpawnY);
    }

    public static void Show(string message, float duration = 5) {
        GameObject popup = GameObject.Instantiate(Self.PopupPrefab);
        popup.transform.SetParent(Self.transform, false);
        //Debug.Log("Self.SpawnY" + Self.SpawnY);
        popup.GetComponent<PopupController>().Configure(message, Self, Self.SpawnY, duration);
        Self.Popups.Add(popup);
    }

    public void Remove(GameObject popup) {
        if (popup == null) {
            Popups.RemoveAt(0); //dequeue
        }
        else {
            Popups.Remove(popup);
        }
        //calc top
    }
}

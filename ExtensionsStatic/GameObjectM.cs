using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectM {

    public static T GetComponentOnObject<T>(string gameObjectName) {
        var gameObject = GameObject.Find(gameObjectName);
        if (gameObject == null) {
            return default(T);
        }
        return gameObject.GetComponent<T>();
    }

    public static GameObject FindAnywhere(string gameObjectName) {
        Transform[] transforms = GameObject.FindObjectsOfType(typeof(Transform)) as Transform[];
        foreach (var transform in transforms) {
            if (transform.name.Equals(gameObjectName)) {
                return transform.gameObject;
            }
            var child = transform.FindFirstChildByName(gameObjectName);
            if (child != null) {
                return child.gameObject;
            }
        }

        return null;
    }
}

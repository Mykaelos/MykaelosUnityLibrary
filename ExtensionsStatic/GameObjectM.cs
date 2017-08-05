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
}

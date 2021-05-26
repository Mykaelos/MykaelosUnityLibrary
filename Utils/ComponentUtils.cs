using UnityEngine;
using UnityEditor;

public class ComponentUtils {

    public static T Get<T>(string gameObjectName) where T : Component {
        var gameObject = GameObject.Find(gameObjectName);

        return gameObject != null ? gameObject.GetComponent<T>() : default;
    }
}

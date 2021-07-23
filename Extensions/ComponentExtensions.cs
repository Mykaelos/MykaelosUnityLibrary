using UnityEngine;

public static class ComponentExtensions {
    // Have to use 'this' for Component Extensions.

    // this.GetComponentAnywhere<CustomController>("CustomerObject");
    public static T GetComponentAnywhere<T>(this Component component, string gameObjectName) {
        var gameObject = GameObject.Find(gameObjectName);

        return gameObject != null ? gameObject.GetComponent<T>() : default(T);
    }

    // this.GetComponentInChild<SpriteRenderer>("Sprite");
    public static T GetComponentInChild<T>(this Component component, string childName) {
        var child = component.transform.FindFirstChildByName(childName);

        return child != null ? child.GetComponent<T>() : default(T);
    }

    public static T GetRequiredComponentInChild<T>(this Component component, string childName) {
        var requiredComponent = component.GetComponentInChild<T>(childName);
        if (requiredComponent == null) {
            Debug.LogError(typeof(T).Name + " IS REQUIRED FOR " + component.gameObject.name);
        }

        return requiredComponent;
    }

    public static T GetRequiredComponent<T>(this Component component) {
        var requiredComponent = component.GetComponent<T>();
        if (requiredComponent == null) {
            Debug.LogError(typeof(T).Name + " IS REQUIRED FOR " + component.gameObject.name);
        }

        return requiredComponent;
    }

    public static T GetRequiredComponent<T>(this Component component, string gameObjectName) {
        var gameObject = GameObject.Find(gameObjectName);
        if (gameObject == null) {
            Debug.LogError(gameObjectName + " NOT FOUND IN SCENE");
            return default;
        }

        var requiredComponent = gameObject.GetComponent<T>();
        if (requiredComponent == null) {
            Debug.LogError(typeof(T).Name + " IS REQUIRED FOR " + gameObjectName);
        }

        return requiredComponent;
    }
}

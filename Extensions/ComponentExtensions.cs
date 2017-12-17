using UnityEngine;

public static class ComponentExtensions {
    // Have to use 'this' for Component Extensions.

    // this.GetComponentInChild<SpriteRenderer>("Sprite");
    public static T GetComponentInChild<T>(this Component component, string childName) {
        var child = component.transform.FindFirstChildByName(childName);

        return child != null ? child.GetComponent<T>() : default(T);
    }
}

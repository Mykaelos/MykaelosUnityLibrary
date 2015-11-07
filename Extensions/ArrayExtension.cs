using UnityEngine;

public static class ArrayExtension {

    public static T RandomElement<T>(this T[] array) {
        if (array.Length == 0) {
            return default(T);
        }

        return array[Random.Range(0, array.Length)];
    }
}

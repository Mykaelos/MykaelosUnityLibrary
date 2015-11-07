using UnityEngine;
using System.Collections.Generic;

public static class ListExtension {

    public static T RandomElement<T>(this List<T> array) {
        if (array.Count == 0) {
            return default(T);
        }

        return array[Random.Range(0, array.Count)];
    }
}

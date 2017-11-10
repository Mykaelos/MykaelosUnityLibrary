using System.Collections.Generic;
using UnityEngine;

public static class IEnumerableExtension {
    /**
     * IsNullOrEmpty can be used on a null object because Extension methods are effectively static methods at compile time.
     * Example:
     * object[] args;
     * if (args.IsNullOrEmpty()) {
     *    // act on the empty array.
     * }
     */
    public static bool IsNullOrEmpty<T>(this ICollection<T> collection) {
        if (collection == null) {
            return true;
        }
        return collection.Count < 1;
    }
}

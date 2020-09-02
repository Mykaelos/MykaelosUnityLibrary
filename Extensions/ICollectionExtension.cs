using System.Collections;

public static class ICollectionExtension {
    /**
     * IsNullOrEmpty can be used on a null object because Extension methods are effectively static methods at compile time.
     * Example:
     * object[] args;
     * if (args.IsNullOrEmpty()) {
     *    // act on the empty array.
     * }
     */
    public static bool IsNullOrEmpty(this ICollection collection) {
        return collection == null || collection.Count < 1;
    }

    public static bool IsNotEmpty(this ICollection collection) {
        return !IsNullOrEmpty(collection);
    }
}

using System.Collections.Generic;
using UnityEngine;

public static class IEnumerableExtension {

    //http://danielvaughan.org/post/IEnumerable-IsNullOrEmpty.aspx
    //public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable) {
    //    if (enumerable == null) {
    //        return true;
    //    }
    //    /* If this is a list, use the Count property. 
    //     * The Count property is O(1) while IEnumerable.Count() is O(N). */
    //    var collection = enumerable as ICollection<T>;
    //    if (collection != null) {
    //        return collection.Count < 1;
    //    }

    //    return enumerable.;
    //}

    public static bool IsNullOrEmpty<T>(this ICollection<T> collection) {
        if (collection == null) {
            return true;
        }
        return collection.Count < 1;
    }
}

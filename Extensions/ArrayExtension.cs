using UnityEngine;

public static class ArrayExtension {

    public static T RandomElement<T>(this T[] array) {
        if (array.Length == 0) {
            return default(T);
        }

        return array[Random.Range(0, array.Length)];
    }

    public static void Swap<T>(this T[] array, int index1, int index2) {
        T temp = array[index1];
        array[index1] = array[index2];
        array[index2] = temp;
    }
}

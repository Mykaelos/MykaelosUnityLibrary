using UnityEngine;
using System.Collections.Generic;
using System.Text;

public static class ListExtension {

    // Randomly shuffles the list.
    // Borrowed from https://stackoverflow.com/a/1262619.
    public static List<T> Shuffle<T>(this List<T> list) {
        int x = list.Count;
        while (x > 1) {
            x--;
            list.Swap(Random.Range(0, x + 1), x);
        }

        return list; // Method Chaining.
    }

    public static T RandomElement<T>(this List<T> list) {
        if (list.Count == 0) {
            return default(T);
        }

        return list[Random.Range(0, list.Count)];
    }

    public static T RandomElementExcept<T>(this List<T> list, T excludedElement) {
        if (list.Count == 0) {
            return default(T);
        }

        List<T> listWithoutElement = new List<T>(list);
        if (excludedElement != null) {
            listWithoutElement.Remove(excludedElement);
        }

        return RandomElement(listWithoutElement);
    }

    public static T RandomElementExcept<T>(this List<T> list, List<T> excludedElements) {
        if (list.Count == 0) {
            return default(T);
        }

        List<T> listWithoutElement = new List<T>(list);
        if (excludedElements.IsNotEmpty()) {
            foreach (var excludeItem in excludedElements) {
                listWithoutElement.Remove(excludeItem);
            }
        }

        return RandomElement(listWithoutElement);
    }

    public static int RandomIndexByWeight(this List<float> weightsList, float totalWeight = -1) {
        if (weightsList.Count == 0) {
            return -1;
        }
        if(totalWeight == -1) {
            totalWeight = TotalWeights(weightsList);
        }

        float choice = Random.Range(0f, totalWeight);
        float bottomWeight = 0;
        for (int i = 0; i < weightsList.Count; i++) {
            if (choice < bottomWeight + weightsList[i]) {
                return i;
            }
            bottomWeight += weightsList[i];
        }

        return -1; //no idea how you got here, but you didn't find anything.
    }

    public static float TotalWeights(this List<float> weightsList) {
        float totalWeight = 0;
        for (int i = 0; i < weightsList.Count; i++) {
            totalWeight += weightsList[i];
        }
        return totalWeight;
    }

    public static List<T> Swap<T>(this List<T> list, int index1, int index2) {
        var temp = list[index1];
        list[index1] = list[index2];
        list[index2] = temp;

        return list; // Method Chaining.
    }

    public static T First<T>(this List<T> list) {
        return !list.IsNullOrEmpty() ? list[0] : default(T);
    }

    public static T Last<T>(this List<T> list) {
        return !list.IsNullOrEmpty() ? list[list.Count - 1] : default(T);
    }

    public static List<T> RemoveFirst<T>(this List<T> list) {
        if (!list.IsNullOrEmpty()) {
            list.RemoveAt(0);
        }

        return list; // Method Chaining.
    }

    public static List<T> RemoveLast<T>(this List<T> list) {
        if (!list.IsNullOrEmpty()) {
            list.RemoveAt(list.Count - 1);
        }

        return list; // Method Chaining.
    }

    // An actual implementation of List.ToString() that ToStrings all of the elements inside of the string.
    // Borrowed with some clean up and generics from https://forum.unity.com/threads/noob-question-debug-log-a-list.761591/#post-5072021.
    public static string ToString<T>(this List<T> list, string delimiter = ",", int builderCapacity = 500) {
        if (list == null) {
            return "null";
        }

        int lastIndex = list.Count - 1;
        if (lastIndex == -1) {
            return "{}";
        }

        var builder = new StringBuilder(builderCapacity);
        builder.Append('{');
        for (int n = 0; n < lastIndex; n++) {
            Append(list[n], builder);
            builder.Append(delimiter);
        }
        Append(list[lastIndex], builder);
        builder.Append('}');

        return builder.ToString();
    }

    private static void Append<T>(T target, StringBuilder toBuilder) {
        if (target == null) {
            toBuilder.Append("null");
        }
        else {
            toBuilder.Append(target.ToString());
        }
    }
}

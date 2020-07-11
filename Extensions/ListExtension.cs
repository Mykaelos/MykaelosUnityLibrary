using UnityEngine;
using System.Collections.Generic;

public static class ListExtension {

    // Randomly shuffles the list.
    // Borrowed from https://stackoverflow.com/a/1262619
    public static void Shuffle<T>(this List<T> list) {
        int x = list.Count;
        while (x > 1) {
            x--;
            list.Swap(Random.Range(0, x + 1), x);
        }
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

        if (listWithoutElement.Count == 0) {
            return default(T);
        }

        return listWithoutElement[Random.Range(0, listWithoutElement.Count)];
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

    public static void Swap<T>(this List<T> list, int index1, int index2) {
        var temp = list[index1];
        list[index1] = list[index2];
        list[index2] = temp;
    }

    public static T First<T>(this List<T> list) {
        return !list.IsNullOrEmpty() ? list[0] : default(T);
    }

    public static T Last<T>(this List<T> list) {
        return !list.IsNullOrEmpty() ? list[list.Count - 1] : default(T);
    }

    public static void RemoveFirst<T>(this List<T> list) {
        if (!list.IsNullOrEmpty()) {
            list.RemoveAt(0);
        }
    }

    public static void RemoveLast<T>(this List<T> list) {
        if (!list.IsNullOrEmpty()) {
            list.RemoveAt(list.Count - 1);
        }
    }
}

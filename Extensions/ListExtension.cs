using UnityEngine;
using System.Collections.Generic;

public static class ListExtension {

    public static T RandomElement<T>(this List<T> list) {
        if (list.Count == 0) {
            return default(T);
        }

        return list[Random.Range(0, list.Count)];
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
}

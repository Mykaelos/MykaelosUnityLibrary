using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeightedList<T> {
    public List<T> Items = new List<T>();
    public List<float> Weights = new List<float>();
    public float TotalWeight = 0;

    //http://stackoverflow.com/questions/3120035/indexing-count-of-buckets/3120179#3120179
    //http://stackoverflow.com/questions/4511331/randomly-selecting-an-element-from-a-weighted-list

    public WeightedList(Dictionary<T, float> items) : this(new List<T>(items.Keys), new List<float>(items.Values)) { }

    public WeightedList(List<T> items, List<float> weights) {
        Items = items;
        Weights = weights;
        CalculateTotalWeight();
    }

    public void Add(T item, float weight) {
        Items.Add(item);
        Weights.Add(weight);
        TotalWeight += weight;
    }

    public bool Remove(T item) {
        int index = Items.IndexOf(item);
        if(index == -1) {
            return false;
        }
        Items.RemoveAt(index);
        TotalWeight -= Weights[index];
        Weights.RemoveAt(index);
        return true;
    }

    void CalculateTotalWeight() {
        TotalWeight = 0;
        for (int i = 0; i < Weights.Count; i++) {
            TotalWeight += Weights[i];
        }
    }

    public int RandomIndexByWeight() {
        if (Weights.Count == 0) {
            return -1;
        }

        float choice = Random.Range(0f, TotalWeight);
        float bottomWeight = 0;
        for (int i = 0; i < Weights.Count; i++) {
            if (choice < bottomWeight + Weights[i]) {
                return i;
            }
            bottomWeight += Weights[i];
        }

        return -1; //no idea how you got here, but you didn't find anything.
    }

    public T RandomByWeight() {
        int index = RandomIndexByWeight();
        if(index == -1) {
            return default(T);
        }
        return Items[index];
    }
}

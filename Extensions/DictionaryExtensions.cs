﻿using System.Collections.Generic;

public static class DictionaryExtensions {
    public static V Get<K, V>(this Dictionary<K, V> dictionary, K key, V defaultValue = default(V)) {
        V foundValue;
        return dictionary.TryGetValue(key, out foundValue) ? foundValue : defaultValue; //default of an object is null
    }

    public static void Set<K, V>(this Dictionary<K, V> dictionary, K key, V value) {
        dictionary[key] = value;
    }

    public static V RandomElement<K, V>(this Dictionary<K, V> dictionary) {
        if (dictionary.Count == 0) {
            return default(V);
        }

        return new List<V>(dictionary.Values).RandomElement();
    }

    public static V First<K, V>(this Dictionary<K, V> dictionary, V defaultValue = default(V)) {
        V foundValue = defaultValue;
        using (var iterator = dictionary.Keys.GetEnumerator()) {
            if (iterator.MoveNext()) {
                foundValue = dictionary.Get(iterator.Current, defaultValue);
            }
        }

        return foundValue;
    }

    public static bool Has<K, V>(this Dictionary<K, V> dictionary, K key) {
        return dictionary.ContainsKey(key);
    }
}

/* Notes:
 * dictionary[key]++; // This will break if no value exists for key. Has(key) can be used, or just use Set(Get()+1) to keep it a one-liner.
 * dictionary.Set(key, dictionary.Get(key) + 1);
 */

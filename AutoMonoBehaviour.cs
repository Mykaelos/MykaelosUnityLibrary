using UnityEngine;
using System.Collections;

//The point of this Component is to make itself available for other classes to be able to use the StartCoroutine functionality.
public class AutoMonoBehaviour : MonoBehaviour {
    public static MonoBehaviour Instantiate(GameObject parent = null) {
        if(parent == null) {
            parent = new GameObject("AutoMonoBehaviour");
        }
        return parent.AddComponent<AutoMonoBehaviour>();
    }
}

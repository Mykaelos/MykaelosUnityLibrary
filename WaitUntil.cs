using UnityEngine;
using System.Collections;

public class WaitUntil : MonoBehaviour {
    private static WaitUntil _Self;
    private static WaitUntil Self {
        get { return _Self != null ? _Self : _Self = (new GameObject("WaitUntil")).AddComponent<WaitUntil>(); }
    }


    public static void IsTrue(System.Func<bool> checkFn, System.Action<bool> callback, float maxDuration) {
        Self.StartCoroutine(Self.IsTrueLoop(checkFn, callback, maxDuration));
    }

    private IEnumerator IsTrueLoop(System.Func<bool> checkFn, System.Action<bool> callback, float maxDuration) {
        float startTime = Time.time;
        bool success = false;
        while (!success && Time.time - startTime <= maxDuration) {
            if (checkFn()) {
                success = true;
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }
        callback(success);
    }
}

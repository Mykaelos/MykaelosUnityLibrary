using UnityEngine;
using System.Collections;
//Partically borrowed with refactoring from:
//http://unitytipsandtricks.blogspot.com/2013/05/camera-shake.html

public class CameraShaker : MonoBehaviour {
    public bool IsShaking = false;
    public float Duration = 0;
    public float Magnitude = 0;
    //public static int DebugCounter = 0;

    static CameraShaker _Instance = null;
    static CameraShaker Instance {
        get { return _Instance == null ? _Instance = Camera.main.gameObject.AddComponent<CameraShaker>() : _Instance; }
    }

    
    public static void ShakeDuration(float duration, float magnitude) {
        Instance.StartCoroutine(Instance.Shaker(duration, magnitude));
    }
    
    public static void ShakeSingleFrame(float magnitude) {
        Instance.StartCoroutine(Instance.Shaker(0f, magnitude));
    }

    IEnumerator Shaker(float duration, float magnitude) {
        if(IsShaking) {
            if(duration > 0) {//excluding single frame shake, which would keep a new magnitude indefinitely.
                Duration = Mathf.Max(duration, Duration);
                Magnitude = Mathf.Max(magnitude, Magnitude);
            }
            yield break;
        }
        Instance.IsShaking = true;
        Duration = duration;
        Magnitude = magnitude;

        //int currentDebugCounter = ++DebugCounter;
        float elapsed = 0f;
        Vector3 originalPosition = transform.localPosition;
        
        do {
            float percentComplete = elapsed / (Duration > 0 ? Duration : 1f);
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            float x = Random.Range(-1f, 1f);
            float y = Random.Range(-1f, 1f);
            x *= Magnitude * damper;
            y *= Magnitude * damper;

            transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;
            //Debug.Log(string.Format("counter: {3}, originalPosition: {0}/{4}, percentComplete: {1}, damper: {2}", 
            //    originalPosition, percentComplete, damper, currentDebugCounter, transform.localPosition));

            yield return null;
        } while(elapsed < Duration);

        transform.localPosition = originalPosition;
        //Debug.Log(string.Format("counter: {1}, originalPosition: {0}/{2}",
        //        originalPosition, currentDebugCounter, transform.localPosition));
        Instance.IsShaking = false;
    }
}

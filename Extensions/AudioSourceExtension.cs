using UnityEngine;
using System.Collections;
using System;

public static class AudioSourceExtension {

    public static AudioSourceFadeState FadeOut(this AudioSource source, MonoBehaviour behaviour = null, float waitDuration = 0f, float fadeDuration = 3f, bool stopOnFinished = true, Action finishedCallback = null) {
        behaviour = behaviour ?? AutoMonoBehaviour.Instantiate(source.gameObject);
        AudioSourceFadeState fadeState = new AudioSourceFadeState(source);
        behaviour.StartCoroutine(StartFadeOut(source, waitDuration, fadeDuration, stopOnFinished, finishedCallback, fadeState));

        return fadeState;
    }

    private static IEnumerator StartFadeOut(AudioSource source, float waitDuration, float fadeDuration, bool stopOnFinished, Action finishedCallback, AudioSourceFadeState fadeState) {
        if (waitDuration > 0) {
            yield return new WaitForSeconds(waitDuration);
        }
        float startingVolume = source.volume;
        float currentDuration = 0;
        float startTime = Time.time;

        while (currentDuration < fadeDuration && fadeState.IsFading) {
            source.volume = Mathf.Lerp(startingVolume, 0, currentDuration / fadeDuration);
            yield return null;
            currentDuration = Time.time - startTime;
        }
        fadeState.IsFading = false;

        if (finishedCallback != null && fadeState.CallCallback) {
            finishedCallback();
        }

        if (stopOnFinished && fadeState.ShouldStop) {
            source.Stop();
        }
    }
}

public class AudioSourceFadeState {
    public AudioSource Source;
    public bool IsFading = false;
    public bool CallCallback = true;
    public bool ShouldStop = true;
    

    public AudioSourceFadeState(AudioSource source, bool isFading = true, bool callCallback = true, bool shouldStop = true) {
        Source = source;
        IsFading = isFading;
        CallCallback = callCallback;
        ShouldStop = shouldStop;
    }
}

/* 
TODOs
- Rename this file to AudioSourceFadeExtension
- Create a new AudioSourceExtension that does some nice batch methods like AudioManager.PlaySound() with pitch, volume, etc.
*/

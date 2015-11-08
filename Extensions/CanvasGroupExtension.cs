using UnityEngine;
using System.Collections;
using System;

public static class CanvasGroupExtension {
   
    public static void SetVisible(this CanvasGroup group, bool isVisible) {
        group.alpha = isVisible ? 1 : 0;
        group.blocksRaycasts = isVisible;
        group.interactable = isVisible;
    }

    public static void FadeOut(this CanvasGroup group, MonoBehaviour behaviour = null, float fadeDuration = 3f, Action finishedCallback = null) {
        behaviour = behaviour ?? AutoMonoBehaviour.Instantiate(group.gameObject);
        behaviour.StartCoroutine(StartFadeOut(group, fadeDuration, finishedCallback));
    }

    private static IEnumerator StartFadeOut(CanvasGroup group, float fadeDuration, Action finishedCallback) {
        float startingAlpha = group.alpha;
        float currentDuration = 0;
        float startTime = Time.time;

        while (currentDuration < fadeDuration) {
            group.alpha = Mathf.Lerp(startingAlpha, 0, currentDuration / fadeDuration);
            yield return 0;
            currentDuration = Time.time - startTime;
        }

        group.SetVisible(false);

        if (finishedCallback != null) {
            finishedCallback();
        }
    }
}

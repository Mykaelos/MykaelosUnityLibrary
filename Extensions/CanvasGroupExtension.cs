using UnityEngine;
using System.Collections;
using System;

public static class CanvasGroupExtension {
   
    public static void Show(this CanvasGroup group) {
        SetVisible(group, true);
    }

    public static void Hide(this CanvasGroup group) {
        SetVisible(group, false);
    }

    public static void SetVisible(this CanvasGroup group, bool isVisible, bool isSolid = true) {
        group.alpha = isVisible ? 1 : 0;
        group.blocksRaycasts = isVisible && isSolid;
        group.interactable = isVisible && isSolid;
    }

    public static bool IsVisible(this CanvasGroup group, bool requiresSolid = false) {
        return group.alpha == 1 && (requiresSolid ? group.blocksRaycasts && group.interactable : true);
    }

    public static void ToggleVisible(this CanvasGroup group, bool isSolid = true) {
        group.SetVisible(!group.IsVisible(), isSolid);
    }

    public static IEnumerator FadeOut(this CanvasGroup group, MonoBehaviour behaviour = null, float waitDuration = 0, float fadeDuration = 3f, Action finishedCallback = null) {
        behaviour = behaviour ?? AutoMonoBehaviour.Instantiate(group.gameObject);
        IEnumerator coroutine = StartFadeOut(group, waitDuration, fadeDuration, finishedCallback);
        behaviour.StartCoroutine(coroutine);
        return coroutine;
    }

    private static IEnumerator StartFadeOut(CanvasGroup group, float waitDuration, float fadeDuration, Action finishedCallback) {
        if (waitDuration > 0) {
            yield return new WaitForSeconds(waitDuration);
        }
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

    public static void StopFadeOut(this CanvasGroup group, IEnumerator coroutine, MonoBehaviour behaviour = null) {
        behaviour = behaviour ?? group.gameObject.GetComponent<AutoMonoBehaviour>();
        if (behaviour == null) {
            Debug.Log("No behaviour to stop fade out!");
            return;
        }
        behaviour.StopCoroutine(coroutine);
    }
}

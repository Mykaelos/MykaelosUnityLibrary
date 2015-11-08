using UnityEngine;
using System.Collections;
using System;

public static class SpriteRendererExtension {

    public static void SetAlpha(this SpriteRenderer spriteRenderer, float alpha) {
        Color temp = spriteRenderer.color;
        temp.a = alpha;
        spriteRenderer.color = temp;
    }

    public static void FadeOut(this SpriteRenderer spriteRenderer, MonoBehaviour behaviour = null, float waitDuration = 0f, float fadeDuration = 3f, bool destroyOnFinished = true, Action finishedCallback = null) {
        behaviour = behaviour ?? AutoMonoBehaviour.Instantiate(spriteRenderer.gameObject);
        behaviour.StartCoroutine(StartFadeOut(spriteRenderer, waitDuration, fadeDuration, destroyOnFinished, finishedCallback));
    }

    private static IEnumerator StartFadeOut(SpriteRenderer spriteRenderer, float waitDuration, float fadeDuration, bool destroyOnFinished, Action finishedCallback) {
        if(waitDuration > 0) {
            yield return new WaitForSeconds(waitDuration);
        }
        float startingAlpha = spriteRenderer.color.a;
        float currentDuration = 0;
        float startTime = Time.time;

        while(currentDuration < fadeDuration) {
            spriteRenderer.SetAlpha(Mathf.Lerp(startingAlpha, 0, currentDuration / fadeDuration));
            yield return 0;
            currentDuration = Time.time - startTime;
        }

        if(finishedCallback != null) {
            finishedCallback();
        }

        if(destroyOnFinished) {
            GameObject.Destroy(spriteRenderer.gameObject);
        }
    }
}

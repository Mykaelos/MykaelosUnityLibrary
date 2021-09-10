using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour {
    private Image Image;

    private float Direction; //-1 is fade out, 1 is fade in
    private float CurrentDuration;
    private float Duration;

    public const float FADEOUT = 1; //fade to black
    public const float FADEIN = -1; //fade to clear

    private static bool _IsFading = false;
    public static bool IsFading {
        get { return _IsFading; }
    }

    public static bool IsFaded {
        get {
            return Instance.Direction == FADEOUT && !IsFading;
        }
    }


    public static void FadeOut(float duration, Color color, Action finishedCallback = null) {
        Fade(FADEOUT, duration, color, finishedCallback);
    }

    public static void FadeIn(float duration, Color color, Action finishedCallback = null) {
        Fade(FADEIN, duration, color, finishedCallback);
    }

    public static void Fade(float fadeDirection, float duration, Color color, Action finishedCallback = null) {
        Instance.Direction = fadeDirection;
        Instance.Duration = duration;

        Instance.Image.color = color;

        Instance.StartCoroutine(Instance.StartFade(finishedCallback));
    }

    public static void SetFaded(bool isFaded, Color color) {
        Instance.Image.color = color;
        Instance.Image.SetAlpha(isFaded ? 1 : 0);
        Instance.Image.raycastTarget = Instance.Image.IsVisible();
    }

    private static TransitionManager _Instance = null;
    private static TransitionManager Instance {
        get { return _Instance == null ? _Instance = (new GameObject("TransitionManager")).AddComponent<TransitionManager>() : _Instance; }
    }

    private void Awake() {
        DontDestroyOnLoad(this);
        _Instance = this;

        BuildCanvas();
    }

    private void BuildCanvas() {
        var canvas = gameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        gameObject.AddComponent<GraphicRaycaster>();

        var imageGameObject = new GameObject("Image");
        imageGameObject.transform.SetParent(transform);
        var rectTransform = imageGameObject.AddComponent<RectTransform>();
        rectTransform.anchoredPosition3D = Vector3.zero;
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;

        Image = imageGameObject.AddComponent<Image>();
        var texture = Texture2D.whiteTexture;
        Image.sprite = Sprite.Create(Texture2D.whiteTexture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
        Image.raycastTarget = true;
    }

    private IEnumerator StartFade(Action finishedCallback) {
        _IsFading = true;
        CurrentDuration = Direction > 0 ? 0.01f : Duration - 0.01f;
        Image.SetAlpha(CurrentDuration / Duration);
        Image.raycastTarget = Image.IsVisible();

        while (CurrentDuration / Duration > 0 && CurrentDuration / Duration < 1) {
            yield return new WaitForSeconds(0.01f);

            CurrentDuration += Direction * Time.deltaTime;
            Image.SetAlpha(CurrentDuration / Duration);
        }

        Image.SetAlpha(CurrentDuration / Duration);
        Image.raycastTarget = Image.IsVisible();

        _IsFading = false;
        if (finishedCallback != null) {
            finishedCallback();
        }
    }
}

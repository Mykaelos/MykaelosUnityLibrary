using UnityEngine;
using System.Collections;
using System;

public class FadeTransition : MonoBehaviour {
    private Texture2D Texture = Texture2D.whiteTexture; //super tiny basic texture
    private int DrawDepth = -1000; //forces the texture to be drawn last
    private Color OpaqueColor = Color.black;
    private Color TransparentColor = Color.clear;
    private Color CurrentColor = Color.clear;
    private float Direction; //-1 is fade out, 1 is fade in
    private float CurrentDuration;
    private float Duration;

    //Public variables
    public static bool IsFading = false;
    public const float FadeOut = 1; //fade to black
    public const float FadeIn = -1; //fade to clear


    public static void Fade(float direction, float duration, Color color, Action finishedCallback = null) {
        Instance.Direction = direction;
        Instance.Duration = duration;

        Instance.OpaqueColor = color;
        Instance.TransparentColor = new Color(color.r, color.g, color.b, 0);

        Instance.StartCoroutine(Instance.StartFade(finishedCallback));
    }

    public static void SetOpaque(bool opaque) {
        Instance.CurrentColor = opaque ? Instance.OpaqueColor : Instance.TransparentColor;
    }

    public static bool IsFaded() {
        return Instance.CurrentColor.Equals(Instance.OpaqueColor);
    }


    private static FadeTransition _Instance = null;
    private static FadeTransition Instance {
        get { return _Instance == null ? _Instance = (new GameObject("FadeTransition")).AddComponent<FadeTransition>() : _Instance; }
    }

    private void Awake() {
        DontDestroyOnLoad(this);
        _Instance = this;
    }

    private IEnumerator StartFade(Action finishedCallback) {
        IsFading = true;
        CurrentDuration = Direction > 0 ? 0.01f : Duration - 0.01f;
        CurrentColor = Direction > 0 ? TransparentColor : OpaqueColor;

        while (CurrentDuration / Duration > 0 && CurrentDuration / Duration < 1) {
            yield return new WaitForSeconds(0.01f);
            CurrentDuration += Direction * Time.deltaTime;
            CurrentColor = Color.Lerp(TransparentColor, OpaqueColor, CurrentDuration / Duration);
        }

        CurrentColor = Direction > 0 ? OpaqueColor : TransparentColor;
        IsFading = false;
        if (finishedCallback != null) {
            finishedCallback();
        }
    }

    private void OnGUI() {
        Color previousColor = GUI.color;
        int previousDepth = GUI.depth;

        GUI.color = CurrentColor;
        GUI.depth = DrawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture);

        //Reset the previous values or it will affect other OnGUI calls.
        GUI.color = previousColor;
        GUI.depth = previousDepth;
    }
}

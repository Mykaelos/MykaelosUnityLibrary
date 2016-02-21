using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ImageAnimator : MonoBehaviour {
    public List<Sprite> Sprites = new List<Sprite>(2);//can dynamically increase in editor
    public float FPS = 2;
    public int CurrentFrame = 0;
    public bool IsAnimating = true;

    private Image Image;
    private Timer NextFrameTimer;


    void Awake() {
        Image = GetComponent<Image>();
        NextFrameTimer = new Timer();
    }

    public void Start() {
        if (Sprites != null && Sprites.Count > 0) {
            Image.sprite = Sprites[CurrentFrame];
        }
    }

    public void FixedUpdate() {
        if (IsAnimating && NextFrameTimer.Check(1f / FPS)) {
            NextFrameTimer.Reset();
            if (++CurrentFrame >= Sprites.Count) {
                CurrentFrame = 0;
            }
            Image.sprite = Sprites[CurrentFrame];
        }
    }
}

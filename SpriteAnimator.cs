﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteAnimator : MonoBehaviour {
    public List<Sprite> Sprites = new List<Sprite>(2);//can dynamically increase in editor
    public float FPS = 2;
    public int CurrentFrame = 0;
    public bool IsAnimating = true;

    private SpriteRenderer Renderer;
    private Timer NextFrameTimer;


    void Awake() {
        Renderer = GetComponent<SpriteRenderer>();
        NextFrameTimer = new Timer();
    }

    public void Start() {
        Renderer.sprite = Sprites[CurrentFrame];
    }

    public void FixedUpdate() {
        if (IsAnimating && NextFrameTimer.Check(1f / FPS)) {
            NextFrameTimer.Reset();
            if (++CurrentFrame >= Sprites.Count) {
                CurrentFrame = 0;
            }
            Renderer.sprite = Sprites[CurrentFrame];
        }
    }
}
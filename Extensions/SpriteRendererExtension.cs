using UnityEngine;
using System.Collections;

public static class SpriteRendererExtension {

    public static void SetAlpha(this SpriteRenderer spriteRenderer, float alpha) {
        Color temp = spriteRenderer.color;
        temp.a = alpha;
        spriteRenderer.color = temp;
    }
}

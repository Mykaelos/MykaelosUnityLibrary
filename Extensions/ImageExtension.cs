using UnityEngine;
using UnityEngine.UI;

public static class ImageExtension {
    /**
     * alpha is between 0 and 1.
     */
    public static void SetAlpha(this Image image, float alpha) {
        Color temp = image.color;
        temp.a = alpha;
        image.color = temp;
    }

    public static float GetAlpha(this Image image) {
        return image.color.a;
    }

    public static bool IsVisible(this Image image) {
        return image.GetAlpha() > 0;
    }
}

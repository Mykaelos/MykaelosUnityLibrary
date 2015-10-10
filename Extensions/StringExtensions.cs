using UnityEngine;
using System.Collections;

public static class StringExtensions {

    public static Color HexAsColor(this string self) { // "RRGGBBAA"
        try {
            byte r = System.Convert.ToByte(self.Substring(0, 2), 16);
            byte g = System.Convert.ToByte(self.Substring(2, 2), 16);
            byte b = System.Convert.ToByte(self.Substring(4, 2), 16);
            byte a = self.Length == 8 ? System.Convert.ToByte(self.Substring(6, 2), 16) : (byte)255;

            return new Color32(r, g, b, a);
        }
        catch (System.Exception e) {
            Debug.LogError("Failed to parse: " + self + "\nError:" + e);
            return Color.white;
        }
    }
}

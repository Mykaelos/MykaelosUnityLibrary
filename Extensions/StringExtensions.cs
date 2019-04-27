using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class StringExtensions {

    public static Color HexAsColor(this string self) { // "#RRGGBBAA" or "RRGGBBAA" or "RRGGBB"
        try {
            int startIndex = self.IndexOf("#") == 0 ? 1 : 0; //looking for initial #

            byte r = System.Convert.ToByte(self.Substring(startIndex, 2), 16);
            byte g = System.Convert.ToByte(self.Substring(startIndex + 2, 2), 16);
            byte b = System.Convert.ToByte(self.Substring(startIndex + 4, 2), 16);
            byte a = self.Length > startIndex + 6 ? System.Convert.ToByte(self.Substring(startIndex + 6, 2), 16) : (byte)255;

            return new Color32(r, g, b, a);
        }
        catch (System.Exception e) {
            Debug.LogError("Failed to parse: " + self + "\nError:" + e);
            return Color.white;
        }
    }

    public static bool IsNullOrEmpty(this string self) {
        return string.IsNullOrEmpty(self);
    }

    public static List<string> SplitAndTrim(this string self, char splitChar = ',') {
        if (self == null || self.Length == 0) {
            Debug.Log("StringExtensions.SplitAndTrim: String was empty!");
            return new List<string>();
        }

        var list = new List<string>(self.Split(splitChar));
        for (int i = 0; i < list.Count; i++) {
            list[i] = list[i].Trim();
        }
        return list;
    }

    /**
     * Quick and easy formatting with a string literal.
     * For example: "I like you {0}".FormatWith("a lot!");
     */
    public static string FormatWith(this string self, params object[] args) {
        if (self == null) {
            return null;
        }
        return string.Format(self, args);
    }
}

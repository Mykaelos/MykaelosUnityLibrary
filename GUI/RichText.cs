using UnityEngine;
using System.Collections;

public class RichText {
    private static string ColorFormat = "<color={0}>{1}</color>";



    public static string Color(string text, string color) {
        return string.Format(ColorFormat, color, text);
    }
}

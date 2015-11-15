using UnityEngine;
//using System.Collections;

public static class ColorExtension {

    //alpha is between 0 and 1
    public static Color SetA(this Color color, float a) {
        //Color temp = new Color(color.r, color.g, color.b, a);
        Color temp = color;
        temp.a = a;
        color = temp;
        return color;
    }




}

//    public static void Hex(this Color color, string hexString) {
//        //hexString.Replace("#", "")
//        int hex = byte.Parse(hexString.Replace("#", ""), System.Globalization.NumberStyles.HexNumber);

//        color.
//    }

//    public static Color Hex(int HexVal) {
//        byte R = (byte)((HexVal >> 16) & 0xFF);
//        byte G = (byte)((HexVal >> 8) & 0xFF);
//        byte B = (byte)((HexVal) & 0xFF);
//        return new Color(R, G, B, 255);
//    }
//}

////public class Color {

////    public static Color Hex(int HexVal) {
////        byte R = (byte)((HexVal >> 16) & 0xFF);
////        byte G = (byte)((HexVal >> 8) & 0xFF);
////        byte B = (byte)((HexVal) & 0xFF);
////        return 
////    }
////}


//public class ColorX {




//}
using UnityEngine;
using System.Collections;

public static class RichTextExtension {
    private static string ColorFormat = "<color={0}>{1}</color>";
    private static string SizeFormat = "<size={0}>{1}</size>";
    private static string ItalicsFormat = "<i>{0}</i>";
    private static string BoldFormat = "<b>{0}</b>";


    public static string Color(this string self, string color) {
        return string.Format(ColorFormat, color, self);
    }

    public static string Size(this string self, int size) {
        return string.Format(SizeFormat, size, self);
    }

    public static string Italics(this string self) {
        return string.Format(ItalicsFormat, self);
    }

    public static string Bold(this string self) {
        return string.Format(BoldFormat, self);
    }

    public static string NewLine(this string self) {
        return string.Format("{0}\n", self);
    }
}

/** http://docs.unity3d.com/Manual/StyledText.html
Color name	                Hex value
aqua (same as cyan)	        #00ffffff
black	                    #000000ff
blue	                    #0000ffff
brown	                    #a52a2aff
cyan (same as aqua)	        #00ffffff
darkblue	                #0000a0ff
fuchsia (same as magenta)   #ff00ffff
green	                    #008000ff
grey	                    #808080ff
lightblue	                #add8e6ff
lime	                    #00ff00ff
magenta (same as fuchsia)   #ff00ffff
maroon	                    #800000ff
navy	                    #000080ff
olive	                    #808000ff
orange	                    #ffa500ff
purple	                    #800080ff
red	                        #ff0000ff
silver	                    #c0c0c0ff
teal	                    #008080ff
white	                    #ffffffff
yellow	                    #ffff00ff
*/

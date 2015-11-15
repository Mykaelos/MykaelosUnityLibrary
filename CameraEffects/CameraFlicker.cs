using UnityEngine;

public class CameraFlicker : MonoBehaviour {
    Texture2D CurrentTexture;
    Color CurrentColor;
    int DrawDepth = -1000; //forces the texture to be drawn last
    int Counter;


    public static void FlickerSingleFrame(Texture2D texture = null) {
        Instance.CurrentTexture = texture == null ? Texture2D.blackTexture : texture;
        Instance.CurrentColor = Color.white;
        Instance.Counter = 0;
    }

    //alpha is between 0 and 1
    public static void FlickerSingleFrame(Color color, float alpha) {
        Instance.CurrentTexture = Texture2D.whiteTexture;
        Instance.CurrentColor = color.SetA(alpha);
        Instance.Counter = 0;
    }

    static CameraFlicker _Instance = null;
    static CameraFlicker Instance {
        get { return _Instance == null ? _Instance = (new GameObject("CameraFlicker")).AddComponent<CameraFlicker>() : _Instance; }
    }

    void OnGUI() {
        if(Counter > 2) { //odd behaviour but we need to show atleast 2 frames to get the flicker to show up
            return;
        }
        Counter++;

        Color previousColor = GUI.color;
        int previousDepth = GUI.depth;

        GUI.color = CurrentColor;
        GUI.depth = DrawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), CurrentTexture);

        //Reset the previous values or it will affect other OnGUI calls.
        GUI.color = previousColor;
        GUI.depth = previousDepth;
    }
}

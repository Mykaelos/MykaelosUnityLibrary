using UnityEngine;

// This is a utility class to help me remember how LayerMasks work.
// https://docs.unity3d.com/Manual/Layers.html
public class LayerMaskM {


    public static int GetMaskExcept(params string[] ignoredLayerNames) {
        int layerMask = LayerMask.GetMask(ignoredLayerNames);
        return ~layerMask; // Invert mask.
    }
}

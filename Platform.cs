using UnityEngine;
using System.Collections;
using Enum.Extensions;

/*Utility for determining the current running platform.
 * TODO:
 * - Add other platform checks like windows vs mac
 * - Add boolean checks
 */

public class Platform {
    private static PlatformType _CurrentPlatform = PlatformType.NOTSET;
    public static PlatformType CurrentPlatform {
        get { return !_CurrentPlatform.Is(PlatformType.NOTSET) ? _CurrentPlatform : DeterminePlatform(); }
    }

    public static bool IsWeb {
        get { return CurrentPlatform.Is(PlatformType.WebGL) || CurrentPlatform.Is(PlatformType.WebPlayer); }
    }


    private static PlatformType DeterminePlatform() {
#if UNITY_STANDALONE
        _CurrentPlatform = PlatformType.Standalone;
#endif

#if UNITY_WEBGL
        _CurrentPlatform = PlatformType.WebGL;
#endif

#if UNITY_WEBPLAYER
        _CurrentPlatform = PlatformType.WebPlayer;
#endif

        return _CurrentPlatform;
    }
}

public enum PlatformType {
    NOTSET,
    Standalone,
    WebGL,
    WebPlayer
}

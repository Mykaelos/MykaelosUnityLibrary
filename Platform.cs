
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

    public static bool IsMobile {
        get { return CurrentPlatform.Is(PlatformType.Android) || CurrentPlatform.Is(PlatformType.iOS); }
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

#if (UNITY_ANDROID)
        _CurrentPlatform = PlatformType.Android;
#endif
#if (UNITY_IOS)
        _CurrentPlatform = PlatformType.iOS;
#endif

        return _CurrentPlatform;
    }
}

public enum PlatformType {
    NOTSET,
    Standalone,
    WebGL,
    WebPlayer,
    Android,
    iOS
}

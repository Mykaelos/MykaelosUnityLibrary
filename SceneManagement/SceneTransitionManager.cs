using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * SceneTransitionManager helps easily fade between scenes using TransitionManager.
 * Once SceneTransitionManager is setup, just use SceneTransitionManager.LoadScene() to jump to another scene.
 * 
 * SceneTransitionManager.LoadScene(sceneName)
 * 
 * SceneTransitionManager can be setup in 2 ways:
 * - Through code: SceneTransitionManager.Setup();
 *      Setup() should be called early in the scene so that the FadeIn can start immediately.
 *      Setup() default params override the public object properties.
 * 
 * - As a GameObject in the Scene:
 *      Add SceneTransitionManager to a GameObject in your first scene, setup the public properties, and it will live until the game is closed.
 *      If multiple SceneTransitionManagers exist (like one per scene), existing ones should automatically be deleted, and only the original one will remain.
 */
public class SceneTransitionManager : MonoBehaviour {
    public float FadeInWaitDelaySeconds = 0.1f;
    public float TransitionFadeDurationSeconds = 0.5f;
    public Color TransitionFadeColor = "262626".HexAsColor();
    public Action LoadingFinishedCallback;

    private bool HasStartedNextLevel = false;


    public static void Setup(float fadeInWaitDelaySeconds = 0.1f, float transitionFadeDuration = 0.5f, Color transitionFadeColor = new Color(/*Black*/), Action loadingFinishedCallback = null) {
        Instance.FadeInWaitDelaySeconds = fadeInWaitDelaySeconds;
        Instance.TransitionFadeDurationSeconds = transitionFadeDuration;
        Instance.TransitionFadeColor = transitionFadeColor;
        Instance.LoadingFinishedCallback += loadingFinishedCallback;
    }

    public static void LoadScene(string sceneName) {
        if (Instance.HasStartedNextLevel) {
            return;
        }
        Instance.HasStartedNextLevel = true;

        TransitionManager.FadeOut(Instance.TransitionFadeDurationSeconds, Instance.TransitionFadeColor, delegate {
            SceneManager.LoadScene(sceneName);
        });
    }

    #region Instance
    private static SceneTransitionManager _Instance = null;
    private static SceneTransitionManager Instance {
        get {
            if (_Instance == null) {
                _Instance = new GameObject("SceneTransitionManager").AddComponent<SceneTransitionManager>();
            }

            return _Instance;
        }
    }

    private void Awake() {
        if (_Instance != null && _Instance != this) { // Already exists, we're the duplicate.
            Destroy(this.gameObject);
            return;
        }

        _Instance = this;
        TransitionManager.SetFaded(true, TransitionFadeColor);
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        Instance.HasStartedNextLevel = false;

        // FadeIn
        WaitUntil.Seconds(FadeInWaitDelaySeconds, delegate { // Wait until most things are loaded.
            TransitionManager.FadeIn(TransitionFadeDurationSeconds, TransitionFadeColor, delegate {
                LoadingFinishedCallback?.Invoke();
            });
        });
    }

    private void OnDestroy() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        if (_Instance == this) {
            _Instance = null;
        }
    }
    #endregion Instance
}

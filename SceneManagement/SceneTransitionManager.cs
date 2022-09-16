using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour {
    private float TransitionFadeDuration = 0.17f;
    private Color TransitionFadeColor = "15574E".HexAsColor();
    private bool HasStartedNextLevel = false;

    private Action LoadingFinishedCallback;


    public static void Setup(Action loadingFinishedCallback = null) {
        Instance.LoadingFinishedCallback += loadingFinishedCallback;
    }

    private void Start() {
        // Wait until most things are loaded.
        WaitUntil.Seconds(0.1f, delegate {
            TransitionManager.FadeIn(TransitionFadeDuration, TransitionFadeColor, delegate {
                LoadingFinishedCallback?.Invoke();
            });
        });
    }

    public static void LoadScene(string sceneName) {
        if (Instance.HasStartedNextLevel) {
            return;
        }
        Instance.HasStartedNextLevel = true;

        TransitionManager.FadeOut(Instance.TransitionFadeDuration, Instance.TransitionFadeColor, delegate {
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
        _Instance = this;
        TransitionManager.SetFaded(true, TransitionFadeColor);
    }

    private void OnDestroy() {
        _Instance = null;
    }
    #endregion Instance
}

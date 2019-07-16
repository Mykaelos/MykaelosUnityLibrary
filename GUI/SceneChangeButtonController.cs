using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChangeButtonController : MonoBehaviour {
    public string DestinationSceneName;
    public Color FadeOutColor;
    public float FadeOutDuration = 1f;
    private bool LoadingNextScene = false;


    void Start() {
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick() {
        if (!LoadingNextScene) {
            LoadingNextScene = true;
            TransitionManager.FadeOut(FadeOutDuration, FadeOutColor, delegate {
                SceneManager.LoadScene(DestinationSceneName);
                LoadingNextScene = false;
            });
        }
    }
}

/* TODOs:
 * Find a way to FadeIn Automatically for the next scene, inside of this code.
 */

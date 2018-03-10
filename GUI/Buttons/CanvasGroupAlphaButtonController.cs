using UnityEngine;
using UnityEngine.UI;

public class CanvasGroupAlphaButtonController : MonoBehaviour {
    public GameObject CanvasGroupTarget;


    void Awake() {
        GetComponent<Button>().onClick.AddListener(OnToggle);
    }

    void OnDestroy() {
        GetComponent<Button>().onClick.RemoveListener(OnToggle);
    }

    public void OnToggle() {
        if (CanvasGroupTarget == null) {
            Debug.LogError("CanvasGroupTarget is not set!");
            return;
        }

        var canvasGroup = CanvasGroupTarget.GetComponent<CanvasGroup>();
        if (canvasGroup == null) {
            Debug.LogError("CanvasGroupTarget needs to have a CanvasGroup!");
            return;
        }

        canvasGroup.SetVisible(!canvasGroup.IsVisible());
    }
}

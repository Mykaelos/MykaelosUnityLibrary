using UnityEngine;
using System.Collections;

public class PanelSlider : MonoBehaviour {
    public Vector3 StartPosition;
    public Vector3 EndPosition;
    public float TransitionDuration = 0.5f;
    public string ToggleKey = "";

    private float CurrentProgress = 0.01f;
    private float Direction = -1;

    void Start() {
        Messenger.On(gameObject.name + ".PanelSlider.Toggle", MessageToggle);
    }

    void OnDestroy() {
        Messenger.Un(gameObject.name + ".PanelSlider.Toggle", MessageToggle);
    }

    void Update() {
        if (!string.IsNullOrEmpty(ToggleKey) && Input.GetKeyDown(ToggleKey)) {
            OnToggle();
        }
    }

    public void FixedUpdate() {
        if (CurrentProgress <= 0 && Direction == -1 || CurrentProgress >= TransitionDuration && Direction == 1) {
            return;
        }

        CurrentProgress += Direction * Time.deltaTime;
        transform.localPosition = Vector3.Lerp(StartPosition, EndPosition, LerpHelper.SmoothStep2(CurrentProgress / TransitionDuration));
    }

    public void OnToggle() {
        Direction *= -1;
        Messenger.Fire(gameObject.name + ".PanelSlider.AfterToggle", new object[] { Direction });
    }

    private void MessageToggle(object[] args) {
        if (args != null && args.Length == 1) {
            Direction = (int)args[0]; //direction, -1 or 1
        }
        else {
            OnToggle();
        }
    }
}

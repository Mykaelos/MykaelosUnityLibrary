using UnityEngine;
using UnityEngine.UI;

public class ToggleText : MonoBehaviour {
    public string OnString;
    public string OffString;
    public string ToggleKey;

    public System.Action<bool> ToggleCallback;

    private bool _IsOn = true;
    public bool IsOn {
        get { return _IsOn; }
        set { _IsOn = value; UpdateToggle(); }
    }

    private Text Text;


    protected virtual void Awake() {
        Text = GetComponent<Text>();
    }

    void Update() {
        if (!string.IsNullOrEmpty(ToggleKey) && Input.GetKeyDown(ToggleKey)) {
            Toggle();
        }
    }

    public void Toggle() {
        IsOn = !IsOn;
    }

    private void UpdateToggle() {
        Text.text = _IsOn ? OnString : OffString;
        ToggleCallback?.Invoke(_IsOn);
    }
}

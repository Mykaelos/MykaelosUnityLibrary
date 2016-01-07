using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToggleSprite : MonoBehaviour {
    public Sprite OnSprite;
    public Sprite OffSprite;
    public string ToggleKey;

    public System.Action<bool> ToggleCallback;

    private bool _IsOn = true;
    public bool IsOn {
        get { return _IsOn; }
        set { _IsOn = value; UpdateToggle(); }
    }

    private Image Image;


    protected virtual void Awake() {
        Image = GetComponent<Image>();
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
        Image.sprite = IsOn ? OnSprite : OffSprite;
        if (ToggleCallback != null) {
            ToggleCallback(IsOn);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class ToggleSprite : MonoBehaviour {
    public Sprite OnSprite;
    public Sprite OffSprite;
    public string ToggleKey;

    public System.Action<bool> ToggleCallback;

    private bool _IsOn = true;
    public bool IsOn {
        get { return _IsOn; }
        set {
            _IsOn = value;
            UpdateVisuals();
            ToggleCallback?.Invoke(_IsOn);
        }
    }

    private Image Image;


    protected virtual void Awake() {
        Image = GetComponent<Image>();
        UpdateVisuals();
    }

    protected void Update() {
        if (!string.IsNullOrEmpty(ToggleKey) && Input.GetKeyDown(ToggleKey)) {
            Toggle();
        }
    }

    public void Toggle() {
        IsOn = !IsOn;
    }

    private void UpdateVisuals() {
        if (Image != null) {
            Image.sprite = IsOn ? OnSprite : OffSprite;
        }
    }
}

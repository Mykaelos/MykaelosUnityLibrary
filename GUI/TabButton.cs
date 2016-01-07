using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TabButton : ToggleSprite {
    public List<GameObject> TabButtonGroup = new List<GameObject>();
    public GameObject TabPanel;
    public bool IsPrimary = false;

    private List<TabButton> LinkedButtons = new List<TabButton>();
    private CanvasGroup TabPanelGroup;


    protected override void Awake() {
        base.Awake();
        TabPanelGroup = TabPanel.GetComponent<CanvasGroup>();
        GetComponent<Button>().onClick.AddListener(() => OnClick());
    }

    void Start() {
        RegisterOtherTabButtons();

        if (IsPrimary) {
            OnClick();
        }
    }

    void RegisterOtherTabButtons() {
        foreach (var button in TabButtonGroup) {
            TabButton tabButton = button.GetComponent<TabButton>();
            if(!LinkedButtons.Contains(tabButton)) {
                LinkedButtons.Add(tabButton);
                tabButton.LinkedButtons.Add(this);
            }
        }
    }

    public void OnClick() {
        foreach (var button in LinkedButtons) {
            button.SetPanelVisible(false);
        }
        SetPanelVisible(true);
    }

    public void SetPanelVisible(bool isVisible) {
        TabPanelGroup.SetVisible(isVisible);
        IsOn = isVisible;
    }
}

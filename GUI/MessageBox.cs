using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
 * Panel (place this class here) REQUIRES CanvasGroup
 * |-Image (Background image of modal window)
 * |-Text (main text of the modal window)
 * |-Button (ok/close button)
 *   |-Image (Background image of button)
 *   |-Text (Button title)
 */

public delegate bool MessageBoxCallback(); //return true to close MessageBox

public class MessageBox : MonoBehaviour {
    private static MessageBox Self;

    private CanvasGroup Group;
    private Image PanelImage;
    private Text Text;
    private Button Button;
    private Image ButtonImage;
    private Text ButtonText;

    public void Awake() {
        Self = this;

        Group = GetComponent<CanvasGroup>();
        PanelImage = GetComponent<Image>();
        Text = transform.Find("Text").gameObject.GetComponent<Text>();
        Button = transform.Find("Button").gameObject.GetComponent<Button>();
        ButtonImage = transform.Find("Button").gameObject.GetComponent<Image>();
        ButtonText = transform.Find("Button/Text").gameObject.GetComponent<Text>();

        SetVisible(false);
    }

    public static void Style(Color panelColor, Color panelTextColor, Color buttonColor, Color buttonTextColor) {
        Self.PanelImage.color = panelColor;
        Self.Text.color = panelTextColor;
        Self.ButtonImage.color = buttonColor;
        Self.ButtonText.color = buttonTextColor;
    }

    public static void Show(string message, string buttontext, MessageBoxCallback callback) {
        Self.Text.text = message;
        Self.ButtonText.text = buttontext;

        Self.Button.onClick.RemoveAllListeners();
        Self.Button.onClick.AddListener(delegate {
            if (callback != null) {
                if (callback()) { //return true to close MessageBox
                    Self.SetVisible(false);
                }
            }
            else {
                Self.SetVisible(false); //if there is no callback, just close by default
            }
        });

        Self.SetVisible(true);
    }

    private void SetVisible(bool isVisible) {
        Group.alpha = isVisible ? 1 : 0;
        Group.blocksRaycasts = isVisible;
        Group.interactable = isVisible;
    }
}

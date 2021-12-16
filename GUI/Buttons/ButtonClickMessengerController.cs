using UnityEngine;
using UnityEngine.UI;

public class ButtonClickMessengerController : MonoBehaviour {
    public string OnClickMessage;


    private void Awake() {
        var button = this.GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked() {
        if (!OnClickMessage.IsNullOrEmpty()) {
            Messenger.Fire(OnClickMessage);
        }
    }
}

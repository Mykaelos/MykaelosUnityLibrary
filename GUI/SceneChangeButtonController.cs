using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChangeButtonController : MonoBehaviour {
    public string DestinationSceneName;
    private Button Button;


    void Start() {
        Button = GetComponent<Button>();
        Button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick() {
        SceneManager.LoadScene(DestinationSceneName);
    }
}

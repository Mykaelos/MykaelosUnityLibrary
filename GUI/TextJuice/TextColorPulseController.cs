using UnityEngine;
using UnityEngine.UI;

namespace MykaelosUnityLibrary.GUI.TextJuice {
    public class TextColorPulseController : MonoBehaviour {
        [Tooltip("Target Text Component. If not assigned, neighbor Text component will be used.")]
        public Text Text;
        [Tooltip("Target color that text component will fade to. Change the color's alpha for a faded effect. Starting color is the color of the text component.")]
        public Color TargetFadeColor = Color.white.SetA(64f/255f);
        [Tooltip("Duration of each pulse in seconds.")]
        public float PulseDuration = 1;

        private Color StartingColor;


        private void Start() {
            if (Text == null) {
                Text = GetComponent<Text>();
            }

            if (Text == null) {
                Debug.LogError("Text cannot be null. Either place TextColorPulseController next to Text Component or assign Text Object in inspector.");
            }

            StartingColor = Text.color;
        }

        private void Update() {
            Text.color = Color.Lerp(StartingColor, TargetFadeColor, LerpHelper.Reverse(LerpHelper.CurveToOneFastSlow(Mathf.PingPong(Time.time / PulseDuration, 1), 2)));
        }
    }
}
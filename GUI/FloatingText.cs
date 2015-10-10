using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour {
    private static GameObject FloatingTextPrefab;
    private Text Text;
    private CanvasGroup Group;
    private Rigidbody2D Rigidbody;
    private float Duration;//seconds


    public static void Create(Vector3 position, string text, Color color) {
        Create(position, Vector2.one, Vector2.up * 4, text, color, 1f);
    }

    public static void Create(Vector3 position, Vector2 size, Vector2 velocity, string text, Color color, float duration) {
        if (FloatingTextPrefab == null) {
            FloatingTextPrefab = Resources.Load<GameObject>("GUI/FloatingText");
        }

        GameObject prefab = (GameObject)GameObject.Instantiate(FloatingTextPrefab, position, Quaternion.identity);
        prefab.transform.localScale = size;
        prefab.GetComponent<FloatingText>().Setup(text, color, duration, velocity);
    }

    public void Setup(string text, Color color, float duration, Vector2 velocity) {
        Text.text = text;
        Text.color = color;
        Duration = duration;
        Rigidbody.velocity = velocity;

        StartCoroutine(Fade());
    }

    void Awake() {
        Transform canvas = transform.Find("Canvas");
        Group = canvas.GetComponent<CanvasGroup>();
        Text = canvas.Find("Text").GetComponent<Text>();
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    IEnumerator Fade() {
        Group.alpha = 1;
        float lastTime,
            currentDuration = 0,
            percentProgress;

        while (currentDuration < Duration) {
            lastTime = Time.time;
            yield return new WaitForSeconds(0.01f);
            currentDuration += (Time.time - lastTime);

            percentProgress = LerpHelper.CurveToZeroSlowFast(currentDuration / Duration, 5f);
            Group.alpha = percentProgress;
        }

        Group.alpha = 0;
        Destroy(this.gameObject);
    }
}

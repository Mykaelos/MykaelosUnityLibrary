using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraBorderFlasher : MonoBehaviour {
    static CameraBorderFlasher Self;
    static GameObject Prefab;
    Image Image;
    CanvasGroup CanvasGroup;
    float Duration = 0;


    public static void Flash(string prefabLocationAndName, float duration = 0.1f, Sprite sprite = null, Color? color = null, bool forceReloadPrefab = false) {
        if(Prefab == null || forceReloadPrefab) {
            Prefab = Resources.Load<GameObject>(prefabLocationAndName);
        }
        if(Self == null || forceReloadPrefab) {
            if(Self != null) {
                Destroy(Self.gameObject);//remove previous one
            }
            var flashObject = GameObject.Instantiate(Prefab);
            Self = flashObject.GetComponent<CameraBorderFlasher>();
        }

        Self.Setup(duration, sprite, color);
    }

    void Awake() {
        Image = transform.Find("Image").GetComponent<Image>();
        CanvasGroup = GetComponent<CanvasGroup>();
    }

    void Setup(float duration, Sprite sprite, Color? color = null) {
        if(sprite != null) { //otherwise use default sprite
            Image.sprite = sprite;
        }
        Image.color = color == null ? Color.white : (Color)color;
        Duration = duration;

        StartCoroutine(Show());
    }

    IEnumerator Show() {
        CanvasGroup.alpha = 1;
        yield return new WaitForSeconds(Duration);
        CanvasGroup.alpha = 0;
    }
}

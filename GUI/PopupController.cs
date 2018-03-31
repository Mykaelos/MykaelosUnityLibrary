using UnityEngine;
using UnityEngine.UI;

public class PopupController : MonoBehaviour {
    public float Border = 16;
    public float PushSpeed = 10;
    private float FadeDuration = 1;
    private float MaxLife = 5;

    private float LifeStarted = float.MaxValue - 10000;
    private float ParentBottom;//negative
    private float Height;
    private CanvasGroup Group;
    private Popup Parent;


    private float DurationRemaining {
        get { return LifeStarted + MaxLife - Time.time; }
    }

    public bool IsShowing {
        get { return DurationRemaining > 0; }
    }

    public void Configure(string message, Popup parent, float parentBottom, float duration = 5) {
        Parent = parent;
        Text text = transform.Find("Text").GetComponent<Text>();
        text.text = message;
        MaxLife = duration;

        ParentBottom = parentBottom;
        Height = text.preferredHeight + Border * 2f;
        RectTransformExtensions.SetHeight(((RectTransform)transform), Height);

        float lastPopupBottom = ParentBottom;
        if (parent.Popups.Count > 0) {
            GameObject lastPopup = parent.Popups[parent.Popups.Count - 1];
            RectTransform rectTrans = (RectTransform)lastPopup.transform;
            lastPopupBottom = rectTrans.localPosition.y - rectTrans.rect.height / 2f;
        }

        BoxCollider2D collider = this.GetComponent<BoxCollider2D>();
        collider.size = new Vector3(collider.size.x, Height + Border);
        transform.localPosition = new Vector3(0, lastPopupBottom - (Height + Border * 5f) / 2f, 0);

        //return transform.localPosition.y - Height / 2f;
    }

    private void Awake() {
        Group = this.GetComponent<CanvasGroup>();
        LifeStarted = Time.time;
    }

    private void FixedUpdate() {
        ConstrainPosition();
        Group.alpha = Mathf.Lerp(0, 1, (DurationRemaining >= FadeDuration ? 1 : DurationRemaining / FadeDuration));

        if (!IsShowing) {
            Destroy();
        }
    }

    private void ConstrainPosition() {
        if (transform.localPosition.y < ParentBottom + Height / 2f) {
            float newY = Mathf.Min(transform.localPosition.y + PushSpeed, ParentBottom + Height / 2f);
            transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            //if (transform.localPosition.y >= ParentBottom + Height / 2f) {//popup is on screen
            //    LifeStarted = Time.time;
            //}
        }
    }

    public void OnClick() {
        Destroy();
    }

    private void Destroy() {
        Parent.Remove(this.gameObject);
        Destroy(this.gameObject);
    }
}

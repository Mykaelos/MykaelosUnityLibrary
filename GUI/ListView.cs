using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public delegate void PrepareViewDelegate(GameObject view, int index);

public class ListView : MonoBehaviour {
    public GameObject ChildViewPrefab;
    public PrepareViewDelegate PrepareViewDelegate;
    public ScrollRect ParentScrollRect;
    public bool IsInScrollRect {
        get { return ParentScrollRect != null; }
    }

    public float ParentHeight;
    public float ChildViewHeight;

    private List<GameObject> ChildViews = new List<GameObject>();

    public void Initialize(GameObject childViewPrefab, PrepareViewDelegate prepareViewDelegate, int count) {
        ChildViewPrefab = childViewPrefab;
        PrepareViewDelegate = prepareViewDelegate;
        ParentScrollRect = this.GetComponentInParent<ScrollRect>();
        RectTransform ChildViewPrefabRect = (RectTransform)ChildViewPrefab.transform;
        ChildViewHeight = ChildViewPrefabRect.rect.height;
        ParentHeight = ((RectTransform)this.transform.parent).rect.height;

        foreach (Transform child in transform) {
            Destroy(child.gameObject); //remove all setup prefabs
        }

        Refresh(count);

        if (IsInScrollRect) {
            ParentScrollRect.verticalNormalizedPosition = 1;
        }
    }

    public void Refresh(int count) {
        foreach (var child in ChildViews) {
            Destroy(child);
        }
        ChildViews = new List<GameObject>();

        for (int i = 0; i < count; i++) {
            GameObject childView = (GameObject)GameObject.Instantiate(ChildViewPrefab);
            childView.transform.SetParent(this.transform, false);

            ChildViews.Add(childView);

            if (PrepareViewDelegate != null) {
                PrepareViewDelegate(childView, i);
            }
        }

        RecalculateLayout();
    }

    public void RecalculateLayout() {
        int i = 0;
        int count = ChildViews.Count;
        float menuHeight = Mathf.Max(ChildViewHeight * count, IsInScrollRect ? ParentHeight : 0);
        float startHeight = menuHeight / 2 - ChildViewHeight / 2;
        //Debug.Log("ParentHeight: " + ParentHeight);
        //Debug.Log("menuHeight: " + menuHeight);
        //Debug.Log("startHeight: " + startHeight);

        RectTransformExtensions.SetHeight((RectTransform)transform, menuHeight);

        foreach (var view in ChildViews) {
            ((RectTransform)view.transform).localPosition = new Vector3(0, startHeight - ChildViewHeight * i, 0);
            i++;
        }
    }
}

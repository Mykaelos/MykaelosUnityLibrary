using UnityEngine;
using System.Collections;

public class FadeCanvasGroupLink : EventLink {
    public const float FadeOut = -1; //fade to no alpha=0
    public const float FadeIn = 1; //fade to full alpha=1

    private CanvasGroup Group;
    private float Direction;
    private float Duration;


    public FadeCanvasGroupLink(CanvasGroup group, float direction, float duration) {
        Group = group;
        Direction = direction;
        Duration = duration;
    }

    public override void Trigger(EventChain parent, System.Action finishedCallback) {
        parent.StartCoroutine(Fade(finishedCallback));
    }

    IEnumerator Fade(System.Action finishedCallback) {
        float currentDuration = (Direction == FadeIn ? 0f : 1f) * Duration; //the opposite of the duration
        SetVisibility(Direction != FadeIn);

        while(!IsFinished()) {
            yield return new WaitForSeconds(0.01f);
            if (Group == null) { //preventing NPE
                break;
            }
            currentDuration += Direction * Time.deltaTime;
            Group.alpha = Mathf.Clamp01(currentDuration / Duration);
        }

        SetVisibility(Direction == FadeIn);

        if (finishedCallback != null) {
            finishedCallback();
        }
    }

    bool IsFinished() {
        return Direction == FadeIn ? Group.alpha == 1 : Group.alpha == 0;
    }

    void SetVisibility(bool isVisible) {
        Group.alpha = (float)System.Convert.ToInt32(isVisible);
        Group.interactable = isVisible;
        Group.blocksRaycasts = isVisible;
    }
}

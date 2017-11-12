using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSoundEffectController : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler {
    public AudioClip MouseEnterAudioClip;
    public AudioClip MouseClickAudioClip;


    public void OnPointerEnter(PointerEventData eventData) {
        SoundEffectManager.PlaySound(transform, MouseEnterAudioClip, Random.Range(0.9f, 1.1f), 1f);
    }

    public void OnPointerClick(PointerEventData eventData) {
        SoundEffectManager.PlaySound(transform, MouseClickAudioClip, 1f, 1f);
    }
}

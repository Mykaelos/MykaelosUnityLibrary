using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSoundEffectController : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler {
    public string MouseEnterAudioName;
    public string MouseClickAudioName;


    public void OnPointerEnter(PointerEventData eventData) {
        AudioManager.PlaySound(MouseEnterAudioName, Random.Range(0.9f, 1.1f), 1f, true);
    }

    public void OnPointerClick(PointerEventData eventData) {
        AudioManager.PlaySound(MouseClickAudioName, 1f, 1f, true);
    }
}

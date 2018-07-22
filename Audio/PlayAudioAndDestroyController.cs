using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayAudioAndDestroyController : MonoBehaviour {
    public float PitchMin = 1f;
    public float PitchMax = 1f;
    public float Volume = 1f;


    private void Start() {
        var audioSource = GetComponent<AudioSource>();
        AudioManager.PlaySound(audioSource, Random.Range(PitchMin, PitchMax), Volume);
        Destroy(gameObject, audioSource.clip.length);
    }
}

using System.Collections.Generic;
using UnityEngine;

/**
 * AudioRandomizer plays a random clip/pitch on Awake, via a local AudioSource on the same object.
 * Useful for one-shot short-lived effects, like bullet impacts or hit particle effects.
 * 
 * If there are AudioClip linked, it will randomly play once of those, or it will try to play the
 * AudioClip in the local AudioSource.
 */
[RequireComponent(typeof(AudioSource))]
public class AudioRandomizer : MonoBehaviour {
    public List<AudioClip> AudioClips = new List<AudioClip>();

    public bool IsPitchRandom = true;
    [Range(0.0f, 2.0f)]
    public float PitchMinimum = 0.9f;
    [Range(0.0f, 2.0f)]
    public float PitchMaximum = 1.1f;


    private void Awake() {
        var audioSource = GetComponent<AudioSource>();

        if (AudioClips.Count > 0) {
            audioSource.clip = AudioClips.RandomElement();
        }

        if (IsPitchRandom) {
            audioSource.pitch = Random.Range(PitchMinimum, PitchMaximum);
        }

        audioSource.Play();
    }
}

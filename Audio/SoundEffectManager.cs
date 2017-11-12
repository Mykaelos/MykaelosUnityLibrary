using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour {

    public static AudioSource PlaySound(Transform parent, AudioClip audioClip, float pitch = 1, float volumeMultiplier = 1) {
        AudioSource audioSource = GetOrCreateAudioSource(parent, audioClip.name);

        audioSource.clip = audioClip;
        audioSource.loop = false;
        audioSource.volume = volumeMultiplier;
        audioSource.pitch = pitch;
        audioSource.Play();

        return audioSource;
    }

    public static AudioSource GetOrCreateAudioSource(Transform parent, string audioClipName) {
        Transform child = parent.Find(audioClipName);
        if (child == null) {
            child = new GameObject(audioClipName).transform;
            child.SetParent(parent);
            child.SetPositionAndRotation(parent.position, Quaternion.identity);
        }

        AudioSource audioSource = child.GetComponent<AudioSource>();
        if (audioSource == null) {
            audioSource = child.gameObject.AddComponent<AudioSource>();
        }

        return audioSource;
    }
}

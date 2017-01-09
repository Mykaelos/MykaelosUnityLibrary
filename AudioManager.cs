using UnityEngine;
using System.Collections.Generic;

public class AudioManager {
    private static Dictionary<string, AudioClip> AudioClips = new Dictionary<string, AudioClip>();
    
    #region Privates
    private static GameObject _MusicObject;
    private static AudioSource _MusicSource;
    private static AudioSource MusicSource {
        get { return _MusicSource != null ? _MusicSource : GenerateMusicSource(); }
    }

    private static GameObject _TempAudioSourcePrefab;
    private static GameObject TempAudioSourcePrefab {
        get { return _TempAudioSourcePrefab != null ? _TempAudioSourcePrefab : GenerateTempAudioSourcePrefab(); }
    }
    #endregion

    public static bool IsMusicMuted {
        get { return PlayerPrefs.HasKey("IsMusicMuted") && PlayerPrefs.GetInt("IsMusicMuted") == 1; }
        set { PlayerPrefs.SetInt("IsMusicMuted", value ? 1 : 0); MusicSource.mute = value; }
    }
    public static bool IsSoundMuted {
        get { return PlayerPrefs.HasKey("IsSoundMuted") && PlayerPrefs.GetInt("IsSoundMuted") == 1; }
        set { PlayerPrefs.SetInt("IsSoundMuted", value ? 1 : 0); }
    }
    public static float MusicVolume = 0.5f;
    public static float SoundVolume = 0.6f;


    public static void SetTracks(Dictionary<string, string> tracks) {
        AudioClips = new Dictionary<string, AudioClip>();
        foreach (var track in tracks) {
            AudioClips.Add(track.Key, Resources.Load<AudioClip>(track.Value));
        }
    }

    public static void AddTracks(Dictionary<string, string> tracks) {
        AudioClips = AudioClips ?? new Dictionary<string, AudioClip>();
        foreach (var track in tracks) {
            if(!AudioClips.ContainsKey(track.Key)) {
                AudioClips.Add(track.Key, Resources.Load<AudioClip>(track.Value));
            }
        }
    }

    public static void PlayMusic(string audioName, bool repeat, bool restart) {
        PlayMusic(audioName, MusicSource, repeat, restart);
    }

    public static void PlayMusic(string audioName, AudioSource source, bool repeat, bool restart) {
        AudioClip audioClip = GetOrLoadClip(audioName);
        if (audioClip == null) {
            Debug.Log("AudioManager: Could not find clip \"" + audioName + "\"");
            return;
        }

        AudioSource musicSource = source ?? MusicSource;
        musicSource.loop = repeat;
        musicSource.mute = IsMusicMuted;
        musicSource.volume = MusicVolume;
        musicSource.pitch = 1f;

        if (!restart && musicSource.isPlaying && musicSource.clip != null && musicSource.clip.name.Equals(audioClip.name)) {
            return;
        }
        musicSource.clip = audioClip;
        musicSource.Play();
    }

    public static void PlaySound(string audioName, float pitch = 1, float volumeMultiplier = 1, bool dontDestoryOnLoad = false) {
        PlaySound(audioName, Camera.main.transform.position, pitch, volumeMultiplier, dontDestoryOnLoad);
    }

    public static void PlaySound(string audioName, Vector3 position, float pitch = 1, float volumeMultiplier = 1, bool dontDestoryOnLoad = false) {
        AudioClip audioClip = GetOrLoadClip(audioName);
        if(audioClip == null) {
            Debug.Log("AudioManager: Could not find clip \"" + audioName + "\"");
            return;
        }
        
        if (!IsSoundMuted) {
            GameObject tempObject = (GameObject)GameObject.Instantiate(TempAudioSourcePrefab, position, Quaternion.identity);
            if(dontDestoryOnLoad) {
                GameObject.DontDestroyOnLoad(tempObject);
            }
            PlaySound(audioName, tempObject.GetComponent<AudioSource>(), pitch, volumeMultiplier);
            GameObject.Destroy(tempObject, audioClip.length);
        }
    }

    public static void PlaySound(string audioName, AudioSource source, float pitch = 1, float volumeMultiplier = 1) {
        AudioClip audioClip = GetOrLoadClip(audioName);
        if (audioClip == null) {
            Debug.Log("AudioManager: Could not find clip \"" + audioName + "\"");
            return;
        }

        if (!IsSoundMuted) {
            source.clip = audioClip;
            source.loop = false;
            source.mute = IsSoundMuted;
            source.volume = SoundVolume * volumeMultiplier;
            source.pitch = pitch;
            source.Play();
        }
    }

    public static void PlaySound(AudioSource source, float pitch = 1, float volumeMultiplier = 1) {
        if (!IsSoundMuted) {
            source.loop = false;
            source.mute = IsSoundMuted;
            source.volume = SoundVolume * volumeMultiplier;
            source.pitch = pitch;
            source.Play();
        }
    }

    public static void StopMusic(AudioSource source = null) {
        AudioSource musicSource = source ?? MusicSource;
        musicSource.Stop();
    }

    public static void StopSound(AudioSource source) {
        source.Stop();
    }

    public static void SetMusicVolume(float volume) {
        MusicVolume = volume;
        MusicSource.volume = MusicVolume;
    }

    public static void SetSoundVolume(float volume) {
        SoundVolume = volume;
    }

    public static void ToggleMusicMute() {
        IsMusicMuted = !IsMusicMuted;
    }

    public static void ToggleSoundMute() {
        IsSoundMuted = !IsSoundMuted;
    }

    private static AudioClip GetOrLoadClip(string audioName) {
        AudioClip clip = AudioClips.Get(audioName);
        if (clip == null) {
            clip = Resources.Load<AudioClip>(audioName);
            AudioClips.Add(audioName, clip);
        }
        return clip;
    }

    private static AudioSource GenerateMusicSource() {
        if (_MusicObject == null) {
            _MusicObject = new GameObject("MusicSource");
            _MusicSource = _MusicObject.AddComponent<AudioSource>();
            GameObject.DontDestroyOnLoad(_MusicObject);
        }
        else if (MusicSource == null) {
            _MusicSource = _MusicObject.GetComponent<AudioSource>();
        }
        return _MusicSource;
    }

    private static GameObject GenerateTempAudioSourcePrefab() {
        _TempAudioSourcePrefab = new GameObject("TempAudioSource");
        _TempAudioSourcePrefab.AddComponent<AudioSource>();
        return _TempAudioSourcePrefab;
    }
}

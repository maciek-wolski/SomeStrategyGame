using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
#region singleton
    public static SoundManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
#endregion

    [SerializeField] private AudioSource musicSource, effectSource;

    public void PlaySound(AudioClip clip) {
        effectSource.PlayOneShot(clip);
    }

    public void ChangeMasterVolume(float value) {
        AudioListener.volume = value;
    }

    public void ToggleEffects() {
        effectSource.mute = !effectSource.mute;
    }

    public void ToggleMusic() {
        musicSource.mute = !musicSource.mute;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnDestroy : MonoBehaviour
{
    [SerializeField] private AudioClip clip = null;

    private void OnDestroy() {
        if (SoundManager.instance != null) { 
            SoundManager.instance.PlaySound(clip);
        }
    }
}

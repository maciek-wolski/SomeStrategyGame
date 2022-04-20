using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnDestroy : MonoBehaviour
{
    [SerializeField] private AudioClip clip = null;
    private Camera mainCamera;

    private void Awake() {
        mainCamera = Camera.main;
    }
    private void OnDestroy() {
        if (SoundManager.instance != null && IsVisibleToMainCamera()) { 
            SoundManager.instance.PlaySound(clip);
        }
    }

    private bool IsVisibleToMainCamera() {
        Vector3 viewPos = mainCamera.WorldToViewportPoint(gameObject.transform.position);
        if (viewPos.x >= 0 && viewPos.x <= 1 
            && viewPos.y >= 0 && viewPos.y <= 1
            && viewPos.z > 0) {
                
            return true;
        }

        return false;
    }
}

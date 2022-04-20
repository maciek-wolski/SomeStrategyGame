using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnStart : MonoBehaviour
{
    [SerializeField] private AudioClip clip;

    Camera mainCamera;

    private void Awake() {
        mainCamera = Camera.main;
    }

    private void Start() {
        if (IsVisibleToMainCamera())
            SoundManager.instance.PlaySound(clip);
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

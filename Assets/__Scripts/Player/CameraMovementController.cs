using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovementController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform = null;
    [SerializeField] private float cameraMovementSpeed = 20.0f;
    [SerializeField] private Vector2 screenXLimits = Vector2.zero;
    [SerializeField] private Vector2 screenZLimits = Vector2.zero;

    private Controls controls;
    private Vector2 previousInput;

    private void OnEnable() {
        controls = new Controls();
        controls.Player.MoveCamera.performed += SetPreviousInput;
        controls.Player.MoveCamera.canceled += SetPreviousInput;
        controls.Enable();
    }

    private void OnDisable() {
        controls.Player.MoveCamera.performed -= SetPreviousInput;
        controls.Player.MoveCamera.canceled -= SetPreviousInput;
        controls.Disable();
    }

    private void SetPreviousInput(InputAction.CallbackContext ctx)
    {
        previousInput = ctx.ReadValue<Vector2>();
    }

    private void Update() {
        Vector3 currentPosition = transform.position;
        if (previousInput != Vector2.zero) {
            //-previous input because of player object rotation on y axis
            currentPosition += new Vector3(-previousInput.x, 0f, -previousInput.y) * cameraMovementSpeed * Time.deltaTime;
            currentPosition.x = Mathf.Clamp(currentPosition.x, screenXLimits.x, screenXLimits.y);
            currentPosition.z = Mathf.Clamp(currentPosition.z, screenZLimits.x, screenZLimits.y);
        }
        transform.position = currentPosition;
    }
}

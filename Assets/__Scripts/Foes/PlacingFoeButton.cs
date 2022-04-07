using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlacingFoeButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private LayerMask floorLayer = new LayerMask();
    [SerializeField] private TMP_Text priceText = null;

    private Camera mainCamera = null;
    private PlayerManager playerManager = null;
    private GameObject currentFoePreview = null;
    private Foe foe = null;

#region Setters
    public void SetPriceText(string newPriceText){
        priceText.text = $"{newPriceText}";
    }
    public void SetFoe(Foe newFoe){
        foe = newFoe;
    }
#endregion

    private void Start() {
        mainCamera = Camera.main;
        playerManager = PlayerManager.instance;
    }

    private void Update() {
        if (currentFoePreview == null) { return; }
        UpdatePreviewPos();
    }

    private void UpdatePreviewPos()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (!Physics.Raycast(ray, out RaycastHit hitPoint, Mathf.Infinity, floorLayer)) { return; }
        if (!currentFoePreview.activeSelf) { currentFoePreview.SetActive(true); }
        currentFoePreview.transform.position = hitPoint.point;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //leftmouse button for buying - placing foe
        if (eventData.button != PointerEventData.InputButton.Left) { return; }
        if (foe.GetFoePrice() > playerManager.GetPlayerMoney()) { return; }
        currentFoePreview = Instantiate(foe.GetFoePreview());
        currentFoePreview.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (currentFoePreview == null) { return; }
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hitPoint, Mathf.Infinity, floorLayer)){
            playerManager.SpawnFoe(foe.GetFoeId(), hitPoint.point);
        }
        Destroy(currentFoePreview);
    }
}

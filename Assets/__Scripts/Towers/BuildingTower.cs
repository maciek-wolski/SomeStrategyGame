using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class BuildingTower : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private LayerMask buildingSpotLayer = new LayerMask();
    [SerializeField] TowersManager towersManager = null;
    [SerializeField] GameObject towerPlacementUI = null;
    [SerializeField] private int ownerId = -1;
    [SerializeField] private GameObject currentlyPlacedTower = null;
    private PlayerManager playerManager;
    private Camera mainCamera;

#region Getters
    public GameObject GetCurrentlyPlacedTower(){
        return currentlyPlacedTower;
    }
#endregion

#region Setters
    public void SetCurrentlyPlacedTower(GameObject newTower){
        currentlyPlacedTower = newTower;
    }
#endregion

    private void Start() {
        playerManager = PlayerManager.instance;
        mainCamera = Camera.main;
    }

    //checking if player pressed lmb over building spot
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) { return; }
    }
    //releasing handle
    public void OnPointerUp(PointerEventData eventData)
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, buildingSpotLayer)){
            if (currentlyPlacedTower != null) { 
                Health towerHealth = currentlyPlacedTower.GetComponent<Health>();
                if (towerHealth.GetCurrentHealth() <= 0){
                    Destroy(currentlyPlacedTower);
                    currentlyPlacedTower = null;
                }
                else { return; }
            }
            towerPlacementUI.SetActive(true);
            towersManager.SetCurrBuildingTowerFor(this);
        }
    }

    public void ReceivedBuildTowerCommand(GameObject towerObject){
        Tower tower = towerObject.GetComponent<Tower>();
        if (playerManager.GetMyMoney() < tower.GetTowerPrice()) { return; }
        playerManager.ReduceMyMoney(tower.GetTowerPrice());
        towerObject.GetComponent<Health>().SetOwnerId(ownerId);
        currentlyPlacedTower = Instantiate(towerObject, gameObject.transform.position, towerObject.transform.rotation);
    }
}

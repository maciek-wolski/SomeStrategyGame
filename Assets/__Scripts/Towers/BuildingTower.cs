using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingTower : MonoBehaviour
{
    [SerializeField] TowersManager towersManager = null;
    [SerializeField] GameObject towerPlacementUI = null;
    [SerializeField] private int ownerId = -1;
    [SerializeField] private GameObject currentlyPlacedTower = null;
    private PlayerManager playerManager;

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
    }
    private void OnMouseOver() {
        if (playerManager.GetPlayerID() != ownerId){ return; }
        //detecting mouse click
        if (Mouse.current.leftButton.wasPressedThisFrame){
            //cant place tower on already taken spot by healthy tower           
            if (currentlyPlacedTower != null){
                Health towerHealth = currentlyPlacedTower.GetComponent<Health>();
                Debug.Log($"Tower health= {towerHealth.GetCurrentHealth()}/{towerHealth.GetMaxHealth()}");
                if (towerHealth.GetCurrentHealth() <= 0){
                    //tower health got to 0, clean this spot for new tower.
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
        towerObject.GetComponent<Tower>().SetOwnerId(ownerId);
        currentlyPlacedTower = Instantiate(towerObject, gameObject.transform.position, towerObject.transform.rotation);
    }
}

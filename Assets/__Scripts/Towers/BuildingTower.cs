using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingTower : MonoBehaviour
{
    [SerializeField] TowersManager towersManager = null;
    [SerializeField] GameObject towerPlacementUI = null;
    [SerializeField] private int ownerId = -1;
    private Tower currentlyPlacedTower = null;

#region Getters
    public Tower GetCurrentlyPlacedTower(){
        return currentlyPlacedTower;
    }
#endregion

#region Setters
    public void SetCurrentlyPlacedTower(Tower newTower){
        currentlyPlacedTower = newTower;
    }
#endregion

    private void OnMouseOver() {
        //detecting mouse click
        if (Mouse.current.leftButton.wasPressedThisFrame){
            //cant place tower on already taken spot
            if (currentlyPlacedTower != null) { return; }
            towerPlacementUI.SetActive(true);
            towersManager.SetCurrBuildingTowerFor(this);
        }
    }

    public void ReceivedBuildTowerCommand(GameObject tower){
        Debug.Log($"Received tower {tower.gameObject.name} to place");
    }
}

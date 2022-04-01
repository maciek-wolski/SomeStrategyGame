using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowersManager : MonoBehaviour
{
    [Tooltip("Max 3 tower prefabs!")]
    [SerializeField] private List<GameObject> towerPrefabs = new List<GameObject>(3);
    [SerializeField] private GameObject towerPlacementUI = null;
    [SerializeField] private BuildingTower currBuildingTowerFor = null;

#region Getters
    public List<GameObject> GetTowerPrefabs(){
        return towerPrefabs;
    }
    public BuildingTower GetCurrBuildingTowerFor(){
        return currBuildingTowerFor;
    }
#endregion
#region Setters
    public void SetCurrBuildingTowerFor(BuildingTower newBT){
        if (currBuildingTowerFor != null) { return; }
        currBuildingTowerFor = newBT;
    }
#endregion

    private void Start() {
        if (towerPrefabs.Count != 3) { throw new Exception("You have to put 3 tower prefabs into the list"); }
    }

#region ButtonClicks
    public void ButtonCloseClick(){
        currBuildingTowerFor = null;
    }
    public void ButtonBuildTower(HoldingTower holdingTower){
        GameObject tower = holdingTower.GetHoldingTower();
        currBuildingTowerFor.ReceivedBuildTowerCommand(tower);
        currBuildingTowerFor = null;
        towerPlacementUI.SetActive(false);
    }
#endregion


}

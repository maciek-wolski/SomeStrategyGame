using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingTower : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab;


    public GameObject GetHoldingTower(){
        return towerPrefab;
    }

    public void SetHoldingTower(GameObject newHoldingTower){
        towerPrefab = newHoldingTower;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [SerializeField] private int id;
    [SerializeField] private List<Foe> foePrefabs = new List<Foe>();
    [SerializeField] private List<Foe> myFoes = new List<Foe>();
    [SerializeField] private List<Tower> myTowers = new List<Tower>();
    [SerializeField] private Transform targetPoint = null;
    [SerializeField] private float myMoney = 100.0f;
    [SerializeField] private TMP_Text resourceInfoText = null;

    
#region Getters
    public int GetPlayerID(){
        return id;
    }
    public List<Foe> GetMyFoes(){
        return myFoes;
    }
    public float GetPlayerMoney(){
        return myMoney;
    }

#endregion
#region Setters
#endregion

    private void Awake() {
        instance = this;
    }
    private void OnEnable() {
        resourceInfoText.text = $"{myMoney}";
        Foe.OnFoeDestroy += HandleOnFoeDestroy;
        Foe.OnFoeSpawned += HandleOnFoeSpawned;
        Tower.OnTowerSetOwnerId += HandleOnTowerSpawned;
        Tower.OnTowerDestroyed += HandleOnTowerDestroyed;
    }
    private void OnDisable() {
        Foe.OnFoeDestroy -= HandleOnFoeDestroy;
        Foe.OnFoeSpawned -= HandleOnFoeSpawned;
        Tower.OnTowerSetOwnerId -= HandleOnTowerSpawned;
        Tower.OnTowerDestroyed -= HandleOnTowerDestroyed;
    }

#region customEvents
    private void HandleOnFoeSpawned(Foe foe)
    {
        if (foe.GetFoeHealth().GetOwnerId() != id) { return; }
        myFoes.Add(foe);
    }

    private void HandleOnFoeDestroy(Foe foe)
    {
        if (foe.GetFoeHealth().GetOwnerId() != id) { return; }        
        myFoes.Remove(foe);
    }

    private void HandleOnTowerDestroyed(Tower tower)
    {
        if (tower.GetTowerHealth().GetOwnerId() != id) { return; }
        myTowers.Remove(tower);
    }

    private void HandleOnTowerSpawned(Tower tower)
    {
        if (tower.GetTowerHealth().GetOwnerId() != id) { return; }
        myTowers.Add(tower);
    }
#endregion

    public void SpawnFoe(int foeIdToSpawn, Vector3 spawnPoint){
        //find foe in prefab list
        Foe foeToSpawn = foePrefabs.Find(x => x.GetFoeId() == foeIdToSpawn);
        //check if player has enough money
        if (myMoney < foeToSpawn.GetFoePrice()) { return; }
        ReduceMyMoney(foeToSpawn.GetFoePrice());
        //make instance of new object and set its ownerId and target point
        GameObject newFoeObject = foeToSpawn.gameObject;
        Health newFoeHealthScript = newFoeObject.GetComponent<Health>();
        newFoeHealthScript.SetOwnerId(GetPlayerID());
        FoeMovement newFoeMovementScript = newFoeObject.GetComponent<FoeMovement>();
        newFoeMovementScript.SetTargetPoint(targetPoint);
        Instantiate(newFoeObject, spawnPoint, newFoeObject.transform.rotation);
    }

    public void ReduceMyMoney(float reduceValue){
        myMoney -= reduceValue;
        resourceInfoText.text = $"{myMoney}";
    }
    public void AddMoney(float amount) {
        myMoney += amount;
        resourceInfoText.text = $"{myMoney}";
    }

}

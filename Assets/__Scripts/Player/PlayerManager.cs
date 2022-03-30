using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private List<Foe> foePrefabs = new List<Foe>();
    [SerializeField] private List<Foe> myFoes = new List<Foe>();
    [SerializeField] private Transform targetPoint = null;
    [Tooltip("Max 2\n 1 - bottom left corner\n 2 - top right corner")]
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>(2);
    [SerializeField] private float myMoney = 100.0f;

    
#region Getters
    public int GetPlayerID(){
        return id;
    }
    public List<Foe> GetMyFoes(){
        return myFoes;
    }
    public float GetMyMoney(){
        return myMoney;
    }

#endregion
#region Setters
#endregion

    private void OnEnable() {
        if (spawnPoints.Count != 2) { 
            throw new Exception($"Wrong allocation\n read --spawnPoints-- property description"); 
        }
        Foe.OnFoeDestroy += HandleOnFoeDestroy;
        Foe.OnFoeSpawned += HandleOnFoeSpawned;
    }
    private void OnDisable() {
        Foe.OnFoeDestroy -= HandleOnFoeDestroy;
        Foe.OnFoeSpawned -= HandleOnFoeSpawned;
    }

    private void HandleOnFoeSpawned(Foe foe)
    {
        if (foe.GetOwnerId() != id) { return; }
        myFoes.Add(foe);
    }

    private void HandleOnFoeDestroy(Foe foe)
    {
        if (foe.GetOwnerId() != id) { return; }        
        myFoes.Remove(foe);
    }

    public void SpawnFoe(int foeIdToSpawn)
    {
        //find foe in prefab list
        Foe foeToSpawn = foePrefabs.Find(x => x.GetFoeId() == foeIdToSpawn);
        //check if player has enough money
        if (myMoney < foeToSpawn.GetFoePrice()) { return; }
        //make instance of new object and set its ownerId and target point
        GameObject newFoeObject = foeToSpawn.gameObject;
        Foe newFoeScript = newFoeObject.GetComponent<Foe>();
        newFoeScript.SetOwnerId(GetPlayerID());
        FoeMovement newFoeMovementScript = newFoeObject.GetComponent<FoeMovement>();
        newFoeMovementScript.SetTargetPoint(targetPoint);
        //calculate instantiate position
        float minx = spawnPoints[1].position.x, maxx = spawnPoints[0].position.x;
        float minz = spawnPoints[1].position.z, maxz = spawnPoints[0].position.z;
        Vector3 newSpawnPoint = new Vector3(
            UnityEngine.Random.Range(minx, maxx),
            0f,
            UnityEngine.Random.Range(minz, maxz)
        );
        Instantiate(newFoeObject, newSpawnPoint, newFoeObject.gameObject.transform.rotation);
    }

    public void ReduceMyMoney(float reduceValue){
        myMoney -= reduceValue;
    }

}

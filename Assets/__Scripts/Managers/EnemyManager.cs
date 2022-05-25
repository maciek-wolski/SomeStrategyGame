using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private bool canChange = true;
    private int currentWave = 0;
    [SerializeField] private int maxWaves = 10;
    [SerializeField] private float coolDownBetweenWaves = 20.0f;
    [SerializeField] private int id = -1;
    [SerializeField] private List<Foe> foePrefabs = new List<Foe>();
    // [SerializeField] private List<Foe> myFoes = new List<Foe>();
    // [SerializeField] private List<Tower> myTowers = new List<Tower>();
    [SerializeField] private Transform targetPoint = null;
    [Tooltip("Max 2\n 0 - bottom corner\n 1 - top corner")]
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>(2);
    private List<Foe> foeToSpawn = new List<Foe>();
    [SerializeField] private List<Foe> myFoes = new List<Foe>();

#region Getters
    public int GetEnemyId(){
        return id;
    }
#endregion

    private void OnEnable() {
        if (spawnPoints.Count != 2) { 
            throw new Exception($"Wrong allocation\n read --spawnPoints-- property description"); 
        }

        Foe.OnFoeSpawned += HandleOnFoeSpawned;
        Foe.OnFoeDestroy += HandleOnFoeDestroyed;
    }
    private void OnDisable() {
        Foe.OnFoeSpawned -= HandleOnFoeSpawned;
        Foe.OnFoeDestroy -= HandleOnFoeDestroyed;
    }

    private void HandleOnFoeSpawned(Foe foe)
    {
        if (foe.GetFoeHealth().GetOwnerId() != id) { return; }
        myFoes.Add(foe);
    }
    private void HandleOnFoeDestroyed(Foe foe)
    {
        if (foe.GetFoeHealth().GetOwnerId() != id) { return; }
        myFoes.Remove(foe);
    }

    private void Update() {
        if (canChange){
            if (currentWave >= maxWaves) { 
                canChange = false;
                return; 
            }
            WaveIdChanged();
            StartCoroutine(waveCallsCoroutine());
        }
    }

    private IEnumerator waveCallsCoroutine() {
        canChange = false;
        currentWave++;
        yield return new WaitForSeconds(coolDownBetweenWaves);
        canChange = true;
    }

    private void SpawnFoe(int foeIdToSpawn) {
        //find foe in prefab list
        Foe foeToSpawn = foePrefabs.Find(x => x.GetFoeId() == foeIdToSpawn);
        //make instance of new object and set its ownerId and target point
        GameObject newFoeObject = foeToSpawn.gameObject;
        Health newFoeHealthScript = newFoeObject.GetComponent<Health>();
        newFoeHealthScript.SetOwnerId(GetEnemyId());
        FoeMovement newFoeMovementScript = newFoeObject.GetComponent<FoeMovement>();
        newFoeMovementScript.targetPoint = targetPoint;
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
    private void SpawnTower(int towerIdToSpawn, Transform towerSpawnPoint) {

    }

    private void WaveIdChanged() {
        int currWave = currentWave;
        int spawn_foe_01 = 3 + currWave;
        ManageSpawning(spawn_foe_01, foePrefabs[0]);
        int spawn_foe_02 = currWave/2;
        ManageSpawning(spawn_foe_02, foePrefabs[1]);
        int spawn_foe_03 = currWave/4;
        ManageSpawning(spawn_foe_03, foePrefabs[2]);
        int spawn_foe_04 = currWave/6;
        ManageSpawning(spawn_foe_04, foePrefabs[3]);
        int spawn_foe_05 = 0;
        if (currWave%8 == 0) { 
            spawn_foe_05 = currWave/8; 
            ManageSpawning(spawn_foe_05, foePrefabs[4]);
        }

        
    }

    private void ManageSpawning(int howMany, Foe foe) {
        if (howMany == 0) { return; }
        Debug.Log($"Wave {currentWave}, spawning {foeToSpawn.Count}");
        for (int i = 0; i < howMany; i++) {
            foeToSpawn.Add(foe);
        }
        foreach (var item in foeToSpawn) {
            SpawnFoe(item.GetFoeId());
        }
        foeToSpawn.Clear();
    }
}

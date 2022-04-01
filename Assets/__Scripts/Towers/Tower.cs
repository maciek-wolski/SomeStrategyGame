using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private Health health = null;
    [SerializeField] private int towerPrice;
    [SerializeField] private GameObject towerDestroyedUI = null;

    public static event Action<Tower> OnTowerSetOwnerId;
    public static event Action<Tower> OnTowerDestroyed;

#region Getters
    public int GetTowerPrice(){
        return towerPrice;
    }
    public Health GetTowerHealth(){
        return health;
    }
#endregion
#region Setters
#endregion

    private void OnEnable() {
        if (towerDestroyedUI.activeInHierarchy) { towerDestroyedUI.SetActive(false); }
        health.OnDie += HandleOnDie;
        OnTowerSetOwnerId?.Invoke(this);
    }
    private void OnDisable() {
        health.OnDie -= HandleOnDie;
    }
    private void OnDestroy() {
        Debug.Log($"destroyed: {this}");
        OnTowerDestroyed?.Invoke(this);
    }

    private void HandleOnDie()
    {
        towerDestroyedUI.SetActive(true);
    }
}
